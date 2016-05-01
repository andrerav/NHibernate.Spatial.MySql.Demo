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

            //// Works, but Area is always null
            var county = session.Query<County>().First();

            //// Crashes with the error "No persister for: GeoAPI.Geometries.IGeometry"
            //var municipalities = session.Query<Municipality>().Select(m => m.Geom.ConvexHull()).ToList();

            var query =
                (from l in session.Query<County>()
                from np in session.Query<Municipality>()
                where np.Geom.Within(l.Geom) && l.CountyNo == 16
                select np).ToList();

            //var query2 =
            //    (from c in session.Query<County>()
            //     group c.Municipalities by c.CountyNo into g

            //    select new { CountyNo = g.Key, Hull =  }
            //    ).ToList();


            //var query2 = (from m in session.Query<Municipality>()
            //              group m by m.County into g
            //              select new { CountyNo = g.Key, Hull = }
                          
            //              )


            var query3 =
                (from l in session.Query<Municipality>()
                where l.County.Name == "Telemark"
                select l.Geom.ConvexHull()).ToList();


            var geom = session.Query<County>().Where(c => c.Name == "Telemark").First().Geom;

            var query4 = session.Query<Municipality>()
                                .Where(m => m.Geom.Within(geom))
                                .ToList();

        }
    }
}
