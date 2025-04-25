using aprendizahem.Extension;
using aprendizahem.Interfaces;
using aprendizahem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace aprendizahem.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolio)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolio;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> PortfolioGetUser()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PortfolioCreate(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if (stock == null)
                return NotFound("Stock nao encontrado. ");

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

            if (userPortfolio.Contains(stock))
                return BadRequest("O usuario ja tem esse stock adicionado em seu portfolio.");

            var portfolio = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };

            await _portfolioRepository.CreatePortfolioAsync(portfolio);
            if (portfolio == null)
                return StatusCode(500, "Nao foi possivel criar o portfolio.");
            else
                return Created();

        }
        
    }
}
