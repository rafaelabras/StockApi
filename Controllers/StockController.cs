using aprendizahem.Data;
using aprendizahem.Dtos.Stock;
using aprendizahem.Mappers;
using aprendizahem.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
