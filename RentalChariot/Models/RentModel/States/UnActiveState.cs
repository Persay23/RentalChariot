namespace RentalChariot.RentManagement
{
    public class UnActiveState : IRentState
    {
        public string Name => "UnActive";

        public bool isPaid => true;

        public IRentState Cancel()
        {
            return new CancelledState();
        }

        public IRentState PayForRent()
        {
            return this;
        }

        public IRentState StartRent()
        {
            return new ActiveState();
        }

        public IRentState EndRent()
        {
            return this;
        }
    }
}
