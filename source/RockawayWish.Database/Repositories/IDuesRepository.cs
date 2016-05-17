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
        int UpdateDues(int duesId, int duesYear, decimal duesAmount);
        int DeleteDues(int duesId);
        int InsertUserDues(string userId, int duesId, int paymentTypeId);
        int DeleteUserDues(int userDuesId);
    }
}
