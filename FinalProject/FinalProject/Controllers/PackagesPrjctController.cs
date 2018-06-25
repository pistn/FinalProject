using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using System.Web;

namespace FinalProject.Controllers
{
    public class PackagesPrjctController : Controller
    {
        private readonly FinalProjectContext _context;

        public PackagesPrjctController(FinalProjectContext context)
        {
            _context = context;
        }

        // GET: PackagesPrjct
        public async Task<IActionResult> Index(long? id)
        {
            var pckgs = (from m in _context.Package
                         //join sem in _context.Project on m.Id equals sem.Id
                         where m.ProjectId == id
                         select m);// new 
                         //{
                         //    Name=m.Name,
                         //    Description=m.Description,
                         //    Value=m.Value,
                         //    Title=sem.Title
                         //}
                         //      ).SingleOrDefault();


            var finalProjectContext = pckgs.Include(p => p.Project);//.Include(p => p.Person);
            return View(await finalProjectContext.ToListAsync());
            //var finalProjectContext = pckgs;//.FirstOrDefaultAsync(m => m.Id == id);
            //return View(await pckgs.ToListAsync());
            //return View(pckgs);
        }


        //GET: PackagesPrjct/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Package
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        //GET: PackagesPrjct/Create
        public IActionResult Create()
        {
            string useridd = HttpContext.User.Identity.Name;
            var userid = (from m in _context.AspNetUsers
                          where m.Email == useridd
                          select m.Id)
                         .FirstOrDefault();

            var prjcts = (from m in _context.Project
                         // join sem in _context.Package on m.Id equals sem.ProjectId
                          where m.PersonId == userid
                          select new { m.Title, m.Id }).Distinct();
            // ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Description");
            ViewData["ProjectId"] = new SelectList(prjcts, "Id", "Title");
            return View();

            //return View();
        }

        //POST: PackagesPrjct/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectId,Name,Description,Value")] Package package)
        {
            if (ModelState.IsValid)
            {
                _context.Add(package);
                await _context.SaveChangesAsync();
                //return RedirectUrl(nameof(Index));
                //return RedirectToAction("Index");
                //var request = HttpContext.Request;
                //var referrer = request.UrlReferrer;
                //return Redirect(Request.UrlReferrer.ToString());
                return Redirect("https://localhost:44361");
                //Response.Redirect(Request.UrlReferrer.AbsoluteUri.ToString());
                //return RedirectToAction("Create",
            //new { returnUrl = Request.UrlReferrer.ToString() });
            }

            string useridd = HttpContext.User.Identity.Name;
            var userid = (from m in _context.AspNetUsers
                          where m.Email == useridd
                          select m.Id)
                         .FirstOrDefault();

            var prjcts = (from m in _context.Project
                          //join sem in _context.Package on m.Id equals sem.ProjectId
                          where m.PersonId == userid
                          select new { m.Title, m.Id }).Distinct();
            // ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Description");
            

            ViewData["ProjectId"] = new SelectList(prjcts, "Id", "Title", package.ProjectId);

            return View();
            //ViewData["ProjectId"] = new SelectList(prjcts);
            //ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Title", package.ProjectId);

            //return View(package);
        }

        //GET: PackagesPrjct/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Package.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            string useridd = HttpContext.User.Identity.Name;
            var userid = (from m in _context.AspNetUsers
                          where m.Email == useridd
                          select m.Id)
                         .FirstOrDefault();

            var prjcts = (from m in _context.Project
                          //join sem in _context.Package on m.Id equals sem.ProjectId
                          where m.PersonId == userid
                          select new { m.Title, m.Id }).Distinct();

             ViewData["ProjectId"] = new SelectList(prjcts, "Id", "Title", package.ProjectId);
            //ViewData["ProjectId"] = new SelectList(prjcts);
            return View();
        }

        //POST: PackagesPrjct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ProjectId,Name,Description,Value")] Package package)
        {
            if (id != package.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(package);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackageExists(package.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return Redirect("https://localhost:44361");
            }
            string useridd = HttpContext.User.Identity.Name;
            var userid = (from m in _context.AspNetUsers
                          where m.Email == useridd
                          select m.Id)
                         .FirstOrDefault();

            var prjcts = (from m in _context.Project
                              //join sem in _context.Package on m.Id equals sem.ProjectId
                          where m.PersonId == userid
                          select new { m.Title, m.Id }).Distinct();

            ViewData["ProjectId"] = new SelectList(prjcts, "Id", "Title", package.ProjectId);

            //ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Title", package.ProjectId);
            return View();
        }

        //GET: PackagesPrjct/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Package
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        //POST: PackagesPrjct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var package = await _context.Package.FindAsync(id);
            _context.Package.Remove(package);
            await _context.SaveChangesAsync();
            // return RedirectToAction(nameof(Index));
            return Redirect("https://localhost:44361");
        }

        private bool PackageExists(long id)
        {
            return _context.Package.Any(e => e.Id == id);
        }
    }
}
