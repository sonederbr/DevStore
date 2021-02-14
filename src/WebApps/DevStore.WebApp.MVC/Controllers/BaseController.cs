using System;

using DevStore.Core.Bus;

using Microsoft.AspNetCore.Mvc;

namespace DevStore.WebApp.MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly IMediatorHandler _mediatorHandler;

        protected Guid ClientId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

        protected BaseController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }
    }
}