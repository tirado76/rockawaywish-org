using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockawish.Models
{
    public class PaymentType
    {
	    public int PaymentTypeId { get; set; }
	    public string PaymentTypeName  { get; set; }
	    public DateTime create_dt { get; set; }
    }
}
