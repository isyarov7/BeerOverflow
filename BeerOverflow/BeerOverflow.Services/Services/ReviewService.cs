using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOs;
using BeerOverflow.Services.MappersDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var review = new Review
            {
                Content = reviewDTO.Content,
                BeerId = reviewDTO.BeerId
            };

            var alreadyCreated = _context.Reviews.Where(b => b.Content == review.Content).FirstOrDefault();

            if (alreadyCreated.IsDeleted == true)
            {
                alreadyCreated.IsDeleted = false;
            }
            else if (alreadyCreated.IsDeleted == false)
            {
                alreadyCreated.IsDeleted = false;
            }
            else
            {
                _context.Reviews.Add(review);
            }

            _context.Reviews.Add(review);

            _context.SaveChanges();
        }

        public void DeleteReview(ReviewDTO reviewDTO)
        {
            var review = _context.Reviews
                .Where(x => x.IsDeleted == false)
                .FirstOrDefault(x => x.Content == reviewDTO.Content);

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
                .Include(rev => new ReviewDTO
                {
                    Content = rev.Content,
                })
               .ToList()
               .GetDTO();

            return reviews;
        }

        public ReviewDTO GetReview(int id)
        {
            var review = _context.Reviews
                .Where(rev => rev.IsDeleted == false)
                .FirstOrDefault(rev => rev.Id == id).GetDTO();

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
        public async Task<ReviewDTO> CreateReviewAsync(ReviewDTO reviewDTO)
        {
            if (reviewDTO == null)
            {
                throw new ArgumentNullException();
            }

            var review = (await this._context.Reviews
                .FirstOrDefaultAsync(c => c.Content == reviewDTO.Content));

            var toRevive = _context.Reviews.Where(c => c.Content == review.Content && c.IsDeleted == true)
                .FirstOrDefault();

            if (_context.Reviews.Any(c => c.Content == review.Content && c.IsDeleted == true))
            {
                toRevive.IsDeleted = false;
            }
            else if (_context.Reviews.Any(c => c.Content == review.Content && c.IsDeleted == false))
            {
                toRevive.IsDeleted = false;
            }
            else
            {
                _context.Reviews.Add(review);
            }

            _context.SaveChanges();

            return review.GetDTO();
        }

        public async Task<ReviewDTO> DeleteReviewAsync(ReviewDTO reviewDTO)
        {
            var review = (await this._context.Reviews
                .FirstOrDefaultAsync(x => x.Content == reviewDTO.Content && x.IsDeleted == false));

            if (review == null)
            {
                throw new ArgumentNullException();
            }

            review.IsDeleted = true;

            _context.SaveChanges();

            return review.GetDTO();
        }
        public async Task<ReviewDTO> GetAllReviewsAsync()
        {
            var reviews = (await this._context.Reviews
                .FirstOrDefaultAsync(review => review.IsDeleted == false)).GetDTO();

            return reviews;
        }
        public async Task<ReviewDTO> GetReviewAsync(int id)
        {
            var review = (await this._context.Reviews
                .FirstOrDefaultAsync(review => review.IsDeleted == false)).GetDTO();

            if (review == null)
            {
                throw new ArgumentException();
            }

            return review;
        }

        public async Task<ReviewDTO> UpdateReviewAsync(ReviewDTO reviewDTO, string newContent)
        {
            var review = (await this._context.Reviews
               .FirstOrDefaultAsync(x => x.Content == reviewDTO.Content)).GetDTO();

            if (review == null)
            {
                throw new ArgumentNullException();
            }

            review.Content = newContent;

            _context.SaveChanges();

            return review;
        }
    }
}
