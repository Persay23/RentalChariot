namespace RentalChariot.DTOs
{
    public class BanRequest
    {
        public CurrentUser Admin { get; set; }

        public ExternalUser UserToBan { get; set; }
    }
}