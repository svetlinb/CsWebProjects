using Events.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Services
{
    public abstract class BaseService
    {
        protected ApplicationDbContext _context = new ApplicationDbContext();

        protected ApplicationDbContext Context
        {
            get
            {
                return _context;
            }
        }
    }
}
