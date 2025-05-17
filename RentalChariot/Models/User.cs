using System.ComponentModel.DataAnnotations.Schema;

namespace RentalChariot.UserManagement;

public class User
{

    public int Id { get; set; }
    public required string name { get; set; }
    public required string password { get; set; }
    public UserRole Role { get; set; }
    [NotMapped]
    public IUserState _userState { get; set; }

    public User()
    {
        _userState = new UnAutorizedState();
    }

}

