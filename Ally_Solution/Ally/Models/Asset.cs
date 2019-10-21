using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ally.Models
{
  public class Asset
  {
    public int Id { get; set; }
    public int BankId { get; set; }
    public Bank Bank { get; set; }

    [DisplayFormat(DataFormatString = "{0:C0}")]
    public decimal TotalAssets { get; set; }
  }
}
