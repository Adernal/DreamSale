using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model;
using Denmakers.DreamSale.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denmakers.DreamSale.Services.Attributes
{
    public partial class GenericAttributeService : IGenericAttributeService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : entity ID
        /// {1} : key group
        /// </remarks>
        private const string GENERICATTRIBUTE_KEY = "DreamSale.genericattribute.{0}-{1}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string GENERICATTRIBUTE_PATTERN_KEY = "DreamSale.genericattribute.";
        #endregion

        #region Fields

        private readonly IRepository<GenericAttribute> _genericAttributeRepository;
        protected readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor
        public GenericAttributeService(IRepository<GenericAttribute> genericAttributeRepository, IUnitOfWork unitOfWork)
        {
            this._genericAttributeRepository = genericAttributeRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        
        public virtual void DeleteAttribute(GenericAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");

            _genericAttributeRepository.Delete(attribute);
            _unitOfWork.Commit();

            //cache
            //_cacheManager.RemoveByPattern(GENERICATTRIBUTE_PATTERN_KEY);

            //event notification
            //_eventPublisher.EntityDeleted(attribute);
        }
        public virtual void DeleteAttributes(IList<GenericAttribute> attributes)
        {
            if (attributes == null)
                throw new ArgumentNullException("attributes");

            _genericAttributeRepository.Delete(attributes);
            _unitOfWork.Commit();

            //cache
            //_cacheManager.RemoveByPattern(GENERICATTRIBUTE_PATTERN_KEY);

            //event notification
            //foreach (var attribute in attributes)
            //{
            //    _eventPublisher.EntityDeleted(attribute);
            //}
        }

        
        public virtual GenericAttribute GetAttributeById(int attributeId)
        {
            if (attributeId == 0)
                return null;

            return _genericAttributeRepository.GetById(attributeId);
        }

        
        public virtual void InsertAttribute(GenericAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");

            _genericAttributeRepository.Insert(attribute);
            _unitOfWork.Commit();
            //cache
            //_cacheManager.RemoveByPattern(GENERICATTRIBUTE_PATTERN_KEY);

            //event notification
            //_eventPublisher.EntityInserted(attribute);
        }

        
        public virtual void UpdateAttribute(GenericAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");

            _genericAttributeRepository.Update(attribute);

            //cache
            //_cacheManager.RemoveByPattern(GENERICATTRIBUTE_PATTERN_KEY);

            //event notification
            //_eventPublisher.EntityUpdated(attribute);
        }

        public virtual IList<GenericAttribute> GetAttributesForEntity(int entityId, string keyGroup)
        {
            string key = string.Format(GENERICATTRIBUTE_KEY, entityId, keyGroup);
            var attributes = _genericAttributeRepository.FindBy(a => a.KeyGroup == keyGroup).ToList();
            return attributes;
        }

        public virtual void SaveAttribute<TPropType>(BaseEntity entity, string key, TPropType value, int storeId = 0)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (key == null)
                throw new ArgumentNullException("key");

            string keyGroup = "";// entity.GetUnproxiedEntityType().Name;

            var props = GetAttributesForEntity(entity.Id, keyGroup)
                .Where(x => x.StoreId == storeId)
                .ToList();
            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); //should be culture invariant

            var valueStr = CommonHelper.To<string>(value);

            if (prop != null)
            {
                if (string.IsNullOrWhiteSpace(valueStr))
                {
                    //delete
                    DeleteAttribute(prop);
                    _unitOfWork.Commit();
                }
                else
                {
                    //update
                    prop.Value = valueStr;
                    UpdateAttribute(prop);
                    _unitOfWork.Commit();
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(valueStr))
                {
                    //insert
                    prop = new GenericAttribute
                    {
                        EntityId = entity.Id,
                        Key = key,
                        KeyGroup = keyGroup,
                        Value = valueStr,
                        StoreId = storeId,

                    };
                    InsertAttribute(prop);
                    _unitOfWork.Commit();
                }
            }
        }

        #endregion
    }
}
