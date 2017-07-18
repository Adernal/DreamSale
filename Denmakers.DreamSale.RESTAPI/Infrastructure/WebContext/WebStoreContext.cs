using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Stores;
using Denmakers.DreamSale.Services.Stores;
using System;
using System.Linq;

namespace Denmakers.DreamSale.RESTAPI.Infrastructure.WebContext
{
    public partial class WebStoreContext : IStoreContext
    {
        private readonly IStoreService _storeService;
        private readonly IWebHelper _webHelper;
        private Store _cachedStore;

        public WebStoreContext(IStoreService storeService, IWebHelper webHelper)
        {
            this._storeService = storeService;
            this._webHelper = webHelper;
        }

        /// <summary>
        /// Gets or sets the current store
        /// </summary>
        public virtual Store CurrentStore
        {
            get
            {
                if (_cachedStore != null)
                    return _cachedStore;

                //ty to determine the current store by HTTP_HOST
                var host = _webHelper.ServerVariables("HTTP_HOST");
                var allStores = _storeService.GetAllStores();
                var store = allStores.FirstOrDefault(s => s.ContainsHostValue(host));

                if (store == null)
                {
                    //load the first found store
                    store = allStores.FirstOrDefault();
                }
                if (store == null)
                    throw new Exception("No store could be loaded");

                _cachedStore = store;
                return _cachedStore;
            }
        }
    }
}