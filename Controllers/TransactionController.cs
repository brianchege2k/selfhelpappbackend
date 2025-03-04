using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfHelpGroupAPI.Data;
using SelfHelpGroupAPI.Models;

namespace SelfHelpGroupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.Include(t => t.Member).ToListAsync();
        }

        // GET: api/transactions/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.Include(t => t.Member)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (transaction == null)
                return NotFound();

            return transaction;
        }

        // POST: api/transactions
        [HttpPost]
        public async Task<ActionResult<Transaction>> AddTransaction([FromBody] Transaction transaction)
        {
            // Validate the incoming transaction
            if (transaction == null)
                return BadRequest("Transaction data is required.");

            // Fetch the member by MemberId
            var member = await _context.Members.FindAsync(transaction.MemberId);
            if (member == null)
                return BadRequest("Member not found.");

            // Update member balance based on transaction type
            if (transaction.Type == "Deposit")
            {
                member.Balance += transaction.Amount;
            }
            else if (transaction.Type == "Withdrawal")
            {
                if (member.Balance < transaction.Amount)
                    return BadRequest("Insufficient balance.");

                member.Balance -= transaction.Amount;
            }
            else
            {
                return BadRequest("Invalid transaction type. Use 'Deposit' or 'Withdrawal'.");
            }

            // Ensure Member is null since it’s not provided by the client
            transaction.Member = null;

            // Add the transaction to the context
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
        }
    }
}