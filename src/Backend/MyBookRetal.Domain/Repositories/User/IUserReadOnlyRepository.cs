namespace MyBookRental.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWithEmail(string email);

        Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);

        public Task<Entities.User?> GetByEmailAndPassword(string email, string password);

        public Task<Entities.User> GetByEmail(string email);
    }
}
