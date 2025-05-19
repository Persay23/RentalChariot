using System.ComponentModel.DataAnnotations;

namespace RentalChariot.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Brand { get; set; }

        [Required]
        [StringLength(16)]
        public string Model { get; set; }

        [Required]
        [StringLength(8)]
        public string Number { get; set; }

        [Required]
        public DateTime ProdYear { get; set; }

        [Required]
        [StringLength(16)]
        public string Color { get; set; }

        [Required]
        public short EngineVol { get; set; }

        [Required]
        public int Mileage { get; set; }


    }
}
