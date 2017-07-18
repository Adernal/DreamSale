using System;
using System.Web.Mvc;
using System.ComponentModel;
using FluentValidation.Attributes;
using Denmakers.DreamSale.ViewModels.Validators.Catalog;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    [Validator(typeof(ProductReviewValidator))]
    public partial class ProductReviewVM
    {
        public int Id { get; set; }

        [DisplayName("Store Name" )]
        public string StoreName { get; set; }

        [DisplayName("Product")]
        public int ProductId { get; set; }

        [DisplayName("Product")]
        public string ProductName { get; set; }

        [DisplayName("Customer")]
        public int CustomerId { get; set; }

        [DisplayName("Customer")]
        public string CustomerInfo { get; set; }

        [AllowHtml]
        [DisplayName("Title")]
        public string Title { get; set; }

        [AllowHtml]
        [DisplayName("Review Text")]
        public string ReviewText { get; set; }

        [AllowHtml]
        [DisplayName("Reply Text")]
        public string ReplyText { get; set; }

        [DisplayName("Rating")]
        public int Rating { get; set; }

        [DisplayName("Is Approved")]
        public bool IsApproved { get; set; }

        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }

        //vendor
        public bool IsLoggedInAsVendor { get; set; }
    }
}
