using ShoppingCart.Core.Interfaces;

namespace ShoppingCart.Core.Entity;

    public abstract class BaseEntity<TEntity> : IBaseEntity where TEntity : IBaseEntity
    {
        protected BaseEntity()
        {
            if (Id == Guid.Empty)
                Id = Guid.NewGuid();
            
            if (string.IsNullOrEmpty(CreatedBy))
            {
                CreatedBy = "SYSTEM";
                CreatedAt = DateTime.Now;
            }

            IsDeleted ??= false;
        }

        public Guid Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
        public string CreatedBy { get; init; }
        public string? UpdatedBy { get; init; }
        public bool? IsDeleted { get; set; }
    }