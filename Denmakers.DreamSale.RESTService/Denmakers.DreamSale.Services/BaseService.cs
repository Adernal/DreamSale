using Denmakers.DreamSale.Data.Infrastructure;

namespace Denmakers.DreamSale.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
