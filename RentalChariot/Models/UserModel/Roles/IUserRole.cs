namespace RentalChariot.UserManagement
{
    public interface IUserRole
    {
        string RoleName { get; }

        IUserState Ban();

        IUserState UnBan();
    }
}