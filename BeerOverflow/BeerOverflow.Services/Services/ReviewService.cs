using AutoMapper;
using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Services
{
    public class ReviewService : IReviewService
    {
        private readonly BeerOverflowDbContext _context;
        public ReviewService(BeerOverflowDbContext context)
        {
            this._context = context;
        }

        public ReviewDTO CreateReview(ReviewDTO reviewDTO)
        {
            throw new NotImplementedException();
        }

        public bool DeleteReview(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReviewDTO> GetAllReviews()
        {
            throw new NotImplementedException();
        }

        public ReviewDTO GetReview(int id)
        {
            throw new NotImplementedException();
        }

        public ReviewDTO UpdateReview(int id, ReviewDTO reviewDTO)
        {
            throw new NotImplementedException();
        }
    }
}
