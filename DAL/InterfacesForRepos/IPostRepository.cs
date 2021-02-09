using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesForRepos
{
    public interface IPostRepository
    {
        // Lekérdezések
        Task<Post> GetPostById(int postId);
        Task<IReadOnlyCollection<Post>> GetPostsByEventId(int eventId);
        Task<IReadOnlyCollection<Post>> GetAllPosts();
        // Létrehozás
        Task CreatePost(Post newPost);
        // Módosítások
        Task ModifyContent(int postId, string content);
        // Törlés
        Task DeletePost(int postId);
    }
}
