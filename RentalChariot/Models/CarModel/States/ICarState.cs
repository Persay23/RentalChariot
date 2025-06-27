namespace RentalChariot.CarManagement
{
    public interface ICarState
    {
        string StateName { get; }

        bool IsAvaliable { get; }

        ICarState SendToRent();

        ICarState SendFromRent();

        ICarState Deactivate();

        ICarState Activate();
    }
}