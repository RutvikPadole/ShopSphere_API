namespace ShopSphere_API.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId {  get; set; }
        public Order Order { get; set; }
        public Decimal Amount {  get; set; }
        public string status {  get; set; }
        public string PaymentMethod {  get; set; }

    }
}
