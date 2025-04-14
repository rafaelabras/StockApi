using aprendizahem.Data;
using aprendizahem.Dtos.Stock;
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
        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _context.Stock.ToListAsync();
                
            var stockDto = stocks.Select(s => s.ToStockDto());
            return Ok(stockDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stocks = await _context.Stock.FindAsync(id);

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
            await _context.Stock.AddAsync(StockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id = StockModel.Id}, StockModel.ToStockDto());
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
           var stockModel = await _context.Stock.FirstOrDefaultAsync(item => item.Id == id);
           if (stockModel == null) { return NotFound(); }

            stockModel.Symbol = stockDto.Symbol;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.Dividend = stockDto.Dividend;
            stockModel.MarketCap = stockDto.MarketCap;
            stockModel.Industry = stockDto.Industry;

            await _context.SaveChangesAsync();
            return Ok(stockModel.ToStockDto);
           
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleteStock = await _context.Stock.FirstOrDefaultAsync(item => item.Id == id);
            if (deleteStock == null) { return NotFound(); }
            _context.Stock.Remove(deleteStock);
            await _context.SaveChangesAsync();

            return NoContent();
            
         }
    }
}
