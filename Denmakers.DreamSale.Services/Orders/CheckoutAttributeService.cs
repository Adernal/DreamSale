using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Orders;
using Denmakers.DreamSale.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denmakers.DreamSale.Services.Orders
{
    public partial class CheckoutAttributeService : ICheckoutAttributeService
    {
        #region Fields

        private readonly IRepository<CheckoutAttribute> _checkoutAttributeRepository;
        private readonly IRepository<CheckoutAttributeValue> _checkoutAttributeValueRepository;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor
        public CheckoutAttributeService(/*ICacheManager cacheManager,*/
            IRepository<CheckoutAttribute> checkoutAttributeRepository,
            IRepository<CheckoutAttributeValue> checkoutAttributeValueRepository,
            IStoreMappingService storeMappingService,
            IUnitOfWork unitOfWork)
        {
            //this._cacheManager = cacheManager;
            this._checkoutAttributeRepository = checkoutAttributeRepository;
            this._checkoutAttributeValueRepository = checkoutAttributeValueRepository;
            this._storeMappingService = storeMappingService;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        #region Checkout attributes

        /// <summary>
        /// Gets all checkout attributes
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <param name="excludeShippableAttributes">A value indicating whether we should exlude shippable attributes</param>
        /// <returns>Checkout attributes</returns>
        public virtual IList<CheckoutAttribute> GetAllCheckoutAttributes(int storeId = 0, bool excludeShippableAttributes = false)
        {
            var query = from ca in _checkoutAttributeRepository.Table
                        orderby ca.DisplayOrder, ca.Id
                        select ca;
            var checkoutAttributes = query.ToList();
            if (storeId > 0)
            {
                //store mapping
                checkoutAttributes = checkoutAttributes.Where(ca => _storeMappingService.Authorize(ca)).ToList();
            }
            if (excludeShippableAttributes)
            {
                //remove attributes which require shippable products
                checkoutAttributes = checkoutAttributes.RemoveShippableAttributes().ToList();
            }
            return checkoutAttributes;
        }

        /// <summary>
        /// Gets a checkout attribute 
        /// </summary>
        /// <param name="checkoutAttributeId">Checkout attribute identifier</param>
        /// <returns>Checkout attribute</returns>
        public virtual CheckoutAttribute GetCheckoutAttributeById(int checkoutAttributeId)
        {
            if (checkoutAttributeId == 0)
                return null;

            return _checkoutAttributeRepository.GetById(checkoutAttributeId);
        }

        /// <summary>
        /// Inserts a checkout attribute
        /// </summary>
        /// <param name="checkoutAttribute">Checkout attribute</param>
        public virtual void InsertCheckoutAttribute(CheckoutAttribute checkoutAttribute)
        {
            if (checkoutAttribute == null)
                throw new ArgumentNullException("checkoutAttribute");

            _checkoutAttributeRepository.Insert(checkoutAttribute);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the checkout attribute
        /// </summary>
        /// <param name="checkoutAttribute">Checkout attribute</param>
        public virtual void UpdateCheckoutAttribute(CheckoutAttribute checkoutAttribute)
        {
            if (checkoutAttribute == null)
                throw new ArgumentNullException("checkoutAttribute");

            _checkoutAttributeRepository.Update(checkoutAttribute);
            _unitOfWork.Commit();
        }

        #endregion

        #region Checkout attribute values

        /// <summary>
        /// Deletes a checkout attribute value
        /// </summary>
        /// <param name="checkoutAttributeValue">Checkout attribute value</param>
        public virtual void DeleteCheckoutAttributeValue(CheckoutAttributeValue checkoutAttributeValue)
        {
            if (checkoutAttributeValue == null)
                throw new ArgumentNullException("checkoutAttributeValue");

            _checkoutAttributeValueRepository.Delete(checkoutAttributeValue);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Gets checkout attribute values by checkout attribute identifier
        /// </summary>
        /// <param name="checkoutAttributeId">The checkout attribute identifier</param>
        /// <returns>Checkout attribute values</returns>
        public virtual IList<CheckoutAttributeValue> GetCheckoutAttributeValues(int checkoutAttributeId)
        {
            var query = from cav in _checkoutAttributeValueRepository.Table
                        orderby cav.DisplayOrder, cav.Id
                        where cav.CheckoutAttributeId == checkoutAttributeId
                        select cav;
            var checkoutAttributeValues = query.ToList();
            return checkoutAttributeValues;
        }

        /// <summary>
        /// Gets a checkout attribute value
        /// </summary>
        /// <param name="checkoutAttributeValueId">Checkout attribute value identifier</param>
        /// <returns>Checkout attribute value</returns>
        public virtual CheckoutAttributeValue GetCheckoutAttributeValueById(int checkoutAttributeValueId)
        {
            if (checkoutAttributeValueId == 0)
                return null;

            return _checkoutAttributeValueRepository.GetById(checkoutAttributeValueId);
        }

        /// <summary>
        /// Inserts a checkout attribute value
        /// </summary>
        /// <param name="checkoutAttributeValue">Checkout attribute value</param>
        public virtual void InsertCheckoutAttributeValue(CheckoutAttributeValue checkoutAttributeValue)
        {
            if (checkoutAttributeValue == null)
                throw new ArgumentNullException("checkoutAttributeValue");

            _checkoutAttributeValueRepository.Insert(checkoutAttributeValue);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the checkout attribute value
        /// </summary>
        /// <param name="checkoutAttributeValue">Checkout attribute value</param>
        public virtual void UpdateCheckoutAttributeValue(CheckoutAttributeValue checkoutAttributeValue)
        {
            if (checkoutAttributeValue == null)
                throw new ArgumentNullException("checkoutAttributeValue");

            _checkoutAttributeValueRepository.Update(checkoutAttributeValue);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes a checkout attribute
        /// </summary>
        /// <param name="checkoutAttribute">Checkout attribute</param>
        public virtual void DeleteCheckoutAttribute(CheckoutAttribute checkoutAttribute)
        {
            if (checkoutAttribute == null)
                throw new ArgumentNullException("checkoutAttribute");

            _checkoutAttributeRepository.Delete(checkoutAttribute);
            _unitOfWork.Commit();
        }

        #endregion

        #endregion
    }
}
