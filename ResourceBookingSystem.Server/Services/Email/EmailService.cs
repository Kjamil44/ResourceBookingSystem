using System.Diagnostics;

namespace ResourceBookingSystem.Server.Services.Email
{
    public class EmailService : IEmailService
    {
        public EmailService() { }

        public void SendEmail(int bookingId)
        {
            Debug.WriteLine($"EMAIL SENT TO admin@admin.com FOR CREATED BOOKING WITH ID {bookingId}");
        }
    }
}
