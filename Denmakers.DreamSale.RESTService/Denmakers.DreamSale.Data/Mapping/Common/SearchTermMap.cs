using Denmakers.DreamSale.Model.Common;

namespace Denmakers.DreamSale.Data.Mapping.Common
{
    public partial class SearchTermMap : DreamSaleEntityTypeConfiguration<SearchTerm>
    {
        public SearchTermMap()
        {
            this.ToTable("SearchTerm");
            this.HasKey(st => st.Id);
        }
    }
}
