namespace RentalChariot.CarManagement
{
    public class DeactivatedState : ICarState
    {
        public string StateName => "Deactivated";

        public bool IsAvaliable => false;

        public ICarState Activate()
        {
            return new AvaliableState();
        }

        public ICarState Deactivate()
        {
            return this;
        }

        public ICarState SendFromRent()
        {
            return this;
        }

        public ICarState SendToRent()
        {
            return this;
        }
    }
}
