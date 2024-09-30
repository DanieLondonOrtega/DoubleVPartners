using DoubleVPartners.Aplication.Dtos;

namespace DoubleVPartners.Aplication.Interfaces
{
    public interface ITokenService
    {
        public (bool status, string jwt, int idRole) GenerateToken(InfoTokenDto entity);
        public int IsUser(InfoTokenDto entity);
        UserDto GetByUsuario(string usuario);
    }
}
