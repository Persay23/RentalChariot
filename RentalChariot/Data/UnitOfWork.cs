using RentalChariot.Data.Repository;
using RentalChariot.Db;
using RentalChariot.LoginModel;
using RentalChariot.Models.UserModel.Services;

namespace RentalChariot.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RentalChariotDbContext _context;

        public UnitOfWork(RentalChariotDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);        
            LoginTokens = new LoginRepository(_context);
            Cars = new CarRepository(_context);
            Rents = new RentRepository(_context);
        }

        public IUserRepository Users { get; private set; }

        public ILoginRepository LoginTokens { get; private set; }

        public ICarRepository Cars { get; private set; }

        public IRentRepository Rents { get; private set; }  

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }


    }
}
