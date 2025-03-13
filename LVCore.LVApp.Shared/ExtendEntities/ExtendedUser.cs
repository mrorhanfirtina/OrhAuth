using OrhAuth.Attributes;
using OrhAuth.Models.Entities;

namespace LVCore.LVApp.Shared.ExtendEntities
{
    public class ExtendedUser : User
    {

        [ExtendUser(isUnique: true)]
        [AddToClaim("lvuser_id")]
        public int LVUserId { get; set; }
        [ExtendUser(maxLength: 100, isRequired: true, defaultValue: "aykut")]
        public string LVUserLogin { get; set; }
        [ExtendUser]
        public string LVPassword { get; set; }
    }
}
