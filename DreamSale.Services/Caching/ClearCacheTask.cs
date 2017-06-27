using DreamSale.Core.Caching;
using DreamSale.Infrastructure;
using DreamSale.Services.Tasks;

namespace DreamSale.Services.Caching
{
    /// <summary>
    /// Clear cache schedueled task implementation
    /// </summary>
    public partial class ClearCacheTask : ITask
    {
        /// <summary>
        /// Executes a task
        /// </summary>
        public void Execute()
        {
            var cacheManager = EngineContext.Current.ContainerManager.Resolve<ICacheManager>("dreamSale_cache_static");
            cacheManager.Clear();
        }
    }
}
