using aprendizahem.Dtos.Comments;
using aprendizahem.Models;

namespace aprendizahem.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment> GetByIdAsync(int id);
        Task<Comment> PostCommentAsync(Comment comment);
        Task<Comment?> UpdateAsync(Comment comment, int id);
        Task<Comment?> DeleteAsync(int id);
    }
}
