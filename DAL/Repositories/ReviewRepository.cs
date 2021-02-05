using DAL.DTOs;
using DAL.Exceptions;
using DAL.InterfacesForRepos;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly FestivallDb db;
        public ReviewRepository(FestivallDb _db)
        {
            db = _db;
        }

        public async Task CreateReview(ReviewDto newReview)
        {
            if(newReview == null) throw new Exception(ExceptionMessageConstants.NullObject);
            if(ReviewRepositoryExtension.IsReviewParamsNull(newReview)) throw new Exception(ExceptionMessageConstants.RequiredParams);
            Review nreview = new Review()
            {
                Description = newReview.Description,
                Stars = newReview.Stars,
                EventId = newReview.EventId,
                UserId = newReview.UserId
            };
            db.Reviews.Add(nreview);
            await db.SaveChangesAsync();
        }

        public async Task DeleteReview(int reviewId)
        {
            var review = await db.Reviews.Where(r => r.Id == reviewId).FirstOrDefaultAsync();
            if (review != null)
            {
                db.Reviews.Remove(review);
                await db.SaveChangesAsync();
            } 
            else throw new Exception(ExceptionMessageConstants.NullObject);
        }

        public async Task<IReadOnlyCollection<ReviewDto>> GetAllReviews()
        {
            return await db.Reviews.GetReviewsList();
        }

        public async Task<ReviewDto> GetReviewById(int reviewId)
        {
            return await db.Reviews.GetReviewById(reviewId);
        }

        public async Task<IReadOnlyCollection<ReviewDto>> GetReviewsByEventId(int eventId)
        {
            return await db.Reviews.GetReviewsByEventId(eventId);
        }

        public async Task ModifyDescription(int reviewId, string newDescription)
        {
            var review = await db.Reviews.Where(r => r.Id == reviewId).FirstOrDefaultAsync();
            if (review != null)
            {
                review.Description = newDescription;
                await db.SaveChangesAsync();
            }
            else throw new Exception(ExceptionMessageConstants.NullObject);
        }

        public async Task ModifyStars(int reviewId, int newStars)
        {
            var review = await db.Reviews.Where(r => r.Id == reviewId).FirstOrDefaultAsync();
            if (review != null)
            {
                review.Stars = newStars;
                await db.SaveChangesAsync();
            }
            else throw new Exception(ExceptionMessageConstants.NullObject);
        }
    }
    internal static class ReviewRepositoryExtension
    {
        public static bool IsReviewParamsNull(ReviewDto reviewDto)
        {
            return reviewDto.EventId == 0 && reviewDto.UserId == null
                && reviewDto.Stars == 0 && reviewDto.Description == null;
        }

        public static async Task<IReadOnlyCollection<ReviewDto>> GetReviewsList(this IQueryable<Review> reviews)
            => await reviews.Select(r => new ReviewDto(r.Stars, r.Description, r.EventId, r.UserId)).ToListAsync();

        public static async Task<ReviewDto> GetReviewById(this IQueryable<Review> reviews, int reviewId)
            => await reviews.Where(r => r.Id == reviewId).Select(r => new ReviewDto(r.Stars, r.Description, r.EventId, r.UserId)).FirstOrDefaultAsync();

        public static async Task<IReadOnlyCollection<ReviewDto>> GetReviewsByEventId(this IQueryable<Review> reviews, int eventId)
            => await reviews.Where(r => r.EventId == eventId).Select(r => new ReviewDto(r.Stars, r.Description, r.EventId, r.UserId)).ToListAsync();
    }
}
