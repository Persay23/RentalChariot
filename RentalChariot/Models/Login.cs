using System.ComponentModel.DataAnnotations;

namespace RentalChariot.Models
{
    public class Login
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime LasLoginTime { get; set; }

    }
}
