using System.ComponentModel.DataAnnotations;

namespace CodeFirstEFCoreDemo.Models
{
    public class ProductItem
    {
        [Key]
        public int ItemId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

    }
}
