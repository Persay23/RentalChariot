using RentalChariot.Models;
using RentalChariot.Repository;
using System;

namespace RentalChariot.Data.Repository
{
    public interface IRentRepository : IRepository<Rent>
    {
        Rent CreateRent(User user, Car car, DateTime Now, DateTime End);

        Rent GetById(int id);

        Rent GetFresh(int id);

        Task SaveRentAsync(Rent rent);

        void Update(Rent rent);
    }
}