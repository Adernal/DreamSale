//Contributor:  Nicholas Mayne


namespace DreamSale.Services.Authentication.External
{
    /// <summary>
    /// Claims translator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IClaimsTranslator<T>
    {
        UserClaims Translate(T response);
    }
}