using aprendizahem.Data;
using aprendizahem.Dtos.Stock;
using aprendizahem.Helpers;
using aprendizahem.Interfaces;
using aprendizahem.Mappers;
using aprendizahem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace aprendizahem.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockrepo;
        public StockController(ApplicationDbContext context, IStockRepository stockRepo)
        {
            _stockrepo = stockRepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockrepo.GetAllAsync(query);
                
            var stockDto = stocks.Select(s => s.ToStockDto()).ToList();
            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockrepo.GetByIdAsync(id);
            if (stocks == null)
            {
                return NotFound();
            }

            return Ok(stocks.ToStockDto());

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var StockModel = stockDto.ToStockFromCreateDTO();
            await _stockrepo.CreateAsync(StockModel);

            return CreatedAtAction(nameof(GetById), new {id = StockModel.Id}, StockModel.ToStockDto());
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockrepo.UpdateAsync(id, stockDto);

           if (stockModel == null) { return NotFound(); }

            return Ok(stockModel.ToStockDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleteStock = await _stockrepo.DeleteAsync(id);
            if (deleteStock == null) { return NotFound(); }
          
            return NoContent();
            
         }
    }
}
