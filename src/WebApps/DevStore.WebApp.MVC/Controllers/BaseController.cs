using System;
using System.Collections.Generic;
using System.Linq;

using DevStore.Core.Communication.Bus;
using DevStore.Core.Messages.CommonMessages.Notifications;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace DevStore.WebApp.MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IBusHandler _mediatorHandler;

        protected Guid ClientId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

        protected BaseController(INotificationHandler<DomainNotification> notifications,
                                 IBusHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        protected bool IsValidOperation()
        {
            return !_notifications.HasNotification();
        }

        protected IEnumerable<string> GetErrors()
        {
            return _notifications.GetNotifications().Select(p => p.Value).ToList();
        }

        protected void SendError(string code, string message)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(code, message));
        }
    }
}