using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
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
        protected readonly IRepository<Log> _logRepository;
        protected readonly IUnitOfWork _unitOfWork;

        public ApiControllerBase(IRepository<Log> errorsRepository, IUnitOfWork unitOfWork)
        {
            _logRepository = errorsRepository;
            _unitOfWork = unitOfWork;
        }

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
        private void LogError(Exception ex, LogLevel logLevel = LogLevel.Error, Customer customer = null)
        {
            try
            {
                Log _error = new Log()
                {
                    LogLevel = logLevel,
                    ShortMessage = ex.Message,
                    FullMessage = ex.StackTrace,
                    //IpAddress = _webHelper.GetCurrentIpAddress(),
                    Customer = customer,
                    //PageUrl = _webHelper.GetThisPageUrl(true),
                    //ReferrerUrl = _webHelper.GetUrlReferrer(),
                    CreatedOnUtc = DateTime.UtcNow
                };

                _logRepository.Insert(_error);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                string errorMsg = e.Message;
            }
        }
    }
}
