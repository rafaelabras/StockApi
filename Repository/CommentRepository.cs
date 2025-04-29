using aprendizahem.Data;
using aprendizahem.Dtos.Comments;
using aprendizahem.Interfaces;
using aprendizahem.Mappers;
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

        public async Task<Comment?> DeleteAsync(int id)
        {
           var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null) { return null; }
           _context.Comments.Remove(comment);
           await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(x => x.appUser).ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.Include(x => x.appUser).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment> PostCommentAsync(Comment addComment)
        {

            await _context.Comments.AddAsync(addComment);
            await _context.SaveChangesAsync();
            return addComment;

        }

        public async Task<Comment> UpdateAsync(Comment comment, int id)
        {
            var commentFind = await _context.Comments.FindAsync(id);

            if (commentFind == null) { return null; }

            commentFind.Title = comment.Title;
            commentFind.Content = comment.Content;
            await _context.SaveChangesAsync();
            return commentFind;

        }
    }
}
