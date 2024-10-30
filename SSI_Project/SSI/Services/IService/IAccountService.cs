using SSI.Ultils.ViewModel;

namespace SSI.Services.IService
{
    public interface IAccountService
    {
        Task Register(RegisterViewModel registerViewModel);
    }
}
