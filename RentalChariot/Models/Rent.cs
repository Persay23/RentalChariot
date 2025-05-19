using System.ComponentModel.DataAnnotations;

namespace RentalChariot.Models
{
    public class Rent
    {
        [Key]
        public int RentId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CarId { get; set; }
        //I dont think we need RentEmployeeID and ReturnEmployeeID
        [Required]
        public int RentPlaceId { get; set; }

        public int? ReturnPlaceId { get; set; }

        [Required]
        public DateTime RentDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        //public decimal? Deposit {  get; set; } //To do add maxvalue 999999.99 minvalue 0

        //public decimal? UnitPrice { get; set; } //To do add maxvalue 999999.99 minvalue 0


    }
}
