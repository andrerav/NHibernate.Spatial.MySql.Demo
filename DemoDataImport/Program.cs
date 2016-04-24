using DemoDataAccess;
using DemoDataAccess.Entity;
using GeoAPI.Geometries;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Newtonsoft.Json.Linq;
using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDataImport
{
	class Program
	{
		static void Main(string[] args)
		{

			FluentConfiguration.Configure(generateTables: true);
			SessionManager.Session.Transaction.Begin();
			ImportMunicipalities();
			ImportCounties();
			SessionManager.Session.Transaction.Commit();

		}

		private static void ImportCounties()
		{
			var countyFeatureCollection = new GeoJsonReader()
							.Read<FeatureCollection>(
								File.ReadAllText("./../../GeoData/fylker2.json")
							);


			foreach (var feature in countyFeatureCollection.Features)
			{
				var county = new County();
				county.Area = feature.Geometry;
				county.Name = feature.Attributes["NAVN"].ToString();
				county.CountyNo = Int32.Parse(feature.Attributes["FylkeNr"].ToString());
				SessionManager.Session.SaveOrUpdate(county);
			}
		}

		private static void ImportMunicipalities()
		{
			var municipalityFeatureCollection = new GeoJsonReader()
										.Read<FeatureCollection>(
											File.ReadAllText("./../../GeoData/kommuner2.json")
										);
			foreach (var feature in municipalityFeatureCollection.Features)
			{
				var municipality = new Municipality();
				municipality.Area = feature.Geometry;
				municipality.Name = feature.Attributes["NAVN"].ToString();
				municipality.MunicipalityNo = Int32.Parse(feature.Attributes["KOMM"].ToString());
				SessionManager.Session.SaveOrUpdate(municipality);
			}
		}
	}
}
