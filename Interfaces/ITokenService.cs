using aprendizahem.Models;

namespace aprendizahem.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);

    }
}
