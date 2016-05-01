using DemoDataAccess;
using DemoDataAccess.Entity;
using log4net.Config;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQueryUtil
{
	class Program
	{
		static void Main()
		{
            XmlConfigurator.Configure();

            FluentConfiguration.Configure();
            var session = SessionManager.Session;

            var query =
                (from l in session.Query<County>()
                from np in session.Query<Municipality>()
                where np.Geom.Within(l.Geom) && l.CountyNo == 16
                select np).ToList();
        }
    }
}
