namespace RentalChariot.RentManagement
{ 
    public interface IRentState
    {
        string Name { get; }

        bool isPaid { get; }


        IRentState PayForRent();

        IRentState Cancel();

        IRentState StartRent();

        IRentState EndRent();
    }
}
