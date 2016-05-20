using DemoDataAccess;
using DemoDataAccess.Entity;
using log4net.Config;
using NHibernate.Linq;
using NHibernate.Spatial.Criterion.Lambda;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQueryUtil
{
	class Program
	{
		static void Main()
		{
			// Log4net configuration
			// XmlConfigurator.Configure();

			// NHibernate configuration
			FluentConfiguration.Configure();

			var session = SessionManager.Session;

			#region Stuff
			//var result =
			//    (from county in session.Query<County>()
			//     from municipality in session.Query<Municipality>()
			//     where municipality.Geom.Within(county.Geom.Buffer(0.000001)) && county.Name == "Sogn og Fjordane"
			//     select municipality).ToList();


			//var result = session.Query<Municipality>().OrderByDescending(m => m.Geom.Area)
			//    .Select(m => new { m.Name, m.Geom.Area })
			//    .Take(10)
			//    .ToList();

			//var result =
			//    (from m1 in session.Query<Municipality>()
			//     select m1.Geom.Centroid)
			//     .Take(1)
			//     .ToList();

			//var result =
			//    (from c1 in session.Query<County>()
			//     from c2 in session.Query<County>()
			//     where c2 != c1 && c1.Id > c2.Id
			//     orderby c1.Geom.Centroid.Distance(c2.Geom.Centroid) ascending // descending
			//     select new { c1, c2, Distance = c1.Geom.Centroid.Distance(c2.Geom.Centroid) })
			//        .Take(5)
			//        .ToList();

			//// Warning!
			//var result =
			//    (from m1 in session.Query<Municipality>()
			//     from m2 in session.Query<Municipality>() where m2 != m1 && m1.Id > m2.Id
			//     orderby m1.Geom.Distance(m2.Geom) descending
			//     select new { m1, m2, Distance = m1.Geom.Distance(m2.Geom) })
			//     .Take(5)
			//     .ToList();


			//var municipality = session.Query<Municipality>().Single(m => m.Name == "Gloppen");
			//var result = session.QueryOver<County>()
			//        .WhereSpatialRestrictionOn(p => p.Geom)
			//        .Overlaps(municipality.Geom)
			//        .List();

			//// SELECT * FROM mysqldemo.municipality where County_id = 18
			//session.Transaction.Begin();
			//var municipalities = session.Query<Municipality>();
			//foreach (var m in municipalities)
			//{
			//	var counties = session.QueryOver<County>()
			//		.WhereSpatialRestrictionOn(p => p.Geom)
			//		.Overlaps(m.Geom)
			//		.List();
			//	if (counties.Count() > 1)
			//	{
			//		counties = counties.Where(c => c.Geom.IsValid && m.Geom.IsValid)
			//							.OrderByDescending(c => c.Geom.Intersection(m.Geom).Area)
			//								.ToList();
			//	}

			//	if (counties.Any() && m.County == null)
			//	{
			//		m.County = counties.First();
			//		m.County.Municipalities.Add(m);
			//		session.SaveOrUpdate(m);
			//	}
			//}
			//session.Transaction.Commit();

			#endregion
		}

	}
}
