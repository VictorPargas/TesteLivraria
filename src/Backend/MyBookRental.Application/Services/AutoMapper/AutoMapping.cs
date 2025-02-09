using AutoMapper;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Entities;

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

            CreateMap<RequestRenewBookRentalJson, BookRental>();
        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();

            CreateMap<Domain.Entities.Book, ResponseRegisteredBookJson>();
            CreateMap<Domain.Entities.BookRental, ResponseRegisteredBookRentalJson>();

            CreateMap<Domain.Entities.BookRental, ResponseBookRentalDetailsJson>()
                .ForMember(dest => dest.RentalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));

            CreateMap<BookRental, ResponseRenewedBookRentalJson>()
               .ForMember(dest => dest.RentalId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.NewDueDate, opt => opt.MapFrom(src => src.DueDate))
               .ForMember(dest => dest.RenewalsCount, opt => opt.MapFrom(src => src.RenewalsCount))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<BookRental, ResponseReturnedBookRentalJson>()
               .ForMember(dest => dest.RentalId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
               .ForMember(dest => dest.LateFee, opt => opt.MapFrom(src => src.LateFee));
         }
    }
}
