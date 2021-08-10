using DioIO.Business.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevIO.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotifier _notifier;

        public SummaryViewComponent(INotifier notifier)
        {
            _notifier = notifier;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //FromResult Garatir compatibilidade
            var notifications = await Task.FromResult(_notifier.GetNotifications());
            notifications.ForEach(c =>
            ViewData.ModelState.AddModelError(string.Empty, c.Message));
            // Trata os erros de negócio como erros de preenchimento dos campos
            return View();
        }
    }
}
