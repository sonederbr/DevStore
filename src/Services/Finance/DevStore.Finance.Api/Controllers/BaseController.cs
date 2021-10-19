using System;

using Microsoft.AspNetCore.Mvc;

namespace DevStore.Finance.Api
{
    public abstract class BaseController : Controller
    {

        protected Guid ClientId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

        protected BaseController()
        {
            
        }
    }
}