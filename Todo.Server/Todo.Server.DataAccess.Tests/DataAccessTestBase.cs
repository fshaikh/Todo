using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Tests
{
    public abstract class DataAccessTestBase
    {
        protected ConnectionInfo GetConnectionInfo()
        {
            ConnectionInfo info = new ConnectionInfo { ConnectionString = "mongodb://localhost", DatabaseName = "TodoDb" };
            return info;
        }
    }
}
