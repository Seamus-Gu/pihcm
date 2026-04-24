namespace PIHCM.Auth.Interfaces
{
    public interface IAuthService
    {
        public Task CheckLogin(SecurityConfig security, string password, LoginUser user);
    }
}