using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Order.CrossCutting.Entity
{
    public abstract class OperationEntity<T> : Entity<T>
    {
        [NotMapped]
        public virtual int? OperationId { get; set; }
    }
}
