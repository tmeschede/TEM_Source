using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ally.Models
{
  public class Rating
  {
    public int Id { get; set; }
    public int BankId { get; set; }
    public Bank Bank { get; set; }
    public int BankRating { get; set; }
  }
}
