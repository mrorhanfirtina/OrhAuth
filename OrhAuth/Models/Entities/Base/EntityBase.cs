using System;

namespace OrhAuth.Models.Entities.Base
{
    /// <summary>
    /// Represents the base entity class for all persistent entities in the system.
    /// Provides common auditing and soft delete properties.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Gets or sets the primary key identifier of the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the creation timestamp of the entity.
        /// Default value is set to the current date and time when the entity is instantiated.
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the timestamp of the last update to the entity.
        /// Nullable; will be null if the entity has not been updated.
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is logically deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the entity was logically deleted.
        /// Nullable; will be null if the entity is not deleted.
        /// </summary>
        public DateTime? DeletedDate { get; set; }
    }
}
