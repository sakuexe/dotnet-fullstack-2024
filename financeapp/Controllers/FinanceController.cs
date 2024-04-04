using financeapp.Data;
using financeapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace financeapp.Controllers;

public class FinanceController : Controller
{
    private readonly FinancesContext _context;
    public FinanceController(FinancesContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Add(Finance finance)
    {
        // add the finance object to the database
        return RedirectToAction("Index");
    }
}

