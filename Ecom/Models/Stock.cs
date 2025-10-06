using System.Text.Json.Serialization;

namespace Ecom.Models
{
    public class Stock
    {
        
        public int Id { get; set; } 
        public int ProductId { get; set; }

       
        public Product? Product { get; set; }

        public int Quantity { get; set; } 
        public StockType Type { get; set; } // Stock In or Stock Out
        public DateTime Date { get; set; } = DateTime.Now;
        //public object InvoiceFile { get; internal set; }
    }
    public enum StockType
    {
        StockIn = 1,
        StockOut = 2
    }
}



