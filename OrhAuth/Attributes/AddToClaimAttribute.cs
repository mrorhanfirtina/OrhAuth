using System;

namespace OrhAuth.Attributes
{
    /// <summary>
    /// Bu öznitelik, ExtendedUser sınıfındaki özelliklerin JWT token claim'lerine eklenmesi gerektiğini belirtir.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AddToClaimAttribute : Attribute
    {
        /// <summary>
        /// Claim içinde kullanılacak isim (belirtilmezse property adı kullanılır)
        /// </summary>
        public string ClaimName { get; set; }

        /// <summary>
        /// Claim öneki (varsayılan: "ext_")
        /// </summary>
        public string Prefix { get; set; } = "ext_";

        public AddToClaimAttribute()
        {
        }

        public AddToClaimAttribute(string claimName)
        {
            ClaimName = claimName;
        }
    }
}
