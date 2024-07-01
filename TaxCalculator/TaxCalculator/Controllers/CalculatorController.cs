using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Data.Entities;
using TaxCalculator.Infrastructure.Models;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly IMappingService _mappingService;
        private readonly ITaxService _taxService;

        public CalculatorController(IMappingService mappingService, ITaxService taxService)
        {
            _mappingService = mappingService;
            _taxService = taxService;
        }

        [HttpPost("Calculate")]
        public async Task<IActionResult> Calculate([FromBody] TaxPayerDTO taxPayerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var taxDto = await _taxService.CalculateTax(taxPayerDto);

                return Ok(taxDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}