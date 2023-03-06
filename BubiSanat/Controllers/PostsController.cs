using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BubiSanat.Data;
using BubiSanat.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BubiSanat.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index(short? id = null)
        {
            IQueryable<Post> applicationDbContext = _context.Posts.Include(p => p.Category).Include(p => p.NextPost).Include(p => p.PreviousPost);
            if (id != null)
            {
                applicationDbContext = applicationDbContext.Where(p => p.CategoryId == id.Value);
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posts/Details/5
        
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.NextPost)
                .Include(p => p.PreviousPost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            post.DisplayCount = post.DisplayCount + 1;
            _context.Update(post);
            _context.SaveChanges();
            return View(post);
        }
        public long Likes(long? id)
        {
            Post post =  _context.Posts.Where(p=>p.Id==id).FirstOrDefault();
           
            post.Likes = post.Likes + 1;
            _context.Update(post);
            _context.SaveChanges();
            return post.Likes;
        }

        [Authorize]
        // GET: Posts/Create
        public IActionResult Create()
        {
            string authorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Post> posts = _context.Posts.Where(p => p.AuthorId == User.FindFirstValue(ClaimTypes.NameIdentifier)).OrderBy(p => p.CreatedAt).ToList();

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["NextPostId"] = new SelectList(_context.Posts, "Id", "Title");
            ViewData["PreviousPostId"] = new SelectList(_context.Posts, "Id", "Title");
            ViewData["authorId"] = authorId;
            return View();
        }

        [Authorize]
        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,CategoryId,Content,PreviousPostId,NextPostId,Tags,AuthorId,FormImage")] Post post)
        {
            MemoryStream memoryStream;

            post.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                if(post.FormImage != null)
                {
                    memoryStream = new MemoryStream();
                    post.FormImage.CopyTo(memoryStream);
                    post.Image= memoryStream.ToArray();
                }
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            ViewData["NextPostId"] = new SelectList(_context.Posts, "Id", "Content", post.NextPostId);
            ViewData["PreviousPostId"] = new SelectList(_context.Posts, "Id", "Content", post.PreviousPostId);
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            if(post.AuthorId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            ViewData["NextPostId"] = new SelectList(_context.Posts, "Id", "Content", post.NextPostId);
            ViewData["PreviousPostId"] = new SelectList(_context.Posts, "Id", "Content", post.PreviousPostId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,CreatedAt,CategoryId,Content,Deleted,PreviousPostId,NextPostId,Likes,DisplayCount,Tags")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            if (post.AuthorId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            ViewData["NextPostId"] = new SelectList(_context.Posts, "Id", "Content", post.NextPostId);
            ViewData["PreviousPostId"] = new SelectList(_context.Posts, "Id", "Content", post.PreviousPostId);
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.NextPost)
                .Include(p => p.PreviousPost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            if (post.AuthorId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            if (post.AuthorId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(long id)
        {
          return _context.Posts.Any(e => e.Id == id);
        }
    }
}
