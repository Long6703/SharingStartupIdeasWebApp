using SSI.Ultils.ViewModel;

namespace SSI.Services.IService
{
    public interface IAccountService
    {
        Task Register(RegisterViewModel registerViewModel);
        UserViewModel Login(LoginViewModel loginViewModel);
        bool CheckEmail(string email);
        Task UpdateProfile(UserViewModel userViewModel);
        bool ChangePassword(string newpassword, string userEmail);
    }
}
