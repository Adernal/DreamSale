namespace Denmakers.DreamSale.Services.Directory
{
    public partial interface IGeoLookupService
    {
        /// <summary>
        /// Get country name
        /// </summary>
        /// <param name="ipAddress">IP address</param>
        /// <returns>Country name</returns>
        string LookupCountryIsoCode(string ipAddress);

        /// <summary>
        /// Get country name
        /// </summary>
        /// <param name="ipAddress">IP address</param>
        /// <returns>Country name</returns>
        string LookupCountryName(string ipAddress);
    }
}
