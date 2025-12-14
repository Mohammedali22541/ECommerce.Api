namespace ECommerce.Shared.Dtos.OrderDtos
{
    public class OrderItemDto
    {
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}