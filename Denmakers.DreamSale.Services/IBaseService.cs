using Denmakers.DreamSale.Data.Context;

namespace Denmakers.DreamSale.Services
{
    public interface IBaseService
    {
        IWorkContext WorkContext { get;}
        void Commit();
    }
}
