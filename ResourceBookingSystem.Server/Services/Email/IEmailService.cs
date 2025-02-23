namespace ResourceBookingSystem.Server.Services.Email
{
    public interface IEmailService
    {
        public void SendEmail(int bookingId);
    }
}
