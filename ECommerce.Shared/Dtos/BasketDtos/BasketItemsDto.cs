using System.ComponentModel.DataAnnotations;

namespace ECommerce.Shared.Dtos.BasketDtos
{
    public record BasketItemsDto(int Id, string? Name, string? PictureUrl ,decimal Price , [Range(0,100)] int Quantity);
    
}