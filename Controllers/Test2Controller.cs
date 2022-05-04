using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project1.Data.datamodel;
using project1.Extensions;

namespace adminsite.Controllers
{
    public class Test2Controller : Controller
    {
        private readonly DataContext _context;

        public Test2Controller(DataContext context)
        {
            _context = context;
        }

        // GET: N5VocabExamples
        public async Task<IActionResult> Index()
        {
            return View(await _context.model2data.ToListAsync());
        }

        // GET: N5VocabExamples/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.model2data
                .FirstOrDefaultAsync(m => m.ExampleKey == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: N5VocabExamples/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: N5VocabExamples/AddOrEdit(Insert)
        // GET: N5VocabExamples/AddOrEdit/5(Update)
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new model2());
            else
            {
                var model = await _context.model2data.FindAsync(id);
                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("ExampleKey,Model1Key,Sentence,InEnglish,InMyanmar,Status,CreatedDate,UpdatedDate")] model2 model)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    model.UpdatedDate = DateTime.Now;
                    _context.Add(model);
                    await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    try
                    {
                        _context.Update(model);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!N5VocabExampleExists(model.ExampleKey))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_View_All", _context.model2data.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", model) });
        }

        // POST: N5VocabExamples/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExampleKey,Model1Key,Sentence,InEnglish,InMyanmar,Status,CreatedDate,UpdatedDate")] model2 model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: N5VocabExamples/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.model2data.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: N5VocabExamples/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExampleKey,Model1Key,Sentence,InEnglish,InMyanmar,Status,CreatedDate,UpdatedDate")] model2 model)
        {
            if (id != model.ExampleKey)
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
                    if (!N5VocabExampleExists(model.ExampleKey))
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
            return View(model);
        }

        //// GET: N5VocabExamples/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var n5VocabExample = await _context.N5VocabExamples
        //        .FirstOrDefaultAsync(m => m.ExampleKey == id);
        //    if (n5VocabExample == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(n5VocabExample);
        //}

        // // POST: N5VocabExamples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.model2data.FindAsync(id);
            _context.model2data.Remove(model);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_View_All", _context.model2data.ToList()) });
        }

        //// POST: N5VocabExamples/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var n5VocabExample = await _context.N5VocabExamples.FindAsync(id);
        //    _context.N5VocabExamples.Remove(n5VocabExample);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool N5VocabExampleExists(int id)
        {
            return _context.model2data.Any(e => e.ExampleKey == id);
        }
    }
}
