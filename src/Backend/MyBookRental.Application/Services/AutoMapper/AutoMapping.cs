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

            CreateMap<RequestBookJson, Domain.Entities.Book>()
               .ForMember(dest => dest.BookAuthors, opt => opt.MapFrom(src =>
                   src.AuthorIds.Select(authorId => new Domain.Entities.BookAuthor { AuthorId = authorId })));

            CreateMap<RequestAuthorJson, Domain.Entities.Author>();
            CreateMap<RequestPublisherJson, Domain.Entities.Publisher>();
            CreateMap<RequestBookAuthorJson, Domain.Entities.BookAuthor>();

            CreateMap<RequestRegisterBookRentalJson, BookRental>()
              .ForMember(dest => dest.RentalDate, opt => opt.MapFrom(src => DateTime.UtcNow))
              .ForMember(dest => dest.ExpectedReturnDate, opt => opt.MapFrom(src => src.DueDate))
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pendente"));




        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();

            CreateMap<Domain.Entities.Book, ResponseRegisteredBookJson>()
                 .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.Name))
                 .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Author.Name)));

            CreateMap<Domain.Entities.Author, ResponseRegisteredAuthorJson>();
            CreateMap<Domain.Entities.Publisher, ResponseRegisteredPublisherJson>();
            CreateMap<Domain.Entities.BookAuthor, ResponseRegisteredBookAuthorJson>();

            CreateMap<BookRental, ResponseRegisteredBookRentalJson>()
                .ForMember(dest => dest.RentalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.RentalDate, opt => opt.MapFrom(src => src.RentalDate))
                .ForMember(dest => dest.ExpectedReturnDate, opt => opt.MapFrom(src => src.ExpectedReturnDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ActualReturnDate == null ? "Em Andamento" : "Finalizado"))
                .ForMember(dest => dest.UserIdentifier, opt => opt.MapFrom(src => src.User.UserIdentifier)) 
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));



        }
    }
}
