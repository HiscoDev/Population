using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer
{
    public class PopulationData
    {
        [Key]
        public int PopulationId { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1900, 2100, ErrorMessage = "Please enter year between (1900 - 2100)")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Total Male count is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter male count greater than 1")]
        public decimal TotalMale { get; set; }

        [Required(ErrorMessage = "Total Female count is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter positive female count greater than 1")]
        public decimal TotalFemale { get; set; }

        public decimal PopulationSummary { get; set; }
    }
}
