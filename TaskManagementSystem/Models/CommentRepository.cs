
namespace TaskManagementSystem.Models
{
    public class CommentRepository : ICommentRepository
    {
        private readonly TMSDbContext DB;
        public CommentRepository(TMSDbContext DB)
        {
            this.DB = DB;
        }
        public Comment AddComment(Comment comment)
        {
            DB.Comment.Add(comment);
            DB.SaveChanges();
            return comment;
        }

        public Comment DeleteComment(int id)
        { 
            Comment comment = DB.Comment.Find(id);
            if (comment != null)
            {
                DB.Comment.Remove(comment);
                DB.SaveChanges();
            }
            return comment;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return DB.Comment;
        }

        public Comment GetComment(int id)
        {
            return DB.Comment.Find(id);
        }

        public Comment UpdateComment(Comment commentChanges)
        {
            var comment = DB.Comment.Attach(commentChanges);
            comment.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            DB.SaveChanges();
            return commentChanges;
        }
    }
}
