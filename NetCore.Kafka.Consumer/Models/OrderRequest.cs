using System.Collections.Generic;
namespace NetCore.Kafka.Consumer.Models
{
    public class OrderRequest
    {
        public IEnumerable<Item> Itens {get; set;}
    }
}