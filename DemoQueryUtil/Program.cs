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
			// Works, but Area is null
			var county = SessionManager.Session.Query<County>().First();

			// Crashes
			var municipalities = SessionManager.Session.Query<Municipality>()
										.Where(m => m.Area.Within(county.Area)).ToList();

		}
	}
}
