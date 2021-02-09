using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesForRepos
{
    public interface IReviewRepository
    {
        // Lekérdezések
        Task<Review> GetReviewById(int reviewId);
        Task<IReadOnlyCollection<Review>> GetReviewsByEventId(int eventId);
        Task<IReadOnlyCollection<Review>> GetAllReviews();
        // Létrehozás
        Task CreateReview(Review newReview);
        // Módosítások
        Task ModifyStars(int reviewId, int newStars);
        Task ModifyDescription(int reviewId, string newDescription);
        // Törlés
        Task DeleteReview(int reviewId);
    }
}
