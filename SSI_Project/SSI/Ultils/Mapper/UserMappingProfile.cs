using AutoMapper;
using SSI.Models;

//using SSI.Models;
using SSI.Ultils.ViewModel;

namespace SSI.Ultils.Mapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => HashPassword(src.Password)));

            CreateMap<User, UserViewModel>();
        }

        private string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }
    }
}
