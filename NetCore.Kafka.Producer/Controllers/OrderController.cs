using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NetCore.Kafka.Producer.Models;

namespace NetCore.Kafka.Producer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] OrderRequest request)
        {
            return Ok(request.Itens.Count());
        }
    }
}