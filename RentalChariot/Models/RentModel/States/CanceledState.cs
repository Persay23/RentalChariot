namespace RentalChariot.RentManagement
{
    public class CancelledState : IRentState
    {
        public string Name => "Cancelled";

        public bool isPaid => true;

        public IRentState Cancel()
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

        public IRentState EndRent()
        {
            return this;
        }
    }
}