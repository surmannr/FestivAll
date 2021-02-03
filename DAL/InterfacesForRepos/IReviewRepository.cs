using DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesForRepos
{
    public interface IReviewRepository
    {
        // Lekérdezések
        Task<ReviewDto> GetReviewById(int reviewId);
        Task<IReadOnlyCollection<ReviewDto>> GetReviewsByEventId(int eventId);
        Task<IReadOnlyCollection<ReviewDto>> GetAllReviews();
        // Létrehozás
        Task CreateReview(ReviewDto newReview);
        // Módosítások
        Task ModifyStars(int reviewId, int newStars);
        Task ModifyDescription(int reviewId, string newDescription);
        // Törlés
        Task DeleteReview(int reviewId);
    }
}
