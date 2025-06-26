namespace RentalChariot.DTOs
{
    public class UnBanRequest
    {
        public CurrentUser Admin { get; set; }

        public ExternalUser UserToUnBan { get; set; }
    }
}