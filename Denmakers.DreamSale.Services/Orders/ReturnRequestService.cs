﻿using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denmakers.DreamSale.Services.Orders
{
    public partial class ReturnRequestService : IReturnRequestService
    {
        #region Fields

        private readonly IRepository<ReturnRequest> _returnRequestRepository;
        private readonly IRepository<ReturnRequestAction> _returnRequestActionRepository;
        private readonly IRepository<ReturnRequestReason> _returnRequestReasonRepository;

        #endregion

        #region Ctor
        public ReturnRequestService(IRepository<ReturnRequest> returnRequestRepository,
            IRepository<ReturnRequestAction> returnRequestActionRepository,
            IRepository<ReturnRequestReason> returnRequestReasonRepository)
        {
            this._returnRequestRepository = returnRequestRepository;
            this._returnRequestActionRepository = returnRequestActionRepository;
            this._returnRequestReasonRepository = returnRequestReasonRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes a return request
        /// </summary>
        /// <param name="returnRequest">Return request</param>
        public virtual void DeleteReturnRequest(ReturnRequest returnRequest)
        {
            if (returnRequest == null)
                throw new ArgumentNullException("returnRequest");

            _returnRequestRepository.Delete(returnRequest);
        }

        /// <summary>
        /// Gets a return request
        /// </summary>
        /// <param name="returnRequestId">Return request identifier</param>
        /// <returns>Return request</returns>
        public virtual ReturnRequest GetReturnRequestById(int returnRequestId)
        {
            if (returnRequestId == 0)
                return null;

            return _returnRequestRepository.GetById(returnRequestId);
        }

        /// <summary>
        /// Search return requests
        /// </summary>
        /// <param name="storeId">Store identifier; 0 to load all entries</param>
        /// <param name="customerId">Customer identifier; 0 to load all entries</param>
        /// <param name="orderItemId">Order item identifier; 0 to load all entries</param>
        /// <param name="customNumber">Custom number; null or empty to load all entries</param>
        /// <param name="rs">Return request status; null to load all entries</param>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Return requests</returns>
        public IPagedList<ReturnRequest> SearchReturnRequests(int storeId = 0, int customerId = 0,
            int orderItemId = 0, string customNumber = "", ReturnRequestStatus? rs = null, DateTime? createdFromUtc = null,
            DateTime? createdToUtc = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _returnRequestRepository.Table;
            if (storeId > 0)
                query = query.Where(rr => storeId == rr.StoreId);
            if (customerId > 0)
                query = query.Where(rr => customerId == rr.CustomerId);
            if (rs.HasValue)
            {
                var returnStatusId = (int)rs.Value;
                query = query.Where(rr => rr.ReturnRequestStatusId == returnStatusId);
            }
            if (orderItemId > 0)
                query = query.Where(rr => rr.OrderItemId == orderItemId);

            if (!string.IsNullOrEmpty(customNumber))
                query = query.Where(rr => rr.CustomNumber == customNumber);

            if (createdFromUtc.HasValue)
                query = query.Where(rr => createdFromUtc.Value <= rr.CreatedOnUtc);
            if (createdToUtc.HasValue)
                query = query.Where(rr => createdToUtc.Value >= rr.CreatedOnUtc);

            query = query.OrderByDescending(rr => rr.CreatedOnUtc).ThenByDescending(rr => rr.Id);

            var returnRequests = new PagedList<ReturnRequest>(query, pageIndex, pageSize);
            return returnRequests;
        }



        /// <summary>
        /// Delete a return request action
        /// </summary>
        /// <param name="returnRequestAction">Return request action</param>
        public virtual void DeleteReturnRequestAction(ReturnRequestAction returnRequestAction)
        {
            if (returnRequestAction == null)
                throw new ArgumentNullException("returnRequestAction");

            _returnRequestActionRepository.Delete(returnRequestAction);
        }

        /// <summary>
        /// Gets all return request actions
        /// </summary>
        /// <returns>Return request actions</returns>
        public virtual IList<ReturnRequestAction> GetAllReturnRequestActions()
        {
            var query = from rra in _returnRequestActionRepository.Table
                        orderby rra.DisplayOrder, rra.Id
                        select rra;
            return query.ToList();
        }

        /// <summary>
        /// Gets a return request action
        /// </summary>
        /// <param name="returnRequestActionId">Return request action identifier</param>
        /// <returns>Return request action</returns>
        public virtual ReturnRequestAction GetReturnRequestActionById(int returnRequestActionId)
        {
            if (returnRequestActionId == 0)
                return null;

            return _returnRequestActionRepository.GetById(returnRequestActionId);
        }

        /// <summary>
        /// Inserts a return request action
        /// </summary>
        /// <param name="returnRequestAction">Return request action</param>
        public virtual void InsertReturnRequestAction(ReturnRequestAction returnRequestAction)
        {
            if (returnRequestAction == null)
                throw new ArgumentNullException("returnRequestAction");

            _returnRequestActionRepository.Insert(returnRequestAction);
        }

        /// <summary>
        /// Updates the  return request action
        /// </summary>
        /// <param name="returnRequestAction">Return request action</param>
        public virtual void UpdateReturnRequestAction(ReturnRequestAction returnRequestAction)
        {
            if (returnRequestAction == null)
                throw new ArgumentNullException("returnRequestAction");

            _returnRequestActionRepository.Update(returnRequestAction);
        }




        /// <summary>
        /// Delete a return request reaspn
        /// </summary>
        /// <param name="returnRequestReason">Return request reason</param>
        public virtual void DeleteReturnRequestReason(ReturnRequestReason returnRequestReason)
        {
            if (returnRequestReason == null)
                throw new ArgumentNullException("returnRequestReason");

            _returnRequestReasonRepository.Delete(returnRequestReason);
        }

        /// <summary>
        /// Gets all return request reaspns
        /// </summary>
        /// <returns>Return request reaspns</returns>
        public virtual IList<ReturnRequestReason> GetAllReturnRequestReasons()
        {
            var query = from rra in _returnRequestReasonRepository.Table
                        orderby rra.DisplayOrder, rra.Id
                        select rra;
            return query.ToList();
        }

        /// <summary>
        /// Gets a return request reaspn
        /// </summary>
        /// <param name="returnRequestReasonId">Return request reaspn identifier</param>
        /// <returns>Return request reaspn</returns>
        public virtual ReturnRequestReason GetReturnRequestReasonById(int returnRequestReasonId)
        {
            if (returnRequestReasonId == 0)
                return null;

            return _returnRequestReasonRepository.GetById(returnRequestReasonId);
        }

        /// <summary>
        /// Inserts a return request reaspn
        /// </summary>
        /// <param name="returnRequestReason">Return request reaspn</param>
        public virtual void InsertReturnRequestReason(ReturnRequestReason returnRequestReason)
        {
            if (returnRequestReason == null)
                throw new ArgumentNullException("returnRequestReason");

            _returnRequestReasonRepository.Insert(returnRequestReason);
        }

        /// <summary>
        /// Updates the  return request reaspn
        /// </summary>
        /// <param name="returnRequestReason">Return request reaspn</param>
        public virtual void UpdateReturnRequestReason(ReturnRequestReason returnRequestReason)
        {
            if (returnRequestReason == null)
                throw new ArgumentNullException("returnRequestReason");

            _returnRequestReasonRepository.Update(returnRequestReason);
        }

        #endregion
    }
}