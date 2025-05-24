namespace RentalChariot.UserManagement
{
    public interface IUserState
    {
        string StateName { get; }

        bool IsAbleToCreateRent { get; }

        IUserState Login();

        IUserState LogOut();
    }
}
