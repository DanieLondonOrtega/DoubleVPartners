using AutoMapper;
using DoubleVPartners.Aplication.Dtos;
using DoubleVPartners.Aplication.Interfaces;
using DoubleVPartners.Domain.Entities;
using DoubleVPartners.Infrastructure.DataAccess.Repository;

namespace DoubleVPartners.Aplication.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IMapper _mapper;
        public UserService(IRepositoryBase<User> userRepository, IMapper mapper)
        {
             _mapper = mapper;
            _userRepository = userRepository;
        }
        public bool Delete(int id)
        {
            return _userRepository.Delete(id);
        }

        public async Task<UserDto> Get(int id)
        {
            var result = await _userRepository.GetByProperty(x => x.IdUser == id);
            return _mapper.Map<UserDto>(result);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var result = await _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDto>>(result);
        }

        public bool Post(UserDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(String.Format(Constants.Constants.EntityIsRequerid, "User"));

            var obj = _mapper.Map<User>(entity);
            return _userRepository.Add(obj);
        }

        public bool Put(UserDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(String.Format(Constants.Constants.EntityIsRequerid, "User"));

            var obj = _mapper.Map<User>(entity);
            return _userRepository.Update(obj);
        }
        public async Task<UserDto> GetByEmail(string email)
        {
            var result = await _userRepository.GetByProperty(x => x.Email == email);
            return _mapper.Map<UserDto>(result);
        }
    }
}
