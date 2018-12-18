using System;
using System.Threading.Tasks;
using CityApp.DataModel;
using Windows.Foundation;

namespace CityApp.Services
{
    public class CalendarService
    {
        public async Task CreateAppointmentAsync(Event @event)
        {
            //  Maak afspraak voor toe te voegen in de agenda
            var appointment = new Windows.ApplicationModel.Appointments.Appointment();
            appointment.Subject = @event.Title;
            appointment.StartTime = new DateTimeOffset(@event.Date);
            var appointmentId = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowAddAppointmentAsync(appointment, new Rect());

            if (appointmentId != String.Empty)
            {
                AlertService.Toast("Afspraak gemaakt", $"Afspraak {@event.Title} toegevoegd aan uw agenda");
            }
            else
            {
                // Er ging iets fout
            }
        }
    }
}
