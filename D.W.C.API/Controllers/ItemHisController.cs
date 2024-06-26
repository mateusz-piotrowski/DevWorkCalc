﻿using D.W.C.API.D.W.C.Service;
using D.W.C.Lib.D.W.C.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace D.W.C.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemHisController : ControllerBase
    {
        private readonly MyDatabaseContext _context;

        public ItemHisController(MyDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ItemDet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkItemHistory>>> GetWorkItemHisotry()
        {
            return await _context.WorkItemsHistory.ToListAsync();
        }

        // GET: api/ItemDet/History/5
        [HttpGet("History/{id}")]
        public async Task<ActionResult<IEnumerable<WorkItemHistory>>> GetWorkItemHistory(int id)
        {
            var historyItems = await _context.WorkItemsHistory
                .Where(h => h.ApiId == id)
                .ToListAsync();

            if (historyItems == null || historyItems.Count == 0)
            {
                return NotFound();
            }

            return historyItems;
        }


        // PUT: api/ItemDet/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemDet(int id, WorkItemHistory itemDet)
        {
            if (id != itemDet.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemDet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemDetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ItemDet
        [HttpPost]
        public async Task<ActionResult<WorkItemHistory>> PostItemDet(WorkItemHistory itemDet)
        {
            _context.WorkItemsHistory.Add(itemDet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorkItemHisotry), new { id = itemDet.Id }, itemDet);
        }

        // DELETE: api/ItemDet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemDet(int id)
        {
            var itemDet = await _context.WorkItemsHistory.FindAsync(id);
            if (itemDet == null)
            {
                return NotFound();
            }

            _context.WorkItemsHistory.Remove(itemDet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemDetExists(int id)
        {
            return _context.WorkItemsHistory.Any(e => e.Id == id);
        }
    }
}


