using System;
using System.Collections.Generic;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Orders
{
    public partial class CustomerReturnRequestsModel
    {
        public CustomerReturnRequestsModel()
        {
            Items = new List<ReturnRequestModel>();
        }
        public int Id { get; set; }
        public IList<ReturnRequestModel> Items { get; set; }

        #region Nested classes
        public partial class ReturnRequestModel
        {
            public int Id { get; set; }
            public string CustomNumber { get; set; }
            public string ReturnRequestStatus { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string ProductSeName { get; set; }
            public int Quantity { get; set; }

            public string ReturnReason { get; set; }
            public string ReturnAction { get; set; }
            public string Comments { get; set; }
            public Guid UploadedFileGuid { get; set; }

            public DateTime CreatedOn { get; set; }
        }
        #endregion
    }
}
