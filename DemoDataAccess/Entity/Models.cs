using FluentNHibernate.Mapping;
using GeoAPI.Geometries;
using NHibernate.Spatial.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDataAccess.Entity
{
	public class Municipality
	{
		public virtual int Id { get; set; }
		public virtual County County { get; set; }
		public virtual string Name { get; set; }
		public virtual int MunicipalityNo { get; set; }
		public virtual IGeometry Geom { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}

	public class MunicipalityMap : ClassMap<Municipality>
	{
		public MunicipalityMap()
		{
			ImportType<IGeometry>();
			Id(x => x.Id);
			Map(x => x.Name);
			Map(x => x.MunicipalityNo);
            Map(x => x.Geom).CustomType<MySQL57GeometryType>().Not.Nullable();
			References(x => x.County).Nullable();
		}
	}

	public class County
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual int CountyNo { get; set; }
		public virtual IGeometry Geom { get; set; }
		public virtual List<Municipality> Municipalities { get; set; } = new List<Municipality>();

		public override string ToString()
		{
			return Name;
		}
	}

	public class CountyMap : ClassMap<County>
	{
		public CountyMap()
		{
			ImportType<IGeometry>();
			Id(x => x.Id);
			Map(x => x.Name);
			Map(x => x.CountyNo);
			Map(x => x.Geom).CustomType<MySQL57GeometryType>().Not.Nullable();
		}
	}
}
