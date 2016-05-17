using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockawayWish.Database;

namespace RockawayWish.Database.Repositories
{
    public interface IDuesRepository
    {
        int InsertDues(int duesYear, decimal duesAmount);
        int UpdateDues(int DuesId, int duesYear, decimal duesAmount);
        int DeleteDues(int DuesId);
    }
}
