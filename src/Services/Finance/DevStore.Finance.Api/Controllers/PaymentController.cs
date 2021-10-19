using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Rebus.Bus;

namespace DevStore.Finance.Api.Controllers
{
    [Route("api/payment")]
    public class PaymentController : BaseController
    {
        private readonly IBus _bus;

        public PaymentController(IBus bus) : base()
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<string> GetAsync()
        {   
            return this.GetType().FullName;
        }
    }
}
