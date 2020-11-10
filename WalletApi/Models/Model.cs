using System;

namespace WalletApi.Models
{
    public interface IModel
    {
        public System.Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
        long Timestamp { get; set; }
    }

    public class Model : IModel
    {
        public System.Guid Id { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public long Timestamp { get; set; }
    }
}