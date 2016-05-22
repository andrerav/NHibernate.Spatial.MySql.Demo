using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using DemoDataAccess;
using DemoDataAccess.Entity;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using NHibernate.Linq;

namespace DemoWebUI.Api
{
	public class MunicipalityJsonController : JsonController
	{
		/// <summary>
		/// Returns a list of municipalities in GeoJSON format
		/// </summary>
		/// <returns></returns>
		public HttpResponseMessage Get()
		{
			var session = SessionManager.Session;

			var municipalities = session.Query<Municipality>().ToList();

			FeatureCollection collection = new FeatureCollection();

			foreach (var municipality in municipalities)
			{
				var feature = new Feature
				{
					Geometry = municipality.Geom,
					Attributes = new AttributesTable(),
				};

				feature.Attributes.AddAttribute("id", municipality.Id);
				feature.Attributes.AddAttribute("name", municipality.Name);
				feature.Attributes.AddAttribute("no", municipality.MunicipalityNo);
				feature.Attributes.AddAttribute("type", "municipality");

				collection.Add(feature);
			}

			var jsonSerializer = new GeoJsonSerializer();
			var sw = new StringWriter();
			jsonSerializer.Serialize(sw, collection);
			return Response(sw.ToString());
		}

		/// <summary>
		/// Returns the county closes to the given municipality
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public HttpResponseMessage GetCounty(int id)
		{
			var session = SessionManager.Session;
			var county =
				(from c in session.Query<County>()
				 from m in session.Query<Municipality>()
				 where m.Id == id
				 orderby m.Geom.Centroid.Distance(c.Geom) ascending 
				 select c)
					.Take(1)
					.ToList()
					.FirstOrDefault();

			var feature = new Feature
			{
				Geometry = county.Geom,
				Attributes = new AttributesTable()
			};

			feature.Attributes.AddAttribute("id", county.Id);
			feature.Attributes.AddAttribute("name", county.Name);
			feature.Attributes.AddAttribute("no", county.CountyNo);
			feature.Attributes.AddAttribute("type", "county");

			var jsonSerializer = new GeoJsonSerializer();
			var sw = new StringWriter();
			jsonSerializer.Serialize(sw, feature);
			return Response(sw.ToString());
		}
	}
}