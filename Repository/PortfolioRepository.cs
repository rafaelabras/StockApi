using aprendizahem.Data;
using aprendizahem.Interfaces;
using aprendizahem.Mappers;
using aprendizahem.Models;
using Microsoft.EntityFrameworkCore;

namespace aprendizahem.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio)
        {
            _context.Portfolios.AddAsync(portfolio); // nao precisa usar await pois o .addasync apenas prepara a entidade para ser adicionada no contexto, a requisicao ao bd so acontece no .savechangesasync.
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolioAsync(AppUser user, string symbol)
        {
            var portfoliomodel = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());
            if (portfoliomodel == null)
                return null;

            _context.Portfolios.Remove(portfoliomodel);
            await _context.SaveChangesAsync();
            return portfoliomodel;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
                .Select(stock => new Stock
                {
                    Id = stock.StockId,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Purchase = stock.Stock.Purchase,
                    Dividend = stock.Stock.Dividend,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap
                }).ToListAsync();
        }

        
    }
}
