namespace TaskManagementSystem.Models
{
    public interface ICommentRepository
    {
        Comment GetComment(int id);
        IEnumerable<Comment> GetAllComments();
        Comment AddComment(Comment comment);
        Comment UpdateComment(Comment commentChanges);
        Comment DeleteComment(int id);
        IEnumerable<Comment> GetCommentsForTask(int id);
    }
}
