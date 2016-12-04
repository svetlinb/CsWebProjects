using Events.Data;
using Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Services
{
    public abstract class BaseService : IBaseService
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
       
        public ApplicationDbContext Context
        {
            get
            {
                return _context;
            }
        }
    }
}
