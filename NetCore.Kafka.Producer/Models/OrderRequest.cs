using System.Collections.Generic;
namespace NetCore.Kafka.Producer.Models
{
    public class OrderRequest
    {
        public IEnumerable<Item> Itens {get; set;}
    }
}