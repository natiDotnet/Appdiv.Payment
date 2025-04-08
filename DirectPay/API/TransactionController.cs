using DirectPay.Domain.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        public TransactionController()
        {
        }

        [HttpPost("initialize")]
        public IActionResult Post([FromBody] Transaction transaction)
        {
            return Ok(transaction);
        }
    }
}
