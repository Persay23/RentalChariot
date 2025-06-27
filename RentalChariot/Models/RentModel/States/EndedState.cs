namespace RentalChariot.RentManagement
{
    public class EndedState : IRentState
    {
        public string Name => "Ended";

        public bool isPaid => true;

        public IRentState Cancel()
        {
            return this; 
        }

        public IRentState EndRent()
        {
            return this;
        }

        public IRentState PayForRent()
        {
            return this;
        }

        public IRentState StartRent()
        {
            return this;
        }
    }
}