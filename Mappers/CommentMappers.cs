using aprendizahem.Dtos.Comments;
using aprendizahem.Models;

namespace aprendizahem.Mappers
{
    public static class CommentMappers
    {

        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };

        }

        public static Comment ToCommentFromCreate(this CreateCommentDto comment, int stockid)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content,
                StockId = stockid
            };

        }
        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto comment)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content,
            };
        }


    }
}
