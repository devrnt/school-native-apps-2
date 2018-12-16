using System;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;

namespace CityApp.Services
{
    public static class AlertService
    {
        public static void Toast(string title, string content)
        {
            var visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = title
                        },
                        new AdaptiveText()
                        {
                            Text = content
                        }
                    }
                }
            };

            var toastContent = new ToastContent()
            {
                Visual = visual
            };

            try
            {
                var toast = new ToastNotification(toastContent.GetXml());
                ToastNotificationManager.CreateToastNotifier().Show(toast);
            }
            catch (Exception)
            {
                Console.WriteLine("Toast was not shown");
            }


        }

    }
}
