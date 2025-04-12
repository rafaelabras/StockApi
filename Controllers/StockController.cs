using aprendizahem.Data;
using aprendizahem.Dtos.Stock;
using aprendizahem.Mappers;
using aprendizahem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Completion;
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
        public IActionResult GetAll()
        {
            var stocks = _context.Stock.ToList()
                .Select(s => s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stocks = _context.Stock.Find(id);

            if (stocks == null)
            {
                return NotFound();
            }
            return Ok(stocks.ToStockDto());

        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var StockModel = stockDto.ToStockFromCreateDTO();
            _context.Stock.Add(StockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id = StockModel.Id}, StockModel.ToStockDto());
        }


        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
           var stockModel = _context.Stock.FirstOrDefault(item => item.Id == id);
           if (stockModel == null) { return NotFound(); }

            stockModel.Symbol = stockDto.Symbol;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.Dividend = stockDto.Dividend;
            stockModel.MarketCap = stockDto.MarketCap;
            stockModel.Industry = stockDto.Industry;

            _context.SaveChanges();
            return Ok(stockModel.ToStockDto);
           
        }
    }
}
