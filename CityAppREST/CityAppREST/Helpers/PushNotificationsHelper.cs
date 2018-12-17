using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.NotificationHubs;

namespace CityAppREST.Helpers
{
    public class PushNotificationsHelper
    {
        private readonly NotificationHubClient _notificationHubClient;

        public PushNotificationsHelper()
        {
            _notificationHubClient = NotificationHubClient.CreateClientFromConnectionString(
                "Endpoint=sb://cityapppush.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=RXtY9k3J7Sfb8DKKJ9MHNOAAlTYee0tiNEOG9l5Md6U=",
                "CityAppPush");
        }

        public async Task<bool> SendNotification(string message)
        {
            var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">" + message + "</text></binding></visual></toast>";
            var result = await _notificationHubClient.SendWindowsNativeNotificationAsync(toast);

            if (result != null)
            {
                if (!(result.State == NotificationOutcomeState.Abandoned || result.State == NotificationOutcomeState.Unknown))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
