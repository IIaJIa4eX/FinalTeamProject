using DatabaseConnector.DTO;

namespace DatabaseConnector.Extensions;

public static class IssueEx
{
    public static IssueDTO Remap(this Issue issue)
    {
        return new()
        {
            Id = issue.Id,
            ContentId = issue.ContentId,
            ContentText = issue.ContentText,
            CreationDate = issue.CreationDate,
            IssueType = issue.IssueType,
            IsVisible = issue.IsVisible,
            UserId = issue.UserId,
        };
    }
}