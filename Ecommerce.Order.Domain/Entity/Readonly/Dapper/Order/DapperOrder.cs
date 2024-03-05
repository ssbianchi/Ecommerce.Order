namespace Ecommerce.Order.Domain.Entity.Readonly.Dapper.Order
{
    public class DapperOrder
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int ProductId { get; set; }
        public int Qtd { get; set; }
        public double Price { get; set; }
    }
}
