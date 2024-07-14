using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BiteWebAPI.Entities
{
    public class ShoppingCartItem
    {
        [Key]
        public int ShoppingCartItemId { get; set; }
        public Item Item { get; set; } = default!;
        public int Amount { get; set; }
        public string? ShoppingCartId { get; set; }
    }

}
