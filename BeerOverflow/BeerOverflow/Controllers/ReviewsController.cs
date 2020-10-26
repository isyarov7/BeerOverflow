using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeerOverflow.Services.Contracts;
using AutoMapper;
using BeerOverflow.Models;
using BeerOverflow.Services.DTOs;
using Microsoft.AspNetCore.Identity;
using BeerOverflow.Models.Models;
using Microsoft.AspNetCore.Authorization;

namespace BeerOverflow.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IBeerService _beerService;
        private readonly IReviewService _service;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        public ReviewsController(SignInManager<User> signInManager, IBeerService beerService, IReviewService service, IMapper mapper)
        {
            this._signInManager = signInManager;
            this._beerService = beerService;
            this._service = service;
            this._mapper = mapper;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllReviewsAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var review = await _service.GetReviewAsync(id);

            return View(review);
        }

        // GET: Reviews/Create
        public async Task<IActionResult> Create()
        {
            if (!_signInManager.IsSignedIn(User))
            {
                Response.Redirect("https://localhost:5001/Identity/Account/Login");
            }
            ViewData["BeerId"] = new SelectList(await _beerService.GetAllBeersAsync(), "Id", "Name");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel reviewViewModel)
        {
            var reviewDTO = _mapper.Map<ReviewDTO>(reviewViewModel);

            await _service.CreateReviewAsync(reviewDTO);

            return RedirectToAction(nameof(Index));
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                Response.Redirect("https://localhost:5001/Identity/Account/Login");
            }
            var review = await _service.GetReviewAsync(id);

            ViewData["BeerId"] = new SelectList(await _beerService.GetAllBeersAsync(), "Id", "Name");

            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReviewViewModel reviewViewModel)
        {
            var reviewDTO = _mapper.Map<ReviewDTO>(reviewViewModel);

            await _service.UpdateReviewAsync(id, reviewDTO);

            return RedirectToAction(nameof(Index));
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (!_signInManager.IsSignedIn(User))
            {
                Response.Redirect("https://localhost:5001/Identity/Account/Login");
            }
            var review = await _service.GetReviewAsync(id);

            return View(review);
        }

        // POST: Reviews/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _service.DeleteReviewAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
