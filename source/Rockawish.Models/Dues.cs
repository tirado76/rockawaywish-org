using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockawayWish.Models
{
    public class Dues
    {
	    public int DuesId { get; set; }
	    public int DuesYear	 { get; set; }
	    public decimal DuesAmount { get; set; }
	    public DateTime create_dt { get; set; }
    }
}
