﻿using aprendizahem.Dtos.Comments;
using aprendizahem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace aprendizahem.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal Dividend { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }

        public List<CommentDto> Comments { get; set; } 
    }
}
