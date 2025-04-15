using aprendizahem.Interfaces;
using aprendizahem.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace aprendizahem.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly ICommentRepository _CommentRepo;
        public CommentController(ICommentRepository commentrepo)
        {
            _CommentRepo = commentrepo;            
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


    }
}
