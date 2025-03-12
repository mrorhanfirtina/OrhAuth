using OrhAuth.Models.Dtos;
using OrhAuth.Models.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace OrhAuth.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);

        // Yeni metot: Ek claim'leri parametre olarak alan overload
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims, IEnumerable<Claim> additionalClaims);

        // Alternatif metot: Dictionary olarak ek claim'leri alan overload
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims, Dictionary<string, string> additionalClaims);

        string CreateRefreshToken();
        bool ValidateToken(string token);
    }
}
