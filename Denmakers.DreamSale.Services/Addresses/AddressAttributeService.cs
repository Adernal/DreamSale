using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denmakers.DreamSale.Services.Addresses
{
    public partial class AddressAttributeService : IAddressAttributeService
    {
        #region Fields
        private readonly IRepository<AddressAttribute> _addressAttributeRepository;
        private readonly IRepository<AddressAttributeValue> _addressAttributeValueRepository;
        protected readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor
        public AddressAttributeService(IRepository<AddressAttribute> addressAttributeRepository, IRepository<AddressAttributeValue> addressAttributeValueRepository, IUnitOfWork unitOfWork)
        {
            this._addressAttributeRepository = addressAttributeRepository;
            this._addressAttributeValueRepository = addressAttributeValueRepository;
            //this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all address attributes
        /// </summary>
        /// <returns>Address attributes</returns>
        public virtual IList<AddressAttribute> GetAllAddressAttributes()
        {
            var query = from aa in _addressAttributeRepository.Table
                        orderby aa.DisplayOrder, aa.Id
                        select aa;
            return query.ToList();
        }

        /// <summary>
        /// Gets an address attribute 
        /// </summary>
        /// <param name="addressAttributeId">Address attribute identifier</param>
        /// <returns>Address attribute</returns>
        public virtual AddressAttribute GetAddressAttributeById(int addressAttributeId)
        {
            if (addressAttributeId == 0)
                return null;

            return _addressAttributeRepository.GetById(addressAttributeId);
        }

        /// <summary>
        /// Inserts an address attribute
        /// </summary>
        /// <param name="addressAttribute">Address attribute</param>
        public virtual void InsertAddressAttribute(AddressAttribute addressAttribute)
        {
            if (addressAttribute == null)
                throw new ArgumentNullException("addressAttribute");

            _addressAttributeRepository.Insert(addressAttribute);

            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the address attribute
        /// </summary>
        /// <param name="addressAttribute">Address attribute</param>
        public virtual void UpdateAddressAttribute(AddressAttribute addressAttribute)
        {
            if (addressAttribute == null)
                throw new ArgumentNullException("addressAttribute");

            _addressAttributeRepository.Update(addressAttribute);
            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes an address attribute
        /// </summary>
        /// <param name="addressAttribute">Address attribute</param>
        public virtual void DeleteAddressAttribute(AddressAttribute addressAttribute)
        {
            if (addressAttribute == null)
                throw new ArgumentNullException("addressAttribute");

            _addressAttributeRepository.Delete(addressAttribute);
            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Gets address attribute values by address attribute identifier
        /// </summary>
        /// <param name="addressAttributeId">The address attribute identifier</param>
        /// <returns>Address attribute values</returns>
        public virtual IList<AddressAttributeValue> GetAddressAttributeValues(int addressAttributeId)
        {
            var query = from aav in _addressAttributeValueRepository.Table
                        orderby aav.DisplayOrder, aav.Id
                        where aav.AddressAttributeId == addressAttributeId
                        select aav;
            var addressAttributeValues = query.ToList();
            return addressAttributeValues;
        }

        /// <summary>
        /// Gets an address attribute value
        /// </summary>
        /// <param name="addressAttributeValueId">Address attribute value identifier</param>
        /// <returns>Address attribute value</returns>
        public virtual AddressAttributeValue GetAddressAttributeValueById(int addressAttributeValueId)
        {
            if (addressAttributeValueId == 0)
                return null;

            return _addressAttributeValueRepository.GetById(addressAttributeValueId);
        }

        /// <summary>
        /// Inserts an address attribute value
        /// </summary>
        /// <param name="addressAttributeValue">Address attribute value</param>
        public virtual void InsertAddressAttributeValue(AddressAttributeValue addressAttributeValue)
        {
            if (addressAttributeValue == null)
                throw new ArgumentNullException("addressAttributeValue");

            _addressAttributeValueRepository.Insert(addressAttributeValue);
            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the address attribute value
        /// </summary>
        /// <param name="addressAttributeValue">Address attribute value</param>
        public virtual void UpdateAddressAttributeValue(AddressAttributeValue addressAttributeValue)
        {
            if (addressAttributeValue == null)
                throw new ArgumentNullException("addressAttributeValue");

            _addressAttributeValueRepository.Update(addressAttributeValue);
        }

        /// <summary>
        /// Deletes an address attribute value
        /// </summary>
        /// <param name="addressAttributeValue">Address attribute value</param>
        public virtual void DeleteAddressAttributeValue(AddressAttributeValue addressAttributeValue)
        {
            if (addressAttributeValue == null)
                throw new ArgumentNullException("addressAttributeValue");

            _addressAttributeValueRepository.Delete(addressAttributeValue);
            //_unitOfWork.Commit();
        }

        #endregion
    }
}
