﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class ProjectsByCatController : Controller
    {
        private readonly FinalProjectContext _context;

        public ProjectsByCatController(FinalProjectContext context)
        {
            _context = context;
        }

        // GET: ProjectsByCat
        public async Task<IActionResult> Index(long? id)
        {
            var prjcat = (from m in _context.Project
                             //join sem in _context.Project on m.Id equals sem.Id
                         where m.CategoryId == id
                         select m);
            var finalProjectContext = prjcat.Include(p => p.Category).Include(p => p.Person);
            return View("~/Views/ProjectsAll/Index.cshtml",await finalProjectContext.ToListAsync());
        }

        // GET: ProjectsByCat/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.Category)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: ProjectsByCat/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Title");
            ViewData["PersonId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: ProjectsByCat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonId,CategoryId,HeroUrl,Title,Description,Deadline,Goal")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Title", project.CategoryId);
            ViewData["PersonId"] = new SelectList(_context.AspNetUsers, "Id", "Id", project.PersonId);
            return View(project);
        }

        // GET: ProjectsByCat/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Title", project.CategoryId);
            ViewData["PersonId"] = new SelectList(_context.AspNetUsers, "Id", "Id", project.PersonId);
            return View(project);
        }

        // POST: ProjectsByCat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,PersonId,CategoryId,HeroUrl,Title,Description,Deadline,Goal")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Title", project.CategoryId);
            ViewData["PersonId"] = new SelectList(_context.AspNetUsers, "Id", "Id", project.PersonId);
            return View(project);
        }

        // GET: ProjectsByCat/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(p => p.Category)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: ProjectsByCat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var project = await _context.Project.FindAsync(id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(long id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
