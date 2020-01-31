using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace StatelessApi.Controllers
{
	[ApiController]
	public class DummyController : ControllerBase
	{
		[HttpGet("dummy")]
		public async Task<ActionResult> Get()
		{
			return Ok("ok");
		}
	}
}
