using System.Data.Entity.ModelConfiguration;

namespace Denmakers.DreamSale.Data.Mapping
{
    public abstract class DreamSaleEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        protected DreamSaleEntityTypeConfiguration()
        {
            PostInitialize();
        }

        /// <summary>
        /// Developers can override this method in custom partial classes
        /// in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {
            
        }
    }
}