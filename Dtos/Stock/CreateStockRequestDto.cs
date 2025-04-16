using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aprendizahem.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "O symbol so vai ate 10 caracteres.")]
        [MinLength(1, ErrorMessage = "O symbol não pode ser menor q 1 caracter")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(10, ErrorMessage = "O symbol so vai ate 10 caracteres.")]
        [MinLength(3, ErrorMessage = "O symbol não pode ser menor q 1 caracter")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000000000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 100)]
        public decimal Dividend { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "O nome da industria não pode pasasr de 10 caracteres")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 5000000000000)]
        public long MarketCap { get; set; }
    }
}
