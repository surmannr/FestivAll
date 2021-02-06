using DAL.DTOs;
using DAL.InterfacesForRepos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class ReviewManager
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewManager(IReviewRepository _reviewRepository)
        {
            reviewRepository = _reviewRepository;
        }

        // Lekérdezések

        public async Task<ReviewDto> GetReviewByIdAsync(int reviewId)
            => await reviewRepository.GetReviewById(reviewId);

        public async Task<IReadOnlyCollection<ReviewDto>> GetReviewsAsync()
            => await reviewRepository.GetAllReviews();

        public async Task<IReadOnlyCollection<ReviewDto>> GetReviewsByEventIdAsync(int eventId)
            => await reviewRepository.GetReviewsByEventId(eventId);

        // Létrehozás

        public async Task CreateReviewAsync(ReviewDto newReviewDto)
            => await reviewRepository.CreateReview(newReviewDto);

        // Törlés

        public async Task DeleteReviewAsync(int reviewId)
            => await reviewRepository.DeleteReview(reviewId);

        // Módosítások

        public async Task ModifyStarsAsync(int reviewId, int stars)
            => await reviewRepository.ModifyStars(reviewId, stars);

        public async Task ModifyContentAsync(int reviewId, string description)
            => await reviewRepository.ModifyDescription(reviewId, description);
    }
}
