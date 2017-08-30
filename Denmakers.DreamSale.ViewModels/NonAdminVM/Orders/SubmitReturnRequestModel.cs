using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Orders
{
    public partial class SubmitReturnRequestModel
    {
        public SubmitReturnRequestModel()
        {
            Items = new List<OrderItemModel>();
            AvailableReturnReasons = new List<ReturnRequestReasonModel>();
            AvailableReturnActions = new List<ReturnRequestActionModel>();
        }
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string CustomOrderNumber { get; set; }

        public IList<OrderItemModel> Items { get; set; }

        [AllowHtml]
        [DisplayName("ReturnRequests.ReturnReason")]
        public int ReturnRequestReasonId { get; set; }
        public IList<ReturnRequestReasonModel> AvailableReturnReasons { get; set; }

        [AllowHtml]
        [DisplayName("ReturnRequests.ReturnAction")]
        public int ReturnRequestActionId { get; set; }
        public IList<ReturnRequestActionModel> AvailableReturnActions { get; set; }

        [AllowHtml]
        [DisplayName("ReturnRequests.Comments")]
        public string Comments { get; set; }

        public bool AllowFiles { get; set; }
        [DisplayName("ReturnRequests.UploadedFile")]
        public Guid UploadedFileGuid { get; set; }

        public string Result { get; set; }

        #region Nested classes

        public partial class OrderItemModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }

            public string ProductName { get; set; }

            public string ProductSeName { get; set; }

            public string AttributeInfo { get; set; }

            public string UnitPrice { get; set; }

            public int Quantity { get; set; }
        }

        public partial class ReturnRequestReasonModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public partial class ReturnRequestActionModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        #endregion
    }
}
