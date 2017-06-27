using DreamSale.Model.Common;

namespace DreamSale.Data.Mapping.Common
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
