using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockawayWish.Database;

namespace RockawayWish.Database.Repositories
{
    public class DuesRepository : IDuesRepository
    {
        public DuesRepository()
        {
            _DBContext = new RockawayWishDBContext();
        }
        private RockawayWishDBContext _DBContext;

        //DUES
        public int InsertDues(int duesYear, decimal duesAmount)
        {
            return 0;
        }
        public int UpdateDues(int duesId, int duesYear, decimal duesAmount)
        {
            return 0;
        }
        public int DeleteDues(int duesId)
        {
            return 0;
        }


        //USERDUES
        public int InsertUserDues(Guid userId, int duesId, int paymentTypeId)
        {
            return 0;
        }
        public int DeleteUserDues(int userDuesId)
        {
            return 0;
        }


        //PAYMENTTYPE
        public int InsertPaymentType(string paymentTypeName)
        {
            return 0;
        }
        public int UpdatePaymentType(int paymentTypeId, string paymentTypeName)
        {
            return 0;
        }
        public int DeletePaymentType(int paymentTypeId)
        {
            return 0;
        }

    }
}
