using Denmakers.DreamSale.ViewModels.NonAdminVM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Catalog
{
    public partial class CustomerBackInStockSubscriptionsModel
    {
        public CustomerBackInStockSubscriptionsModel()
        {
            this.Subscriptions = new List<BackInStockSubscriptionModel>();
        }
        public int Id { get; set; }

        public IList<BackInStockSubscriptionModel> Subscriptions { get; set; }
        public PagerModel PagerModel { get; set; }

        #region Nested classes

        public partial class BackInStockSubscriptionModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string SeName { get; set; }
        }

        #endregion
    }
}
