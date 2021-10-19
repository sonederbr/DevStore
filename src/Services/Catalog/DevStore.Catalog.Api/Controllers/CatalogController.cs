
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Rebus.Bus;

namespace DevStore.Catalog.Api.Controllers
{
    [Route("api/catalog")]
    public class CatalogController : BaseController
    {
        private readonly IBus _bus;

        public CatalogController(IBus bus) : base()
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
