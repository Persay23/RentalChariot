namespace RentalChariot.RentManagement
{ 
    public interface IRentState
    {
        string StateName { get; }

        bool isPaid { get; }


        IRentState PayForRent();

        IRentState Cancel();

        IRentState StartRent();

        IRentState EndRent();
    }
}
