using System.Collections.Generic;
using System.Security.Claims;

namespace LS.Domain.Usuarios
{
    public interface IUser
    {
        string Name { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
