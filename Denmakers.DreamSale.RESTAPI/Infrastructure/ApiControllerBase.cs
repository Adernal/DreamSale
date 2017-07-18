using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Logging;
using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Infrastructure
{
    public class ApiControllerBase : ApiController
    {
        #region Fields
        protected readonly IRepository<Log> _logRepository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IWorkContext _workContext;
        protected readonly IWebHelper _webHelper;
        #endregion

        #region Ctor
        public ApiControllerBase(IRepository<Log> errorsRepository, IUnitOfWork unitOfWork, IWorkContext workContext, IWebHelper webHelper)
        {
            this._logRepository = errorsRepository;
            this._unitOfWork = unitOfWork;
            this._workContext = workContext;
            this._webHelper = webHelper;
        }
        #endregion

        #region Utilities
        protected virtual void LogError(Exception ex, LogLevel logLevel = LogLevel.Error, Customer customer = null)
        {
            try
            {
                Log error = new Log()
                {
                    LogLevel = logLevel,
                    ShortMessage = ex.Message,
                    FullMessage = ex.StackTrace,
                    IpAddress = _webHelper.GetCurrentIpAddress(),
                    Customer = customer ?? _workContext.CurrentCustomer,
                    PageUrl = _webHelper.GetThisPageUrl(true),
                    ReferrerUrl = _webHelper.GetUrlReferrer(),
                    CreatedOnUtc = DateTime.UtcNow
                };

                _logRepository.Insert(error);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                string errorMsg = e.Message;
            }
        }
        protected virtual void LogError(string msg, LogLevel logLevel = LogLevel.Error, Customer customer = null)
        {
            try
            {
                Log error = new Log()
                {
                    LogLevel = logLevel,
                    ShortMessage = msg,
                    FullMessage = null,
                    IpAddress = _webHelper.GetCurrentIpAddress(),
                    Customer = customer ?? _workContext.CurrentCustomer,
                    PageUrl = _webHelper.GetThisPageUrl(true),
                    ReferrerUrl = _webHelper.GetUrlReferrer(),
                    CreatedOnUtc = DateTime.UtcNow
                };

                _logRepository.Insert(error);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                string errorMsg = e.Message;
            }
        }
        #endregion

        #region Exposed Methdos
        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;

            try
            {
                response = function.Invoke();
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        //protected virtual ActionResult AccessDeniedView()
        //{
        //    //return new HttpUnauthorizedResult();
        //    return RedirectToAction("AccessDenied", "Security", new { pageUrl = this.Request.RawUrl });
        //}
        #endregion


    }
}
