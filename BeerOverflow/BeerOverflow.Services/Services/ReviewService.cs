using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflow.Services.Services
{
    public class ReviewService : IReviewService
    {
        private readonly BeerOverflowDbContext _context;
        public ReviewService(BeerOverflowDbContext context)
        {
            this._context = context;
        }

        public void CreateReview(ReviewDTO reviewDTO)
        {
            if (reviewDTO == null)
            {
                throw new ArgumentNullException();
            }

            var review = new Review
            {
                Content = reviewDTO.Content,
                BeerId = reviewDTO.BeerId
            };

            if (_context.Reviews.Any(b => b.Content == review.Content))
            {
                throw new ArgumentException("This content already exists!");
            }

            _context.Reviews.Add(review);

            _context.SaveChanges();
        }

        public void DeleteReview(int id)
        {
            var review = _context.Reviews
                .Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Id == id);

            if (review == null)
            {
                throw new ArgumentNullException();
            }

            review.IsDeleted = true;

            _context.SaveChanges();
        }

        public IEnumerable<ReviewDTO> GetAllReviews()
        {
            var reviews = _context.Reviews
                .Where(rev => rev.IsDeleted == false)
                .Select(rev => new ReviewDTO
                {
                    Content = rev.Content,
                })
               .ToList();

            return reviews;
        }

        public ReviewDTO GetReview(int id)
        {
            var review = _context.Reviews
                .Where(rev => rev.IsDeleted == false)
                .FirstOrDefault(rev => rev.Id == id);

            if (review == null)
            {
                throw new ArgumentNullException();
            }

            var reviewDTO = new ReviewDTO
            {
                Content = review.Content,
            };

            return reviewDTO;
        }

        public void UpdateReview(int id, ReviewDTO reviewDTO)
        {
            var review = _context.Reviews.Where(x => x.IsDeleted == false)
                 .FirstOrDefault(x => x.Id == id);

            if (review == null)
            {
                throw new ArgumentNullException();
            }

            review.Content = reviewDTO.Content;

            _context.SaveChanges();
        }
    }
}
