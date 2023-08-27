using DatabaseConnector.DTO.Post;

namespace DatabaseConnector.Extensions;

public static class PostEx
{
    delegate ICollection<CommentDTO> CommentMapDelegate(ICollection<Comment> coms);
    public static PostDTO Remap(this Post post)
    {
        CommentMapDelegate remap;
        remap = Comments =>
        {
            List<CommentDTO> list = new();
            foreach (var item in Comments)
            {
                list.Add(item.Remap());
            }
            return list;
        };
        return new PostDTO()
        {
            Id = post.Id,
            Category = post.Category!,
            Comments = remap(post.Comments),
            Content = post.Content!.Remap(),
            CreationDate = post.CreationDate,
            IsVisible = post.IsVisible,
            Rating = post.Rating,
            User = post.User!.Remap()
        };
    }
}
