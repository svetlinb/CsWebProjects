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
        protected ApplicationDbContext Context
        {
            get
            {
                return new ApplicationDbContext();
            }
        }
    }
}
