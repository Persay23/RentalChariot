namespace RentalChariot.CarManagement
{
    public class UnAvailableState : ICarState
    {
        public string StateName => "UnAvaliable";

        public bool IsAvaliable => false;

        public ICarState Deactivate()
        {
            return this;
        }

        public ICarState Activate()
        {
            return this;
        }

        public ICarState SendFromRent()
        {
            return new AvaliableState();
        }

        public ICarState SendToRent()
        {
            return this;
        }
    }
}
