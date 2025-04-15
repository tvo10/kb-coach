using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KBCoach.Models;
using Microsoft.EntityFrameworkCore;

namespace KBCoach.Controllers;

public class HomeController : Controller
{
    private readonly DataContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public async Task<JsonResult> GetTransactions()
    {
        List<MiscModel.TransactionModel> transactions = await _context.Transactions.ToListAsync();
        return new JsonResult(transactions);
    }

    [HttpPost]
    public async Task<JsonResult> SaveTransaction([FromBody] MiscModel.TransactionModel transaction)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Invalid data" });
        }
        
        MiscModel.TransactionModel tm = _context.Transactions.FirstOrDefaultAsync(x => x.transaction_id == transaction.transaction_id).Result;

        if (tm == null)
        {
            // Save the transaction here
            _context.Transactions.Add(transaction);
        }
        else
        {
            tm.sender_number = transaction.sender_number;
            tm.receiver_number = transaction.receiver_number;
            tm.from_address = transaction.from_address;
            tm.to_address = transaction.to_address;
            tm.sent_date = transaction.sent_date;
            tm.product_code = transaction.product_code;
            tm.product_type = transaction.product_type;
            tm.product_price = transaction.product_price;
            tm.collector = transaction.collector;
            tm.total = transaction.total;
            tm.qty = transaction.qty;
            _context.Transactions.Update(tm);
        }
        
        await _context.SaveChangesAsync();

        return Json(new { success = true, message = "Transaction saved successfully!" });
    }

    [HttpPost]
    public async Task<JsonResult> DeleteTransaction(int id)
    {
        MiscModel.TransactionModel tm = await _context.Transactions.FirstOrDefaultAsync(x => x.transaction_id == id);

        if (tm != null)
        {
            _context.Transactions.Remove(tm);
            await _context.SaveChangesAsync();
        }
        
        return Json(new { success = true, message = "Transaction deleted successfully!" });
    }
    
    
}