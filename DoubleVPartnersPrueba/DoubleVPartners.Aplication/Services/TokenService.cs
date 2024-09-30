using AutoMapper;
using DoubleVPartners.Aplication.Dtos;
using DoubleVPartners.Aplication.Interfaces;
using DoubleVPartners.Domain.Entities;
using DoubleVPartners.Infrastructure.DataAccess.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DoubleVPartners.Aplication.Services
{
    public class TokenService : ITokenService
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IOptionsSnapshot<JwtDto> _jwtOption;

        public TokenService(IOptionsSnapshot<JwtDto> jwtOption, IRepositoryBase<User> userRepository, IMapper mapper)
        {
            _jwtOption = jwtOption;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public (bool status, string jwt, int idRole) GenerateToken(InfoTokenDto entity)
        {
            int idRole = IsUser(entity);
            if (idRole > 0)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Value.Key));
                var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    null,
                    null,
                    null,
                    expires: DateTime.UtcNow.AddDays(5),
                    signingCredentials: singIn
                    );

                return (true, new JwtSecurityTokenHandler().WriteToken(token), idRole);
            }
            else
            {
                return (false, string.Empty, 0);
            }
        }

        public int IsUser(InfoTokenDto entity)
        {
            UserDto securityUser = GetByUsuario(entity.Email);
            if (securityUser != null)
            {
                if (securityUser.IsActive == true)
                {
                    if(securityUser.Email == entity.Email && securityUser.Password == entity.Password)
                        return securityUser.IdRole; 
                }
            }
            return 0;
        }

        public UserDto GetByUsuario(string email)
        {
            IQueryable<User> result = _userRepository.GetAll().Result;

            return _mapper.Map<UserDto>(result.Where(x => x.Email == email).FirstOrDefault());
        }
    }
}
