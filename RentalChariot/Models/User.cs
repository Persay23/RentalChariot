using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalChariot.UserManagement;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string Password { get; set; }

    [Required]
    [MaxLength(100)]
    public string StateName { get; set; }
    [NotMapped]
    public IUserState _userState { get; set; }
    //Need to update Migration, I am so lazy :(
    [Required]
    [MaxLength(100)]
    public string Role { get; set; }
    [NotMapped]
    public IUserRole _userRole { get; set; }

    public User()
    {
        _userState = new UnActiveState();
        StateName = _userState.StateName;
        _userRole = new UserRole();
        Role = _userRole.RoleName;
    }
    public void Login()
    {
        var newState = _userState.Login();
        ChangeState(newState);
    }
    public void LogOut()
    {
        var newState = _userState.LogOut();
        ChangeState(newState);
    }
    public void Ban(User user)
    {
        if (user.Role == "Admin" || user.StateName == "Banned")
            return;
        var newState = _userRole.Ban();
        if (newState != null)
            user._userState = newState;
    }
    public void UnBan(User user)
    {

    }

    private void ChangeState(IUserState newState)
    {
        _userState = newState;
        StateName = newState.StateName;
    }

    //LATER EXTRACT _userState From StateName 
}

