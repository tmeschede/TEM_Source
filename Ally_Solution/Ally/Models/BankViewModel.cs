using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ally.Models
{
  public class BankViewModel
  {
    public Bank Bank { get; set; }
    public Rating Rating { get; set; }
    public Asset Asset { get; set; }
    public Limit Limit { get; set; }
  }
}
