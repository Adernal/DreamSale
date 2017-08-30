using Denmakers.DreamSale.ViewModels.Validators.Catalog;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Catalog
{
    public partial class ProductReviewOverviewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public int RatingSum { get; set; }

        public int TotalReviews { get; set; }

        public bool AllowCustomerReviews { get; set; }
    }

    [Validator(typeof(CusProductReviewsValidator))]
    public partial class ProductReviewsModel
    {
        public ProductReviewsModel()
        {
            Items = new List<ProductReviewModel>();
            AddProductReview = new AddProductReviewModel();
        }
        public int Id { get; set; }
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductSeName { get; set; }

        public IList<ProductReviewModel> Items { get; set; }
        public AddProductReviewModel AddProductReview { get; set; }
    }

    public partial class ProductReviewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public bool AllowViewingProfiles { get; set; }

        public string Title { get; set; }

        public string ReviewText { get; set; }

        public string ReplyText { get; set; }

        public int Rating { get; set; }

        public ProductReviewHelpfulnessModel Helpfulness { get; set; }

        public string WrittenOnStr { get; set; }
    }


    public partial class ProductReviewHelpfulnessModel
    {
        public int Id { get; set; }
        public int ProductReviewId { get; set; }

        public int HelpfulYesTotal { get; set; }

        public int HelpfulNoTotal { get; set; }
    }

    public partial class AddProductReviewModel
    {
        public int Id { get; set; }
        [AllowHtml]
        [DisplayName("Reviews.Fields.Title")]
        public string Title { get; set; }

        [AllowHtml]
        [DisplayName("Reviews.Fields.ReviewText")]
        public string ReviewText { get; set; }

        [DisplayName("Reviews.Fields.Rating")]
        public int Rating { get; set; }

        public bool DisplayCaptcha { get; set; }

        public bool CanCurrentCustomerLeaveReview { get; set; }
        public bool SuccessfullyAdded { get; set; }
        public string Result { get; set; }
    }
}
