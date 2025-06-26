using RentalChariot.RentManagement;

namespace RentalChariot.Models.RentModel
{
    public static class RentStateFactory
    {
        public static IRentState Update(string stateName)
        {
        Console.WriteLine(stateName);
            return stateName switch {
                "UnPaid" => new UnPaidState(),
                "Active" => new ActiveState(),
                "UnActive" => new UnActiveState(),
                "Ended" => new EndedState(),
                "Canceled" => new CancelledState(),
                "Deleted" => new DeletedState(),
            }; 
        }
    }
}