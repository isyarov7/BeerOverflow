using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerOverflow.Database;
using BeerOverflow.Models.Models;
using BeerOverflow.Services.Contracts;
using AutoMapper;
using BeerOverflow.Models;
using BeerOverflow.Services.DTOs;

namespace BeerOverflow.Controllers
{
    public class StylesController : Controller
    {
        private readonly IStyleService _service;
        private readonly IMapper _mapper;

        public StylesController(IStyleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: Styles
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllStylesAsync());
        }

        // GET: Styles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var style = await _service.GetStyleAsync(id);

            return View(style);
        }

        // GET: Styles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Styles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StyleViewModel styleViewModel)
        {
            var styleDTO = _mapper.Map<StyleDTO>(styleViewModel);

            await _service.CreateStyleAsync(styleDTO);

            return RedirectToAction(nameof(Index));
        }

        // GET: Styles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var style = await _service.GetStyleAsync(id);
            return View(style);
        }

        // POST: Styles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StyleViewModel styleViewModel)
        {
            var styleDTO = _mapper.Map<StyleDTO>(styleViewModel);

            await _service.UpdateStyleAsync(id, styleDTO);

            return RedirectToAction(nameof(Index));
        }

        // GET: Styles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var style = await _service.GetStyleAsync(id);

            return View(style);
        }

        // POST: Styles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _service.DeleteStyleAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
