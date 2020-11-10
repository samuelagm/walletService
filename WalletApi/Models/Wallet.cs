namespace WalletApi.Models
{
    public class Wallet
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public long Balance { get; set; }
        public bool Deleted { get; set; }
        public bool Frozen { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public long Timestamp { get; set; }
    }
}