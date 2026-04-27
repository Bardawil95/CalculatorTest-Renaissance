using System.ComponentModel.DataAnnotations;

namespace Calculator.Api.Models
{
    public class CalculationRequest
    {
        [Required(ErrorMessage = "Start value is required")]
        public int Start { get; set; }

        [Required(ErrorMessage = "Amount value is required")]
        public int Amount { get; set; }
    }
}