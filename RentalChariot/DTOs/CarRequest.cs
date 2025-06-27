using Microsoft.AspNetCore.Mvc;
using RentalChariot.CarManagement;
using System.ComponentModel.DataAnnotations;

namespace RentalChariot.DTOs
{
    public class CarRequest
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Number { get; set; }

        public DateTime ProdYear { get; set; }

        public string Color { get; set; }

        public short EngineVol { get; set; }

        public int Mileage { get; set; }
    }
}