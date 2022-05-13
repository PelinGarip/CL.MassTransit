using CL.MassTransit.Core.Enums;
using CL.MassTransit.Services.Abstract;
using CL.MassTransit.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CL.MassTransit.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly INotificationService _notificationService;

        public IndexModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [BindProperty]
        public NotificationModel NotificationModel { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = _notificationService.Send(NotificationTypes.Email, NotificationModel);
            return RedirectToPage("./Index");
        }
    }
}
