using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RockawayWish.Database;
using RockawayWish.Models;

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

            try
            {
                if ((from tb in _DBContext.Dues where tb.DuesYear == duesYear
                     select tb).Count() > 0)
                {
                    //_UserModel.Message = "Dues year '" + duesYear.ToString() + "' already exists";
                    return 0;
                }
                else
                {
                    Dues objDues = new Dues
                    {
                        DuesYear = duesYear,
                        DuesAmount = duesAmount
                    };

                    _DBContext.Dues.Add(objDues);

                    return 1;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
            
            return 1;
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
