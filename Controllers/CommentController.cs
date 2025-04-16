using aprendizahem.Dtos.Comments;
using aprendizahem.Interfaces;
using aprendizahem.Mappers;
using aprendizahem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace aprendizahem.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly ICommentRepository _CommentRepo;
        private readonly IStockRepository _StockRepo;
        public CommentController(ICommentRepository commentrepo, IStockRepository stockRepo)
        {
            _CommentRepo = commentrepo;
            _StockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _CommentRepo.GetAllAsync();
            var commentDto = comments.Select(x => x.ToCommentDto());
            return Ok(commentDto);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var comment = await _CommentRepo.GetByIdAsync(id);
            if (comment == null) { return NotFound(); }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        [Route("{stockid}")]
        public async Task<IActionResult> PostComment([FromRoute] int stockid,[FromBody]CreateCommentDto createdcomment)
        {
            if (!await _StockRepo.StockExists(stockid))
            {
                return BadRequest("Stock não existe");
            }

            var commentModel = createdcomment.ToCommentFromCreate(stockid);
            await _CommentRepo.PostCommentAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateCommentRequestDto UpdateCommentDto, [FromRoute] int id)
        {
            var comment = await _CommentRepo.UpdateAsync(UpdateCommentDto.ToCommentFromUpdate(), id);

            if (comment == null) { return NotFound("Comentario não encontrado"); }

            return Ok(comment.ToCommentDto());

        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = _CommentRepo.DeleteAsync(id);
            if(comment == null) { return NotFound("Comentario não existe"); }
            return Ok();
        }
    }
}
