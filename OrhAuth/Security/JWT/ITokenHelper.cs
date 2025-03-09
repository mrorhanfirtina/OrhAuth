using OrhAuth.Models.Dtos;
using OrhAuth.Models.Entities;
using System.Collections.Generic;

namespace OrhAuth.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
        string CreateRefreshToken();
        bool ValidateToken(string token);
    }
}
