namespace RentalChariot.UserManagement.Roles
{
    public class AdminRole : IUserRole
    {
        public string RoleName => "Admin";

        public IUserState Ban()
        {
            return new BannedState();
        }

        public IUserState UnBan()
        {
            return new UnActiveState(); ;
        }
    }
}
