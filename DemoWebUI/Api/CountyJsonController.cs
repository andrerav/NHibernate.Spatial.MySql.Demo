using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoDataAccess;
using DemoDataAccess.Entity;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using NHibernate.Linq;

namespace DemoWebUI.Api
{
	public class CountyJsonController : JsonController
	{
		// GET api/<controller>
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		public HttpResponseMessage Get(int id)
		{
			var session = SessionManager.Session;
			var county = session.Get<County>(id);

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