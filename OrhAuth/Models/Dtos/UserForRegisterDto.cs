using System.Collections.Generic;

namespace OrhAuth.Models.Dtos
{
    public class UserForRegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LocalityId { get; set; }

        public ICollection<UserOperationClaimDto> UserOperationClaims { get; set; }

        public UserForRegisterDto()
        {
            UserOperationClaims = new List<UserOperationClaimDto>();
        }
    }
}
