namespace Denmakers.DreamSale.Model.Clients
{
    public partial class Client : BaseEntity
    {
        public string Secret { get; set; }

        public string Name { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }

        public string AllowedOrigin { get; set; }
    }

    public enum ApplicationTypes
    {
        JavaScript = 0,
        NativeConfidential = 1
    };
}
