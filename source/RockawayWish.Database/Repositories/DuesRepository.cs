using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockawayWish.Database;

namespace RockawayWish.Database.Repositories
{
    public class DuesRepository
    {
        public DuesRepository()
        {
            _DBContext = new RockawayWishDBContext();
        }
        private RockawayWishDBContext _DBContext;
    }
}
