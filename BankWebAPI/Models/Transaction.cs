namespace BankWebAPI.Models
{
    public class Transaction
    {
        public string acctNo { get; set; }
        //operation 1: deposit
        //operation 2: withdrawal
        public int operation { get; set; }
        public double amount { get; set; }
    }
}
