using Microsoft.AspNetCore.Identity;

namespace FomoGym.Interfaces;

public interface ITokenService {
    string CreateToken(IdentityUser user);
}