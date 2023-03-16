using FinalProject.Data;
using Microsoft.Data.SqlClient;

namespace FinalProject.Services.Impl
{
    public class PostsRepository : IPostsRepository
    {
        private readonly FinalProjectDbContext _context;
        private readonly ILogger<PostsRepository> _logger;
        public PostsRepository(FinalProjectDbContext context, ILogger<PostsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int Create(Posts data)
        {
            var client = _context.Post.FirstOrDefault(client => client.UserId == data.UserId);
            if (client == null)
                throw new Exception("User not found.");
            _context.Post.Add(data);
            _context.SaveChanges();
            return data.PostId;
        }
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
        public IList<Posts> GetAll()
        {
            throw new NotImplementedException();
        }
        public IList<Posts> GetByClientId(string id)
        {
            List<Posts> cards = new List<Posts>();
            using (SqlConnection sqlConnection = new SqlConnection(_databaseOptions.Value.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(String.Format("select * from cards where ClientId = {0}", id), sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        cards.Add(new Posts
                        {
                            PostId = new Guid(reader["PostId"].ToString()),
                            ContentText = reader["ContentText"]?.ToString(),
                            Rating = reader["Rating"]?.ToString(),
                            CVV2 = reader["CVV2"]?.ToString(),
                            CreationDate = Convert.ToDateTime(reader["ExpDate"])
                        });
                    }
                }
            }
            return cards;
        }

        public Posts GetById(int id)
        {
            throw new NotImplementedException();
        }
        public int Update(Posts data)
        {
            throw new NotImplementedException();
        }
    }
}
