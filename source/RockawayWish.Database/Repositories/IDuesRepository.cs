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
        //DUES
        int InsertDues(int duesYear, decimal duesAmount);
        int UpdateDues(int duesId, int duesYear, decimal duesAmount);
        int DeleteDues(int duesId);

        //USERDUES
        int InsertUserDues(Guid userId, int duesId, int paymentTypeId);
        int DeleteUserDues(int userDuesId);

        //PAYMENTTYPE
        int InsertPaymentType(string paymentTypeName);
        int UpdatePaymentType(int paymentTypeId, string paymentTypeName);
        int DeletePaymentType(int paymentTypeId);
    }
}
