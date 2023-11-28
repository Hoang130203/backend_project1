using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace project1_backend.Models.Custom
{
    public class InfoProduct
    {
        [Key]
        public int ProductId {  get; set; } 
        public string ProductName { get; set; } = null!;
        public int Price {  get; set; }
        public string? Description {get;set; }
        public string ? Type { get; set; }
        public int Quantity { get; set; }
        public string? Color { get; set; } = null!;
        public string? Linkimg { get; set; }
    }
}
