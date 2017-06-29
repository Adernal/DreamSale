using Denmakers.DreamSale.Model.Tasks;

namespace Denmakers.DreamSale.Data.Mapping.Tasks
{
    public partial class ScheduleTaskMap : DreamSaleEntityTypeConfiguration<ScheduleTask>
    {
        public ScheduleTaskMap()
        {
            this.ToTable("ScheduleTask");
            this.HasKey(t => t.Id);
            this.Property(t => t.Name).IsRequired();
            this.Property(t => t.Type).IsRequired();
        }
    }
}