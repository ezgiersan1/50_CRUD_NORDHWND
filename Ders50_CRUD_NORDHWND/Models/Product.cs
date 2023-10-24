using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ders50_CRUD_NORDHWND.Models
{
    public class Product
    {
        private decimal _UnitPrice;

        public Product(string productName)
        {
            ProductName = productName;
        }

        public Product()
        {
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //primary key = yes .... identity= 1-1
        [DisplayName("ID")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Ürün adı girmek zorunludur *")]
        [DisplayName("ÜRÜN ADI")]// .cshtml sayfasında form da labelda görünecek kısım
        [MaxLength(40)]
        [MinLength(3)]
        //RegularExpression
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Fiyat girmek zorunludur *")]
        [DisplayName("FİYAT")] // .cshtml sayfasında form da labelda görünecek

        //encapsulation = kapsülleme

        private decimal Unitprice { get; set; } //direkt erişemem
        public decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = Math.Abs(value); }

        }
    }
}
