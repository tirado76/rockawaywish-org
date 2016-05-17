using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RockawayWish.Database.Repositories;

namespace RockawayWish.Database.Services
{
    public class DuesService
    {
        public DuesService()
        {
            _Repository = new DuesRepository();
        }
        IDuesRepository _Repository;

        public int InsertDues(int duesYear, decimal duesAmount)
        {
            return _Repository.InsertDues(duesYear, duesAmount);
        }
        public int UpdateDues(int duesId, int duesYear, decimal duesAmount)
        {
            return _Repository.UpdateDues(duesId, duesYear, duesAmount);
        }
        public int DeleteDues(int duesId)
        {
            return _Repository.DeleteDues(duesId);
        }
        public int InsertUserDues(Guid userId, int duesId, int paymentTypeId)
        {
            return _Repository.InsertUserDues(userId, duesId, paymentTypeId);
        }
        public int DeleteUserDues(int userDuesId)
        {
            return _Repository.DeleteUserDues(userDuesId);
        }
    }
}
