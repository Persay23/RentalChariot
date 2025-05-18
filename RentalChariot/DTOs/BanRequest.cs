namespace RentalChariot.DTOs
{
    public class BanRequest
    {
        public CurrentUser Admin { get; set; }
        public UserCreateRequest UserToBan { get; set; }
    }
}
