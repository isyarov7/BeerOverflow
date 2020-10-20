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
            if (!_context.Reviews.Any(b => b.BeerId == reviewDTO.BeerId))
            {
                _context.Reviews.Add(reviewDTO.GetReview());
            }
            else
            {
                var review = _context.Reviews.Where(b => b.BeerId == reviewDTO.BeerId).FirstOrDefault();
                review.IsDeleted = false;
            }


            await _context.SaveChangesAsync();

            return reviewDTO;
        }

        public async Task<ReviewDTO> DeleteReviewAsync(int id)
        {

            var review = await Task.Run(() => this._context.Reviews
                 .Include(r => r.Beer)
                 .Where(x => x.Id == id).FirstOrDefault());

            review.IsDeleted = true;

            await _context.SaveChangesAsync();

            return review.GetDTO();
        }
        public async Task<ICollection<ReviewDTO>> GetAllReviewsAsync()
        {
            var reviews = await Task.Run(() => this._context.Reviews
           .Include(b => b.Beer)
           .Where(b => b.IsDeleted == false)
           .Select(b => b.GetDTO())
           .ToListAsync());

            return reviews;
        }
        public async Task<ReviewDTO> GetReviewAsync(int id)
        {
            var review = await Task.Run(() => this._context.Reviews
                 .FirstOrDefaultAsync(r => r.Id == id));

            return review.GetDTO();
        }

        public async Task<ReviewDTO> UpdateReviewAsync(int id, string newContent)
        {
            var review = await Task.Run(() => this._context.Reviews
                .Where(x => x.Id == id).FirstOrDefault());

            review.Content = newContent;

            await _context.SaveChangesAsync();

            return review.GetDTO();
        }
    }
}
