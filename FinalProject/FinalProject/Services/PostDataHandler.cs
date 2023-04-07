using DatabaseConnector;
using FinalProject.DataBaseContext;
using FinalProject.Models.DTO;
using FinalProject.Models.DTO.PostDTO;

namespace FinalProject.Services
{
    public class PostDataHandler
    {
        EFGenericRepository<Post> _postRepository;
        EFGenericRepository<Content> _contentRepository;
        EFGenericRepository<Comment> _commentRepository;

        public PostDataHandler(EFGenericRepository<Post> postRepository, EFGenericRepository<Content> contentRepository)
        {
            _postRepository = postRepository;
            _contentRepository = contentRepository;
        }

        public Post GetById(int id)
        {

            var tmpPost = _postRepository.GetWithInclude
                (
                post => post.Id == id,
                comms => comms.Comments.Take(10),
                cont => cont.Content,
                usr => usr.User
                )
                .FirstOrDefault();

            return tmpPost;
        }

        public bool Create(CreatePostDTO postData)
        {
            bool result;
            try
            {
                int contentId = _contentRepository.CreateAndGetId(new Content
                {
                    CreationDate = DateTime.UtcNow,
                    IsVisible = true,
                    Text = postData.Description
                });

                int entitiesNumb = _postRepository.Create(new Post
                {
                    Category = postData.Category,
                    ContentId = contentId,
                    Rating = 0,
                    UserId = postData.UserId,
                    CreationDate = DateTime.UtcNow,
                });

                if (entitiesNumb > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

            }
            catch
            {
                result = false;
            }


            return result;
        }

        public bool Edit(EditPostDTO postData)
        {

            var post = _postRepository.FindById(postData.Id);

            if (post == null)
            {
                return false;
            }

            var content = _contentRepository.FindById(post.ContentId);

            if (content == null)
            {
                return false;
            }

            content.CreationDate = DateTime.UtcNow;
            content.IsVisible = true;
            content.Text = postData.Description;

            int res = _contentRepository.Update(content);

            if (res > 0)
            {
                return true;
            }

            return false;
        }

        public bool Delete(EditPostDTO postData)
        {
            var post = _postRepository.FindById(postData.Id);

            if (post == null)
            {
                return false;
            }
            post.IsVisible = false;

            int res = _postRepository.Update(post);

            if (res > 0)
            {
                return true;
            }


            return false;

        }

        public bool Rating(string postData, int Id)
        {
            if (postData == "plus" || postData == "minus")
            {
                var post = _postRepository.FindById(Id);

                if (post != null)
                {
                    if (postData == "plus")
                    {
                        post.Rating += 1;
                    }
                    else
                    {
                        post.Rating -= 1;
                    }

                    int res = _postRepository.Update(post);

                    if (res > 0)
                    {
                        return true;
                    }

                    return false;
                }

            }

            return false;

        }

        public bool AddComment(CommentDTO comment)
        {

            try
            {
                int contentId = _contentRepository.CreateAndGetId(new Content
                {
                    CreationDate = DateTime.UtcNow,
                    IsVisible = true,
                    Text = comment.Text
                });

                int res = _commentRepository.Create(new Comment
                {
                    PostId = comment.PostId,
                    CreationDate = DateTime.UtcNow,
                    ParentId = comment.ParentId,
                    ContentId = contentId
                });

                if (res > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }



    }
}
