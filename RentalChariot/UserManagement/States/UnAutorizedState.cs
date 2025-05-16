namespace RentalChariot.UserManagement
{
    public class UnAutorizedState : IUserState
    {
        public IUserState AccessResource()
        {
            return this;
        }
    }
}
