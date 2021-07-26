using System;
namespace PopulationAPI_nh.Models
{
    public class Population
    {
        public int ID { get; set; }
        public string Country { get; set; }
        public int Year { get; set; }
        public float TotalMale { get; set; }
        public float TotalFemale { get; set; }
    }
}
