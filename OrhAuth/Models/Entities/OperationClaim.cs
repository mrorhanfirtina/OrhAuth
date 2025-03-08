using OrhAuth.Models.Entities.Base;
using System.Collections.Generic;

namespace OrhAuth.Models.Entities
{
    public class OperationClaim : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }

        public OperationClaim()
        {
            UserOperationClaims = new HashSet<UserOperationClaim>();
        }
    }
}
