using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.Services.Customers
{
    public partial class CustomerAttributeService : ICustomerAttributeService
    {
        #region Fields

        private readonly IRepository<CustomerAttribute> _customerAttributeRepository;
        private readonly IRepository<CustomerAttributeValue> _customerAttributeValueRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor
        public CustomerAttributeService(/*ICacheManager cacheManager,*/
            IRepository<CustomerAttribute> customerAttributeRepository,
            IRepository<CustomerAttributeValue> customerAttributeValueRepository,
            IUnitOfWork unitOfWork)
        {
            //this._cacheManager = cacheManager;
            this._customerAttributeRepository = customerAttributeRepository;
            this._customerAttributeValueRepository = customerAttributeValueRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes a customer attribute
        /// </summary>
        /// <param name="customerAttribute">Customer attribute</param>
        public virtual void DeleteCustomerAttribute(CustomerAttribute customerAttribute)
        {
            if (customerAttribute == null)
                throw new ArgumentNullException("customerAttribute");

            _customerAttributeRepository.Delete(customerAttribute);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Gets all customer attributes
        /// </summary>
        /// <returns>Customer attributes</returns>
        public virtual IList<CustomerAttribute> GetAllCustomerAttributes()
        {
            var query = from ca in _customerAttributeRepository.Table
                        orderby ca.DisplayOrder, ca.Id
                        select ca;
            return query.ToList();
        }

        /// <summary>
        /// Gets a customer attribute 
        /// </summary>
        /// <param name="customerAttributeId">Customer attribute identifier</param>
        /// <returns>Customer attribute</returns>
        public virtual CustomerAttribute GetCustomerAttributeById(int customerAttributeId)
        {
            if (customerAttributeId == 0)
                return null;

            return _customerAttributeRepository.GetById(customerAttributeId);
        }

        /// <summary>
        /// Inserts a customer attribute
        /// </summary>
        /// <param name="customerAttribute">Customer attribute</param>
        public virtual void InsertCustomerAttribute(CustomerAttribute customerAttribute)
        {
            if (customerAttribute == null)
                throw new ArgumentNullException("customerAttribute");

            _customerAttributeRepository.Insert(customerAttribute);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the customer attribute
        /// </summary>
        /// <param name="customerAttribute">Customer attribute</param>
        public virtual void UpdateCustomerAttribute(CustomerAttribute customerAttribute)
        {
            if (customerAttribute == null)
                throw new ArgumentNullException("customerAttribute");

            _customerAttributeRepository.Update(customerAttribute);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes a customer attribute value
        /// </summary>
        /// <param name="customerAttributeValue">Customer attribute value</param>
        public virtual void DeleteCustomerAttributeValue(CustomerAttributeValue customerAttributeValue)
        {
            if (customerAttributeValue == null)
                throw new ArgumentNullException("customerAttributeValue");

            _customerAttributeValueRepository.Delete(customerAttributeValue);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Gets customer attribute values by customer attribute identifier
        /// </summary>
        /// <param name="customerAttributeId">The customer attribute identifier</param>
        /// <returns>Customer attribute values</returns>
        public virtual IList<CustomerAttributeValue> GetCustomerAttributeValues(int customerAttributeId)
        {
            var query = from cav in _customerAttributeValueRepository.Table
                        orderby cav.DisplayOrder, cav.Id
                        where cav.CustomerAttributeId == customerAttributeId
                        select cav;
            var customerAttributeValues = query.ToList();
            return customerAttributeValues;
        }

        /// <summary>
        /// Gets a customer attribute value
        /// </summary>
        /// <param name="customerAttributeValueId">Customer attribute value identifier</param>
        /// <returns>Customer attribute value</returns>
        public virtual CustomerAttributeValue GetCustomerAttributeValueById(int customerAttributeValueId)
        {
            if (customerAttributeValueId == 0)
                return null;

            return _customerAttributeValueRepository.GetById(customerAttributeValueId);
        }

        /// <summary>
        /// Inserts a customer attribute value
        /// </summary>
        /// <param name="customerAttributeValue">Customer attribute value</param>
        public virtual void InsertCustomerAttributeValue(CustomerAttributeValue customerAttributeValue)
        {
            if (customerAttributeValue == null)
                throw new ArgumentNullException("customerAttributeValue");

            _customerAttributeValueRepository.Insert(customerAttributeValue);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the customer attribute value
        /// </summary>
        /// <param name="customerAttributeValue">Customer attribute value</param>
        public virtual void UpdateCustomerAttributeValue(CustomerAttributeValue customerAttributeValue)
        {
            if (customerAttributeValue == null)
                throw new ArgumentNullException("customerAttributeValue");

            _customerAttributeValueRepository.Update(customerAttributeValue);

            _unitOfWork.Commit();
        }

        #endregion
    }
}
