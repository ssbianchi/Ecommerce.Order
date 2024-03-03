namespace Ecommerce.Order.CrossCutting.Entity
{
    public abstract class Entity<T>
    {
        public virtual T Id { get; set; }
    }
}
