using OrhAuth.Models.Entities.Base;
using System;

namespace OrhAuth.Models.Entities
{
    public class RefreshToken : EntityBase
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public DateTime? RevokedDate { get; set; }
        public string ReplacedByToken { get; set; }

        public virtual User User { get; set; }
    }
}
