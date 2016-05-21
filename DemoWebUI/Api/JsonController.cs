using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace DemoWebUI.Api
{
	public class JsonController : ApiController
	{
		protected HttpResponseMessage Response(string jsonStr)
		{
			var response = this.Request.CreateResponse(HttpStatusCode.OK);
			response.Content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
			return response;
		}
	}
}