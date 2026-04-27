using Microsoft.AspNetCore.Mvc;
using Calculator.Core;
using Calculator.Api.Models;

namespace Calculator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ISimpleCalculator _calculator;
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ISimpleCalculator calculator, ILogger<CalculatorController> logger)
        {
            _calculator = calculator;
            _logger = logger;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] CalculationRequest request)
        {
            // ERROR CASE 1: Invalid input type
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid input type for Add operation");
                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid input type",
                    Details = "Both Start and Amount must be valid integers"
                });
            }

            try
            {
                _logger.LogInformation("Add operation: {Start} + {Amount}", request.Start, request.Amount);

                int result = _calculator.Add(request.Start, request.Amount);

                return Ok(new { result });
            }
            // ERROR CASE 2: Arithmetic overflow
            catch (OverflowException ex)
            {
                _logger.LogWarning("Overflow in Add operation: {Message}", ex.Message);
                return BadRequest(new ErrorResponse
                {
                    Message = "Arithmetic overflow",
                    Details = "The calculation result exceeds the valid integer range"
                });
            }
            // ERROR CASE 3: Unexpected server errors
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in Add operation");
                return StatusCode(500, new ErrorResponse
                {
                    Message = "Internal server error",
                    Details = "An unexpected error occurred while processing your request"
                });
            }
        }

        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] CalculationRequest request)
        {
            // ERROR CASE 1: Invalid input type
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid input type for Subtract operation");
                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid input type",
                    Details = "Both Start and Amount must be valid integers"
                });
            }

            try
            {
                _logger.LogInformation("Subtract operation: {Start} - {Amount}", request.Start, request.Amount);

                int result = _calculator.Subtract(request.Start, request.Amount);

                return Ok(new { result });
            }
            // ERROR CASE 2: Arithmetic overflow/underflow
            catch (OverflowException ex)
            {
                _logger.LogWarning("Overflow in Subtract operation: {Message}", ex.Message);
                return BadRequest(new ErrorResponse
                {
                    Message = "Arithmetic overflow",
                    Details = "The calculation result exceeds the valid integer range"
                });
            }
            // ERROR CASE 3: Unexpected server errors
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in Subtract operation");
                return StatusCode(500, new ErrorResponse
                {
                    Message = "Internal server error",
                    Details = "An unexpected error occurred while processing your request"
                });
            }
        }
    }
}