using DoubleVPartners.Aplication.Dtos;

namespace DoubleVPartners.Aplication.Interfaces
{
    public interface IUserService
    {
        bool Post(UserDto entity);
        bool Put(UserDto entity);
        bool Delete(int id);
        Task<UserDto> Get(int id);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetByEmail(string email);
    }
}
