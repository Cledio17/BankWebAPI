namespace BankWebAPI_Admin.Models
{
    public class Transaction
    {
        public string transId { get; set; }
        public string fromId { get; set; }
        public string toId { get; set; }
        public double bal { get; set; }
    }
}
