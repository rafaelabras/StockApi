using aprendizahem.Data;
using aprendizahem.Interfaces;
using aprendizahem.Models;
using Microsoft.EntityFrameworkCore;

namespace aprendizahem.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }
    }
}
