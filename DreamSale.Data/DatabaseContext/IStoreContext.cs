
using DreamSale.Model.Stores;

namespace DreamSale.Data.DatabaseContext
{
    public interface IStoreContext
    {
        /// <summary>
        /// Gets or sets the current store
        /// </summary>
        Store CurrentStore { get; }
    }
}
