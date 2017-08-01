using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Logging;
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
        protected readonly IBaseService _baseService;
        protected readonly ILogger _logger;
        protected readonly IWebHelper _webHelper;
        #endregion

        #region Ctor
        public ApiControllerBase(IBaseService baseService, ILogger logger, IWebHelper webHelper)
        {
            this._baseService = baseService;
            this._logger = logger;
            this._webHelper = webHelper;
        }
        #endregion

        #region Utilities
        protected virtual void LogError(Exception ex, LogLevel logLevel = LogLevel.Error, Customer customer = null)
        {
            try
            {
                _logger.InsertLog(logLevel, ex.Message, ex.StackTrace, customer ?? _baseService.WorkContext.CurrentCustomer);
                _baseService.Commit();
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
                _logger.InsertLog(logLevel, msg, null, customer ?? _baseService.WorkContext.CurrentCustomer);
                _baseService.Commit();
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
