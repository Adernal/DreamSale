using Denmakers.DreamSale.Data.Context;

namespace Denmakers.DreamSale.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        IDbContext dbContext;

        public IDbContext Init()
        {
            if (dbContext == null)
            {
                dbContext = new DreamSaleObjectContext();
            }
            return dbContext;
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.DisposeObj();
        }
    }
}
