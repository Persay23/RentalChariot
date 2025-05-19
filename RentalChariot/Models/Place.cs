using System.ComponentModel.DataAnnotations;

namespace RentalChariot.Models
{
    public class Place
    {
        [Key]
        public int PlaceId { get; set; }

        [Required]
        [StringLength(24)]
        public string Street { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8)] //Mb is better to add regular expression 
        public string Number { get; set; }

        [Required]
        [StringLength(24)]
        public string City { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)] //Mb is better to add regular expression 
        public string PostCode { get; set; }

        [StringLength(12)]
        public string? PhoneNumber { get; set; }

        [StringLength(40)]
        public string? Note { get; set; }
    }
}
