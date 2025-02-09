using MyBookRental.Domain.Entities;

namespace MyBookRental.Domain.Services.Storage
{
    public interface IBlobStorageService
    {
        Task Upload(User user, Stream file, string filename);
        Task<string> GetFileUrl(User user, string filename);
        Task Delete(User user, string filename);
        Task DeleteContainer(Guid userIdentifier);
    }
}
