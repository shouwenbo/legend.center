namespace Legend.Api.Service
{
    public interface IJWTService
    {
        string GetToken(string userName);
        string TestGet();
    }
}