using BL.InterfacesForManagers;
using DAL.InterfacesForRepos;
using DAL.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class ReviewManager : IReviewManager
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewManager(IReviewRepository _reviewRepository)
        {
            reviewRepository = _reviewRepository;
        }

        // Lekérdezések

        public async Task<ReviewDto> GetReviewByIdAsync(int reviewId)
        {
            var review = await reviewRepository.GetReviewById(reviewId);
            return new ReviewDto(review.Stars, review.Description, review.EventId, review.UserId);
        }

        public async Task<IReadOnlyCollection<ReviewDto>> GetReviewsAsync()
        {
            var reviews = await reviewRepository.GetAllReviews();
            return reviews.Select(r => new ReviewDto(r.Stars, r.Description, r.EventId, r.UserId)).ToList();
        }

        public async Task<IReadOnlyCollection<ReviewDto>> GetReviewsByEventIdAsync(int eventId)
        {
            var reviews = await reviewRepository.GetReviewsByEventId(eventId);
            return reviews.Select(r => new ReviewDto(r.Stars, r.Description, r.EventId, r.UserId)).ToList();
        }

        // Létrehozás

        public async Task CreateReviewAsync(ReviewDto newReviewDto)
        {
            Review review = new Review()
            {
                Stars = newReviewDto.Stars,
                Description = newReviewDto.Description,
                EventId = newReviewDto.EventId,
                UserId = newReviewDto.UserId
            };
            await reviewRepository.CreateReview(review);
        }

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
