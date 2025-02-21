using Company.Domain.DTOs;
using Company.Domain.UseCases.CreateCompany;
using Company.Domain.UseCases.GetAllCompanies;
using Company.Domain.UseCases.GetCompanyById;
using Company.Domain.UseCases.GetCompanyByIsin;
using Company.Domain.UseCases.UpdateCompany;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Models.Company>>> GetAllCompanies()
        {
            var query = new GetAllCompaniesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain.Models.Company>> GetCompanyById(int id)
        {
            var query = new GetCompanyByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET: api/companies/isin/US0378331005
        [HttpGet("isin/{isin}")]
        public async Task<ActionResult<Domain.Models.Company>> GetCompanyByIsin(string isin)
        {
            var query = new GetCompanyByIsinQuery { Isin = isin };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/companies
        [HttpPost]
        public async Task<ActionResult<int>> CreateCompany([FromBody] CompanyDto companyDto)
        {
            var command = new CreateCompanyCommand
            {
                Name = companyDto.Name,
                StockTicker = companyDto.StockTicker,
                Exchange = companyDto.Exchange,
                Isin = companyDto.Isin,
                Website = companyDto.Website
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCompanyById), new { id = result }, result);
        }

        // PUT: api/companies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] CompanyDto companyDto)
        {
            var command = new UpdateCompanyCommand
            {
                Id = id,
                Name = companyDto.Name,
                StockTicker = companyDto.StockTicker,
                Exchange = companyDto.Exchange,
                Website = companyDto.Website
            };

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
