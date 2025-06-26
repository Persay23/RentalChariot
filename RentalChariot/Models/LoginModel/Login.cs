using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalChariot.Models
{
    public class LoginToken
    {
        [Key]
        public int LoginId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime LoginTime { get; set; }

        private LoginToken(int userId) {
            UserId = userId;
            LoginTime = DateTime.Now;
            Token = GenerateToken();
        }

        public static LoginToken Create(int userId)
        {
            return new LoginToken(userId);
        }    

        private string GenerateToken()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; // TODO consider using a more secure token generation method
            StringBuilder result = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 25; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }
    }
}