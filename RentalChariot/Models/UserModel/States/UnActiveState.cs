namespace RentalChariot.UserManagement
{
    public class UnActiveState : IUserState
    {
        public string StateName => "UnActive";

        public bool IsAbleToCreateRent => false;

        public IUserState Login()
        {
            return new ActiveState();
        }

        public IUserState LogOut()
        {
            return this;
        }
    }
}
