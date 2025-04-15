using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;

namespace KBCoach.Models;

public class MiscModel : DbContext
{
    [Table("Transactions")]
    public class TransactionModel
    {
        [Key]
        public int transaction_id { get; set; }
        public string? sender_number { get; set; }
        public string? receiver_number { get; set; }
        public string? to_address { get; set; }
        public string? from_address { get; set; }
        public DateOnly sent_date { get; set; }
        [NotMapped]
        public string sent_date_vn => sent_date.ToString("dd/MM/yyyy");
        public string? product_code { get; set; }
        public string? product_type { get; set; }
        public double? product_price { get; set; }
        public int? qty { get; set; }
        public string? collector { get; set; }
        public double? total { get; set; }
    }
}