namespace RentalChariot.RentManagement
{
    public class ActiveState : IRentState
    {
        public string Name => "Active";

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
            return this;
        }

        public IRentState EndRent()
        {
            return new EndedState();
        }
    }
}
