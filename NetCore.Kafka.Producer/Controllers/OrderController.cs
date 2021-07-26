using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCore.Kafka.Platform.Interfaces;
using NetCore.Kafka.Producer.Models;
using NetCore.Kafka.Messages.Topics;

namespace NetCore.Kafka.Producer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IKafkaProducer<string, OrderRequest> _kafkaProducer;

        public OrderController(IKafkaProducer<string, OrderRequest> kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderRequest request)
        {
            await _kafkaProducer.ProduceAsync(OrderTopics.CreateOrder, null, request);

            return Ok(request.Itens.Count());
        }
    }
}