namespace RentalChariot.CarManagement
{ 
    public class AvaliableState : ICarState
    {
        public string StateName => "Avaliable";

        public bool IsAvaliable => true;

        public ICarState Activate()
        {
            return this;
        }

        public ICarState Deactivate()
        {
            return new DeactivatedState();
        }

        public ICarState SendFromRent()
        {
            return this;
        }

        public ICarState SendToRent()
        {
            return new UnAvailableState();
        }


    }
}
