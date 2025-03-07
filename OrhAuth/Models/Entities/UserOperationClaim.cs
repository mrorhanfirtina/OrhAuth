using OrhAuth.Models.Entities.Base;

namespace OrhAuth.Models.Entities
{
    public class UserOperationClaim : EntityBase
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public virtual User User { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }
    }
}
