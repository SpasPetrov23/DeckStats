using Radzen;

namespace DeckStats.UI.Helpers
{
    public class CommonHelper
    {
        private readonly NotificationService _notifService;

        public CommonHelper(NotificationService notificationService)
        {
            _notifService = notificationService;
        }

        public void NotifyError(string text = "Нещо се обърка.<br/> Промените не бяха запазени.")
        {
            NotificationMessage notificationMsg = new()
            {
                Duration = 4000,
                CloseOnClick = true,
                Payload = DateTime.Now,
                Severity = NotificationSeverity.Error,
                Summary = text
            };
            _notifService.Notify(notificationMsg);
        }

        public void NotifySuccess(string text = "Успешна операция.")
        {
            NotificationMessage notificationMsg = new()
            {
                Duration = 4000,
                CloseOnClick = true,
                Payload = DateTime.Now,
                Severity = NotificationSeverity.Success,
                Summary = text
            };
            _notifService.Notify(notificationMsg);
        }
    }
}
