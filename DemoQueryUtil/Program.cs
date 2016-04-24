using DemoDataAccess;
using DemoDataAccess.Entity;
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
		static void Main(string[] args)
		{
			FluentConfiguration.Configure();

			// Works, but Area is always null
			var county = SessionManager.Session.Query<County>().First();

			// Crashes with the error "No persister for: GeoAPI.Geometries.IGeometry"
			var municipalities = SessionManager.Session.Query<Municipality>()
										.Where(m => m.Area.Within(county.Area)).ToList();
		}
	}
}
