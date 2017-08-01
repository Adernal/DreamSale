using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;

namespace Denmakers.DreamSale.Services
{
    public partial class BaseService : IBaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IWorkContext _workContext;

        public BaseService(IUnitOfWork unitOfWork, IWorkContext workContext)
        {
            this._unitOfWork = unitOfWork;
            this._workContext = workContext;
        }

        public IWorkContext WorkContext
        {
            get
            {
               return _workContext;
            }
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }
    }
}
