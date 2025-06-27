namespace RentalChariot.UserManagement
{
    public class UserRole : IUserRole
    {
        public string RoleName => "User";

        public IUserState Ban()
        {
            return null;
        }

        public IUserState UnBan()
        {
            return null;
        }
    }
}