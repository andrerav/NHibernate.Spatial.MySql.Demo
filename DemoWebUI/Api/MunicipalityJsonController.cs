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
		// GET api/<controller>
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
					Attributes = new AttributesTable()
				};

				feature.Attributes.AddAttribute("Id", municipality.Id);
				feature.Attributes.AddAttribute("Name", municipality.Name);
				feature.Attributes.AddAttribute("No", municipality.MunicipalityNo);

				collection.Add(feature);
			}

			var jsonSerializer = new GeoJsonSerializer();
			var sw = new StringWriter();
			jsonSerializer.Serialize(sw, collection);
			return Response(sw.ToString());
		}

		// TODO: Make this work
		public HttpResponseMessage GetCounty(int id)
		{
			var session = SessionManager.Session;
			var county =
				(from c in session.Query<County>()
					from m in session.Query<Municipality>()
					where m.Id == id && m.Geom.Within(c.Geom.Buffer(0.000001))
					select c)
					.ToList()
					.FirstOrDefault();


			var result =
				(from c2 in session.Query<County>()
				 from municipality in session.Query<Municipality>()
				 where municipality.Geom.Within(c2.Geom.Buffer(0.000001)) && municipality.Id == id
				 select c2).ToList().FirstOrDefault();

			var feature = new Feature
			{
				Geometry = county.Geom,
				Attributes = new AttributesTable()
			};

			feature.Attributes.AddAttribute("Id", county.Id);
			feature.Attributes.AddAttribute("Name", county.Name);
			feature.Attributes.AddAttribute("No", county.CountyNo);

			var jsonSerializer = new GeoJsonSerializer();
			var sw = new StringWriter();
			jsonSerializer.Serialize(sw, feature);
			return Response(sw.ToString());
		}


		// POST api/<controller>
		public void Post([FromBody]string value)
		{
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		public void Delete(int id)
		{
		}
	}
}