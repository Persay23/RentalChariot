﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RentalChariot.UserManagement;     

namespace RentalChariot.Models;

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
        UserState = UserState.Login();
        UpdateUser();
    }

    public void LogOut()
    {
        UserState = UserState.LogOut();
        UpdateUser();
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

    public bool IsAbleToCreateRent()
    {
        return UserState.IsAbleToCreateRent;
    }

    public void UpdateUser()
    {
        StateName = UserState.StateName; 
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
        user.UserState = UserRole.Ban();
        user.UpdateUser();

    }
    public void UnBan(User user)
    {
        if (user.StateName != "Banned")
            return;
        user.UserState = UserRole.UnBan();
        user.UpdateUser();
    }
}