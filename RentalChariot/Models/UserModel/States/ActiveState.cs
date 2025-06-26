namespace RentalChariot.UserManagement
{
    public class ActiveState : IUserState
    {
        public string StateName => "Active";

        public bool IsAbleToCreateRent => true;

        public IUserState Login()
        {
            return this;
        }

        public IUserState LogOut()
        {
            return new UnActiveState();
        }
    }
}