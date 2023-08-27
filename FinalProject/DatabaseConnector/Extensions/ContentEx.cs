using DatabaseConnector.DTO.Post;

namespace DatabaseConnector.Extensions;

public static class ContentEx
{
    public static ContentDTO Remap(this Content content)
    {
        return new ContentDTO()
        {
            CreationDate = content.CreationDate,
            Id = content.Id,
            IsVisible = content.IsVisible,
            Text = content.Text
        };
    }
}
