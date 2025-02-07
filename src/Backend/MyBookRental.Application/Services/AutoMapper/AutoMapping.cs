using AutoMapper;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;

namespace MyBookRental.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Profile, opt => opt.MapFrom(src => "Usuário"));

            CreateMap<RequestRegisterBookJson, Domain.Entities.Book>();

            CreateMap<RequestRegisterBookRentalJson, Domain.Entities.BookRental>();
        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.Book, ResponseRegisteredBookJson>();
            CreateMap<Domain.Entities.BookRental, ResponseRegisteredBookRentalJson>();

            CreateMap<Domain.Entities.BookRental, ResponseBookRentalDetailsJson>()
                .ForMember(dest => dest.RentalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
        }
    }
}
