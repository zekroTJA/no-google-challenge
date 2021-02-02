namespace ToDoList.Modules
{
    /// <summary>
    /// Module to create an authorizeation token string
    /// from the given <see cref="AuthClaims"/> and validating
    /// a given authorization token string returning the 
    /// previously used <see cref="AuthClaims"/> to create
    /// the token.
    /// </summary>
    public interface IAuthorization
    {
        /// <summary>
        /// Returns a authorization token string from the given
        /// <see cref="AuthClaims"/> instance.
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        string GetAuthToken(AuthClaims claims);

        /// <summary>
        /// Validates the given autorization token and recovers
        /// the previously used <see cref="AuthClaims"/> used
        /// to generate the given token.
        /// 
        /// If the token validation fails, an exception is thrown.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        AuthClaims ValidateAuth(string token);
    }
}
