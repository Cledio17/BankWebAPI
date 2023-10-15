namespace BankWebAPI_JS.Models
{
    public class Transaction
    {
        public string transId { get; set; }
        public string fromId { get; set; }
        public string toId { get; set; }
        public double bal { get; set; }
        public DateTime transactionDate { get; set; }
        public string description { get; set; }
    }
}
