using aprendizahem.Data;
using aprendizahem.Dtos.Stock;
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
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockrepo.GetAllAsync();
                
            var stockDto = stocks.Select(s => s.ToStockDto());
            return Ok(stockDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
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
            var StockModel = stockDto.ToStockFromCreateDTO();
            await _stockrepo.CreateAsync(StockModel);

            return CreatedAtAction(nameof(GetById), new {id = StockModel.Id}, StockModel.ToStockDto());
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            var stockModel = await _stockrepo.UpdateAsync(id, stockDto);

           if (stockModel == null) { return NotFound(); }

            return Ok(stockModel.ToStockDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleteStock = await _stockrepo.DeleteAsync(id);
            if (deleteStock == null) { return NotFound(); }
          
            return NoContent();
            
         }
    }
}
