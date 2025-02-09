using AutoMapper;
using MyBookRental.Communication.Requests;
using MyBookRental.Communication.Responses;
using MyBookRental.Domain.Repositories.Publisher;
using MyBookRental.Domain.Repositories;

namespace MyBookRental.Application.UseCase.Publisher
{
    public class RegisterPublisherUseCase : IRegisterPublisherUseCase
    {
        private readonly IPublisherWriteOnlyRepository _writeOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterPublisherUseCase(
            IPublisherWriteOnlyRepository writeOnlyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseRegisteredPublisherJson> Execute(RequestPublisherJson request)
        {
            var publisher = _mapper.Map<Domain.Entities.Publisher>(request);
            await _writeOnlyRepository.Add(publisher);
            await _unitOfWork.Commit();

            return _mapper.Map<ResponseRegisteredPublisherJson>(publisher);
        }
    }
}
