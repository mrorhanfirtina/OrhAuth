using System;

namespace OrhAuth.Attributes
{
    /// <summary>
    /// Indicates that the property should be included as a claim in the generated JWT token
    /// when using an extended user model in OrhAuth.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AddToClaimAttribute : Attribute
    {
        /// <summary>
        /// The name of the claim to be used in the JWT.
        /// If not specified, the property name will be used by default.
        /// </summary>
        public string ClaimName { get; set; }

        /// <summary>
        /// The prefix to be applied to the claim name.
        /// Default value is "ext_".
        /// </summary>
        public string Prefix { get; set; } = "ext_";

        /// <summary>
        /// Initializes a new instance of the <see cref="AddToClaimAttribute"/> class.
        /// </summary>
        public AddToClaimAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddToClaimAttribute"/> class with a specified claim name.
        /// </summary>
        /// <param name="claimName">The claim name to use in the JWT.</param>
        public AddToClaimAttribute(string claimName)
        {
            ClaimName = claimName;
        }
    }
}
