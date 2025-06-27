namespace RentalChariot.RentManagement
{
    public class UnPaidState : IRentState
    {
        public string Name => "UnPaid";

        public bool isPaid => false;

        public IRentState Cancel()
        {
            return new DeletedState();
        }

        public IRentState PayForRent()
        {
            return new UnActiveState();
        }

        public IRentState EndRent()
        {
            return this;
        }

        public IRentState StartRent()
        {
            return this;
        }
    }
}