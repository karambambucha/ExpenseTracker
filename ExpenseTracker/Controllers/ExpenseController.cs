﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using System.Security.Claims;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses([FromQuery] string filter)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var expenses = _context.Expenses.Where(e => e.UserId == userId);

            switch (filter)
            {
                case "Week":
                    expenses = expenses.Where(e => e.Date >= DateTime.Now.AddDays(-7));
                    break;
                case "Month":
                    expenses = expenses.Where(e => e.Date >= DateTime.Now.AddMonths(-1));
                    break;
                case "3 months":
                    expenses = expenses.Where(e => e.Date >= DateTime.Now.AddMonths(-3));
                    break;
            }

            return await expenses.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(string description, decimal amount)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var expense = new Expense()
            {
                UserId = userId,
                Description = description,
                Amount = amount,
                Date = DateTime.UtcNow
            };
            _context.Entry(expense).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExpenses), new { id = expense.Id }, expense);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (id != expense.Id || userId != expense.UserId)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null || expense.UserId != userId)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
