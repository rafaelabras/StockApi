using aprendizahem.Dtos.Stock;
using aprendizahem.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace aprendizahem.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                Dividend = stockModel.Dividend,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };

        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto StockDt)
        {
            return new Stock
            {
                Symbol = StockDt.Symbol,
                CompanyName = StockDt.CompanyName,
                Purchase = StockDt.Purchase,
                Dividend = StockDt.Dividend,
                Industry= StockDt.Industry,
                MarketCap = StockDt.MarketCap
            }; 
        }

    }
}
