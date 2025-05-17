namespace RentalChariot.UserManagement
{
    public class BannedState : IUserState
    {
        public string StateName => "Banned";
        public IUserState Login()
        {
            return this;
        }
        public IUserState LogOut() {
            return this;
        }
    }
}
