using DreamSale.Model.Discounts;

namespace DreamSale.Data.Mapping.Discounts
{
    public partial class DiscountRequirementMap : DreamSaleEntityTypeConfiguration<DiscountRequirement>
    {
        public DiscountRequirementMap()
        {
            this.ToTable("DiscountRequirement");
            this.HasKey(requirement => requirement.Id);

            this.Ignore(requirement => requirement.InteractionType);
            this.HasMany(requirement => requirement.ChildRequirements)
                .WithOptional()
                .HasForeignKey(requirement => requirement.ParentId);
        }
    }
}