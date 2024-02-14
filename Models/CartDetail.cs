using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        [Required]
        public int ElectronicId { get; set; }
        public int Quantity { get; set; }
        public Electronic Electronic { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public double UnitPrice { get; set; }
    }
}
