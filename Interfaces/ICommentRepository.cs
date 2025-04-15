using aprendizahem.Models;

namespace aprendizahem.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment> GetByIdAsync(int id);

    }
}
