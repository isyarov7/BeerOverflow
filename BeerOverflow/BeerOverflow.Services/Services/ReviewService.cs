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
    public class ReviewService :IReviewService
    {
        private readonly BeerOverflowDbContext _context;
        private readonly IMapper _mapper;
        public ReviewService(BeerOverflowDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public ReviewDTO CreateReview(ReviewDTO review)
        {
            var reviewEntity = _mapper.Map<Review>(review);
            this._context.Reviews.Add(reviewEntity);
            this._context.SaveChanges();
            return review;
        }
    }
}
