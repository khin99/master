using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project1.Data.datamodel;
using Microsoft.AspNetCore.Authorization;

namespace adminsite.Controllers
{
  
    public class Test1Controller : Controller
    {
        private readonly DataContext _context;

        public Test1Controller(DataContext context)
        {
            _context = context;
        }

        // GET: N5Vocabs
        public async Task<IActionResult> Index()
        {
            return View(await _context.model1data.OrderByDescending(s => s.OrderId).ToListAsync());
        }

        // GET: N5Vocabs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.model1data
                .Include(m => m.Examples)
                .FirstOrDefaultAsync(m => m.Key == id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.PrevID = id - 1;
            ViewBag.NexID = id + 1;
            return View(model);
        }

        // GET: N5Vocabs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: N5Vocabs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Key,Vocab,Romaji,Hiragana,OrderId,InMyanmar,InEnglish,WordGroup,GroupDetail,Status,CreatedDate,UpdatedDate")] model1 model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();

                int key = model.Key;
                return RedirectToAction(nameof(Details), new { id = key });
            }
            return View(model);
        }

        // GET: N5Vocabs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.model1data.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: N5Vocabs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Key,Vocab,Romaji,Hiragana,OrderId,InMyanmar,InEnglish,WordGroup,GroupDetail,Status,CreatedDate,UpdatedDate")] model1 model)
        {
            if (id != model.Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!N5VocabExists(model.Key))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = model.Key });
            }
            return View(model);
        }

        // GET: N5Vocabs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.model1data
                .FirstOrDefaultAsync(m => m.Key == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: N5Vocabs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.model1data.FindAsync(id);
            _context.model1data.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool N5VocabExists(int id)
        {
            return _context.model1data.Any(e => e.Key == id);
        }
    }
}
