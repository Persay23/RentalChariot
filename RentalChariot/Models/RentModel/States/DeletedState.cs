namespace RentalChariot.RentManagement
{
    public class DeletedState : IRentState
    {
        public string StateName => "Deleted";

        public bool isPaid => false;

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
