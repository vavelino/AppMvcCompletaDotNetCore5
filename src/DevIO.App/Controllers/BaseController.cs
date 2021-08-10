using DioIO.Business.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotifier _notifier;
        public BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool IsValidOperation()
        {
           return ! _notifier.IsThereNotification();
        }

    }
}
