namespace ToDoList.Modules
{
    interface IAuthorization
    {
        string GetAuthToken(AuthClaims claims);
        AuthClaims ValidateAuth(string token);
    }
}
