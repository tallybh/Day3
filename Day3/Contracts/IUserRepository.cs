using Day3.Models;

namespace Day3.Contracts;

public interface IUserRepository
{
    Task<UserModel> GetByUserNameAndOasswordAsync(string userName, string password);
    Task<IEnumerable<UserModel>> GetAllAsync();
}
