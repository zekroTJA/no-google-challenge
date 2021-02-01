namespace ToDoList.Modules
{
    public interface IAuthorization
    {
        string GetAuthToken(AuthClaims claims);
        AuthClaims ValidateAuth(string token);
    }
}
