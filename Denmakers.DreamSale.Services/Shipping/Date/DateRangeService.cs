using System;
using System.Collections.Generic;
using System.Linq;
using Denmakers.DreamSale.Model.Shipping;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;

namespace Denmakers.DreamSale.Services.Shipping.Date
{
    public partial class DateRangeService : IDateRangeService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<DeliveryDate> _deliveryDateRepository;
        private readonly IRepository<ProductAvailabilityRange> _productAvailabilityRangeRepository;

        #endregion

        #region Ctor
        public DateRangeService(IUnitOfWork unitOfWork,
            IRepository<DeliveryDate> deliveryDateRepository,
            IRepository<ProductAvailabilityRange> productAvailabilityRangeRepository)
        {
            this._unitOfWork = unitOfWork;
            this._deliveryDateRepository = deliveryDateRepository;
            this._productAvailabilityRangeRepository = productAvailabilityRangeRepository;
        }

        #endregion

        #region Methods

        #region Delivery dates

        /// <summary>
        /// Get a delivery date
        /// </summary>
        /// <param name="deliveryDateId">The delivery date identifier</param>
        /// <returns>Delivery date</returns>
        public virtual DeliveryDate GetDeliveryDateById(int deliveryDateId)
        {
            if (deliveryDateId == 0)
                return null;

            return _deliveryDateRepository.GetById(deliveryDateId);
        }

        /// <summary>
        /// Get all delivery dates
        /// </summary>
        /// <returns>Delivery dates</returns>
        public virtual IList<DeliveryDate> GetAllDeliveryDates()
        {
            var query = from dd in _deliveryDateRepository.Table
                        orderby dd.DisplayOrder, dd.Id
                        select dd;
            var deliveryDates = query.ToList();
            return deliveryDates;
        }

        /// <summary>
        /// Insert a delivery date
        /// </summary>
        /// <param name="deliveryDate">Delivery date</param>
        public virtual void InsertDeliveryDate(DeliveryDate deliveryDate)
        {
            if (deliveryDate == null)
                throw new ArgumentNullException("deliveryDate");

            _deliveryDateRepository.Insert(deliveryDate);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Update the delivery date
        /// </summary>
        /// <param name="deliveryDate">Delivery date</param>
        public virtual void UpdateDeliveryDate(DeliveryDate deliveryDate)
        {
            if (deliveryDate == null)
                throw new ArgumentNullException("deliveryDate");

            _deliveryDateRepository.Update(deliveryDate);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Delete a delivery date
        /// </summary>
        /// <param name="deliveryDate">The delivery date</param>
        public virtual void DeleteDeliveryDate(DeliveryDate deliveryDate)
        {
            if (deliveryDate == null)
                throw new ArgumentNullException("deliveryDate");

            _deliveryDateRepository.Delete(deliveryDate);

            _unitOfWork.Commit();
        }

        #endregion

        #region Product availability ranges

        /// <summary>
        /// Get a product availability range
        /// </summary>
        /// <param name="productAvailabilityRangeId">The product availability range identifier</param>
        /// <returns>Product availability range</returns>
        public virtual ProductAvailabilityRange GetProductAvailabilityRangeById(int productAvailabilityRangeId)
        {
            return productAvailabilityRangeId != 0 ? _productAvailabilityRangeRepository.GetById(productAvailabilityRangeId) : null;
        }

        /// <summary>
        /// Get all product availability ranges
        /// </summary>
        /// <returns>Product availability ranges</returns>
        public virtual IList<ProductAvailabilityRange> GetAllProductAvailabilityRanges()
        {
            var query = from par in _productAvailabilityRangeRepository.Table
                        orderby par.DisplayOrder, par.Id
                        select par;
            return query.ToList();
        }

        /// <summary>
        /// Insert the product availability range
        /// </summary>
        /// <param name="productAvailabilityRange">Product availability range</param>
        public virtual void InsertProductAvailabilityRange(ProductAvailabilityRange productAvailabilityRange)
        {
            if (productAvailabilityRange == null)
                throw new ArgumentNullException("productAvailabilityRange");

            _productAvailabilityRangeRepository.Insert(productAvailabilityRange);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Update the product availability range
        /// </summary>
        /// <param name="productAvailabilityRange">Product availability range</param>
        public virtual void UpdateProductAvailabilityRange(ProductAvailabilityRange productAvailabilityRange)
        {
            if (productAvailabilityRange == null)
                throw new ArgumentNullException("productAvailabilityRange");

            _productAvailabilityRangeRepository.Update(productAvailabilityRange);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Delete the product availability range
        /// </summary>
        /// <param name="productAvailabilityRange">Product availability range</param>
        public virtual void DeleteProductAvailabilityRange(ProductAvailabilityRange productAvailabilityRange)
        {
            if (productAvailabilityRange == null)
                throw new ArgumentNullException("productAvailabilityRange");

            _productAvailabilityRangeRepository.Delete(productAvailabilityRange);

            _unitOfWork.Commit();
        }

        #endregion

        #endregion
    }
}
