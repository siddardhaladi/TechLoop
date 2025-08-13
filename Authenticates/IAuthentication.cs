namespace TechLoop.Authenticates
{
    public interface IAuthentication
    {
        string Authenticate(string email, string password,string role);
    }
}
