using DemoDataAccess.Entity;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Driver;
using NHibernate.Spatial.Dialect;
using NHibernate.Spatial.Mapping;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDataAccess
{
	public static class FluentConfiguration
	{
		public static void Configure(bool generateTables = false)
		{
			var cfg = Fluently.Configure()
				.Database(FluentNHibernate.Cfg.Db.MySQLConfiguration.Standard
				.ConnectionString(c => c.FromConnectionStringWithKey("MySQL"))
				.Driver<MySqlDataDriver>()
				.ShowSql()
				//.Dialect("NHibernate.Spatial.Dialect.MySQLSpatialDialect ,NHibernate.Spatial.MySQL"))
				.Dialect<MySQLSpatialDialect>())
				.Mappings(x => x.FluentMappings.AddFromAssemblyOf<MunicipalityMap>())
				.BuildConfiguration();

			cfg.AddAuxiliaryDatabaseObject(new SpatialAuxiliaryDatabaseObject(cfg));

			if (generateTables)
			{
				var exporter = new SchemaExport(cfg);
				exporter.Drop(false, true);
				exporter.Create(true, true);
			}

			SessionManager.SessionFactory = cfg.BuildSessionFactory();

		}
	}


	public static class SessionManager
	{
		private static ISession _session;
		public static ISessionFactory SessionFactory;
		public static ISession Session
		{
			get
			{
				if (_session == null)
				{
					if (SessionFactory == null)
					{
						FluentConfiguration.Configure();
					}				
					_session = SessionFactory.OpenSession();
				}
				return _session;
			}
		}
	}
}
