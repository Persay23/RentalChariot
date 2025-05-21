namespace RentalChariot.RentManagement
{
    public class UnPaidState : IRentState
    {
        public string StateName => "UnPaid";

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
            //Unreal
            return this;
        }

        public IRentState StartRent()
        {
            return this;
        }
    }
}
