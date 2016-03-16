using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DbAccess
{
    internal class DatabaseContext
    {
        private static EFDbContext databaseContext;
        private static object flag = new object();

        private DatabaseContext()
        {
            
        }

        public static EFDbContext GetContext()
        {
            if (databaseContext == null)
            {
                lock (flag)
                {
                    if (databaseContext == null)
                    {
                        databaseContext = new EFDbContext();
                    }
                }
            }

            return databaseContext;
        }
    }
}
