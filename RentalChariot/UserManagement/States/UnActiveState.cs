namespace RentalChariot.UserManagement
{
    public class UnActiveState : IUserState
    {
        public string StateName => "UnActive";

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
