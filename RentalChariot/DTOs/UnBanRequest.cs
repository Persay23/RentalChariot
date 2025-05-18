namespace RentalChariot.DTOs
{
    public class UnBanRequest
    {
        public CurrentUser Admin { get; set; }
        public UserCreateRequest UserToUnBan { get; set; }
    }
}
