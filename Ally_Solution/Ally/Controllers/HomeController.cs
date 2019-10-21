using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ally.Models;

namespace Ally.Controllers
{
  public class HomeController : Controller
  {
    private List<Bank> _banks;
    private List<Rating> _ratings;
    private List<Asset> _assets;
    private List<Limit> _limits;

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      SeedData();

      var viewModel = from b in _banks
                      join r in _ratings on b.Id equals r.BankId
                      join a in _assets on b.Id equals a.BankId
                      join l in _limits on b.Id equals l.BankId
                      select new BankViewModel { Bank = b, Rating = r, Asset = a, Limit = l };

      return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private void SeedData()
    {

      _banks = new List<Bank>
      {
        new Bank { Id = 1, Name = "Bank of America" },
        new Bank { Id = 2, Name = "Wells Fargo" },
        new Bank { Id = 3, Name = "Bank of Nova Scotia" },
        new Bank { Id = 4, Name = "Royal Bank of Canada" },
        new Bank { Id = 5, Name = "Montreal Bank" }
      };

      _ratings = new List<Rating>
      {
        new Rating { Id = 1, BankId = 1, BankRating = 7 },
        new Rating { Id = 2, BankId = 2, BankRating = -4 },
        new Rating { Id = 3, BankId = 3, BankRating = 2 },
        new Rating { Id = 4, BankId = 4, BankRating = -1 },
        new Rating { Id = 5, BankId = 5, BankRating = 9 }
      };

      _assets = new List<Asset>
      {
        new Asset { Id = 1, BankId = 1, TotalAssets = 1234000 },
        new Asset { Id = 2, BankId = 2, TotalAssets = 5657345 },
        new Asset { Id = 3, BankId = 3, TotalAssets = 2999002 },
        new Asset { Id = 4, BankId = 4, TotalAssets = 4346823 },
        new Asset { Id = 5, BankId = 5, TotalAssets = 15342679 }
      };

      _limits = new List<Limit>
      {
        new Limit { Id = 1, BankId = 1, BankLimit = CalculateLimit(7, 1234000), EntryDate = DateTime.Now.Date },
        new Limit { Id = 2, BankId = 2, BankLimit = CalculateLimit(-4, 5657345), EntryDate = DateTime.Now.Date },
        new Limit { Id = 3, BankId = 3, BankLimit = CalculateLimit(2, 2999002), EntryDate = DateTime.Now.Date },
        new Limit { Id = 4, BankId = 4, BankLimit = CalculateLimit(-1, 4346823), EntryDate = DateTime.Now.Date },
        new Limit { Id = 5, BankId = 5, BankLimit = CalculateLimit(9, 15342679), EntryDate = DateTime.Now.Date }
      };

    }
    private decimal CalculateLimit(int rating, decimal assets)
    {
      decimal limit = 0;

      switch (rating)
      {
        case -5:
        case -4:
        case -3:
          limit = assets * (decimal) .88;
          break;
        case -2:
        case -1:
        case 0:
          limit = assets * (decimal) .91;
          break;
        case 1:
        case 2:
        case 3:
          if (assets > 3000000)
          {
            limit = assets * (decimal)1.23;
          }
          else
          {
            limit = assets * (decimal)1.05;
          }
          break;
        case 4:
        case 5:
        case 6:
          if (assets > 3000000)
          {
            limit = assets * (decimal)1.23;
          }
          else
          {
            limit = assets * (decimal)1.08;
          }
          break;
        case 7:
        case 8:
        case 9:
        case 10:
          if (assets > 3000000)
          {
            limit = assets * (decimal)1.23;
          }
          else
          {
            limit = assets * (decimal)1.13;
          }
          break;
      }

      return Math.Round(limit);
    }
  }
}
