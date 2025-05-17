namespace RentalChariot.UserManagement
{
    public interface IUserState
    {
        string StateName { get; }

        IUserState Login();

        IUserState LogOut();
    }
}
