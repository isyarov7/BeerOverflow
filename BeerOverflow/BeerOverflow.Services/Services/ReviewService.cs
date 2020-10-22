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

        public async Task<ReviewDTO> CreateReviewAsync(ReviewDTO reviewDTO)
        {
            if (_context.Reviews.Any(b => b.BeerId == reviewDTO.BeerId))
            {
                var oldReview = _context.Reviews.Where(b => b.BeerId == reviewDTO.BeerId).FirstOrDefault();
                _context.Reviews.Remove(oldReview);
            }
            _context.Reviews.Add(reviewDTO.GetReview());

            await _context.SaveChangesAsync();

            return reviewDTO;
        }

        public async Task<ReviewDTO> DeleteReviewAsync(int id)
        {

            var review = await this._context.Reviews
                 .Include(r => r.Beer)
                 .FirstOrDefaultAsync(x => x.Id == id);

            review.IsDeleted = true;

            await _context.SaveChangesAsync();

            return review.GetDTO();
        }
        public async Task<ICollection<ReviewDTO>> GetAllReviewsAsync()
        {
            var reviews = await this._context.Reviews
           .Include(b => b.Beer)
           .Where(b => b.IsDeleted == false)
           .Select(b => b.GetDTO())
           .ToListAsync();

            return reviews;
        }
        public async Task<ReviewDTO> GetReviewAsync(int id)
        {
            var review = await this._context.Reviews
                 .FirstOrDefaultAsync(r => r.Id == id);

            return review.GetDTO();
        }

        public async Task<ReviewDTO> UpdateReviewAsync(int id, ReviewDTO reviewDTO)
        {
            var review = await this._context.Reviews
                .FirstOrDefaultAsync(x => x.Id == id);

            review.Content = reviewDTO.Content;

            await _context.SaveChangesAsync();

            return review.GetDTO();
        }
    }
}
