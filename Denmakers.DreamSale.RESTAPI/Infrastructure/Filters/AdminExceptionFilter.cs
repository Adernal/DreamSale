using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Denmakers.DreamSale.RESTAPI.Infrastructure.Filters
{
    public class AdminExceptionFilter : ExceptionFilterAttribute
    {
        #region Fields
        protected readonly IRepository<Log> _logRepository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IWorkContext _workContext;
        protected readonly IWebHelper _webHelper;
        #endregion

        #region Ctro
        public AdminExceptionFilter(IRepository<Log> logRepository, IUnitOfWork unitOfWork, IWorkContext workContext, IWebHelper webHelper)
        {
            this._logRepository = logRepository;
            this._unitOfWork = unitOfWork;
            this._workContext = workContext;
            this._webHelper = webHelper;
        }
        #endregion

        #region Utilities
        private void LogException(Exception ex)
        {
            var customer = _workContext.CurrentCustomer;

            Log _error = new Log()
            {
                LogLevel = LogLevel.Error,
                ShortMessage = ex.Message,
                FullMessage = ex.StackTrace,
                IpAddress = _webHelper.GetCurrentIpAddress(),
                Customer = customer,
                PageUrl = _webHelper.GetThisPageUrl(true),
                ReferrerUrl = _webHelper.GetUrlReferrer(),
                CreatedOnUtc = DateTime.UtcNow
            };

            _logRepository.Insert(_error);
            _unitOfWork.Commit();
        }
        #endregion

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Exception ex = null;
            if (actionExecutedContext.Exception.InnerException == null)
            {
                ex = actionExecutedContext.Exception;
            }
            else
            {
                ex = actionExecutedContext.Exception.InnerException;
            }
            LogException(ex);
            ////We can log this exception message to the file or database.  
            //var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            //{
            //    Content = new StringContent("An unhandled exception was thrown by service."),
            //    ReasonPhrase = "Internal Server Error.Please Contact your Administrator."
            //};
            //actionExecutedContext.Response = response;
        }
    }
}