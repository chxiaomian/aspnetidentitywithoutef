using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace AspNet.Identity.NoEF.Data
{
    internal class BaseHandler
    {
        public Database db { get; private set; }

        public BaseHandler() 
        { 
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            db = factory.CreateDefault();
        }
    }
}
