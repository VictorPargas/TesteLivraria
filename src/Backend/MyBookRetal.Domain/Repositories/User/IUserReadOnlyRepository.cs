namespace MyBookRental.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWithEmail(string email);

        Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);

        Task<IEnumerable<Entities.User>> GetAllUsersAsync();
        Task<Entities.User?> GetUserByIdentifierAsync(Guid userIdentifier);

        public Task<Entities.User?> GetByEmailAndPassword(string email, string password);

        public Task<Entities.User> GetByEmail(string email);
    }
}
