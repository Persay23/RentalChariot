using RentalChariot.UserManagement.Roles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalChariot.UserManagement;

//USER it's like a CLIENT , later add FirstName, LastName, email
public class User
{
    
    [Key]
    public int UserId { get; set; }
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
    public IUserState UserState { get; set; }
    [NotMapped]
    public IUserRole UserRole { get; set; }

    public User()
    {
        UserState = new UnActiveState();
        StateName = UserState.StateName;
        UserRole = new UserRole();
    }
    public void Login()
    {
        var newState = UserState.Login();
        ChangeState(newState);
    }
    public void LogOut()
    {
        var newState = UserState.LogOut();
        ChangeState(newState);
    }

    public void ChangeState(IUserState newState)
    {
        UserState = newState;
        StateName = newState.StateName;
    }
    public void InitializeUserState()
    {
        UserState = StateName switch
        {
            "UnActive" => new UnActiveState(),
            "Active" => new ActiveState(),
            "Banned" => new BannedState(),
        };
    }
}
public class Admin : User
{
    public Admin() : base()
    {
        UserState = new UnActiveState();
        StateName = UserState.StateName;

        UserRole = new AdminRole();
    }
    public void Ban(User user)
    {
        //UserRole.Ban() return a IUserState not IUserRole
        var newState = UserRole.Ban();
        if (newState != null)
            user.ChangeState(newState);
    }
    public void UnBan(User user)
    {
        if (user.StateName != "Banned")
            return;
        var newState = UserRole.UnBan();
        user.ChangeState(newState);
    }
  }