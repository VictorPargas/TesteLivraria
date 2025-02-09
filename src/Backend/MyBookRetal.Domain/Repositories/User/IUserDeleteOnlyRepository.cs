namespace MyBookRental.Domain.Repositories.User
{
    public interface IUserDeleteOnlyRepository
    {
        Task DeleteAccount(Guid userIdentifier);
    }
}
