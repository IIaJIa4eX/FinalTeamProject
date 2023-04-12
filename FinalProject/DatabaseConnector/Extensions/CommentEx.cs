using DatabaseConnector.DTO.Post;

namespace DatabaseConnector.Extensions;

public static class CommentEx
{
    public static CommentDTO Remap(this Comment comment)
    {
        return new CommentDTO()
        {
            Id = comment.Id,
            Content = comment.Content!.Remap(),
            IsVisible = comment.IsVisible,
            ParentId = comment.ParentId,
            PostId = comment.PostId,
            User = comment.User!.Remap(),
        };
    }
}
