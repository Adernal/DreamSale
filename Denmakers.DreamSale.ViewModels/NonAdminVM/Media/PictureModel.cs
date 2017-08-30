using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Media
{
    public partial class PictureModel
    {
        public PictureModel()
        {
            this.CustomProperties = new Dictionary<string, object>();
        }
        public int Id { get; set; }

        public Dictionary<string, object> CustomProperties { get; set; }

        public string ImageUrl { get; set; }

        public string ThumbImageUrl { get; set; }

        public string FullSizeImageUrl { get; set; }

        public string Title { get; set; }

        public string AlternateText { get; set; }
    }
}
