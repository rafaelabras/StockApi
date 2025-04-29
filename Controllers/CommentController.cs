using aprendizahem.Dtos.Comments;
using aprendizahem.Extension;
using aprendizahem.Interfaces;
using aprendizahem.Mappers;
using aprendizahem.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _UserManager;
        public CommentController(ICommentRepository commentrepo, IStockRepository stockRepo, UserManager<AppUser> usermanager)
        {
            _CommentRepo = commentrepo;
            _StockRepo = stockRepo;
            _UserManager = usermanager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _CommentRepo.GetAllAsync();
            var commentDto = comments.Select(x => x.ToCommentDto());
            return Ok(commentDto);
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _CommentRepo.GetByIdAsync(id);
            if (comment == null) { return NotFound(); }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        [Route("{stockid:int}")]
        public async Task<IActionResult> PostComment([FromRoute] int stockid,[FromBody]CreateCommentDto createdcomment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _StockRepo.StockExists(stockid))
            {
                return BadRequest("Stock não existe");
            }

            var username = User.GetUsername();
            var appuser = await _UserManager.FindByNameAsync(username);

            var commentModel = createdcomment.ToCommentFromCreate(stockid);
            commentModel.AppUserId = appuser.Id;
            await _CommentRepo.PostCommentAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromBody] UpdateCommentRequestDto UpdateCommentDto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _CommentRepo.UpdateAsync(UpdateCommentDto.ToCommentFromUpdate(), id);

            if (comment == null) { return NotFound("Comentario não encontrado"); }

            return Ok(comment.ToCommentDto());

        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var comment = _CommentRepo.DeleteAsync(id);
            if(comment == null) { return NotFound("Comentario não existe"); }
            return Ok();
        }
    }
}
