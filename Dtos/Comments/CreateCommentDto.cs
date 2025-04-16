using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aprendizahem.Dtos.Comments
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "O titulo deve possuir no minimo 5 caracteres")]
        [MaxLength(40, ErrorMessage = "O titulo não pode passar de 40 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(1, ErrorMessage = "O conteudo deve possuir no minimo 1 caractere")]
        [MaxLength(280, ErrorMessage = "O conteudo não pode passar de 280 caracteres")]
        public string Content { get; set; } = string.Empty;
    }
}
