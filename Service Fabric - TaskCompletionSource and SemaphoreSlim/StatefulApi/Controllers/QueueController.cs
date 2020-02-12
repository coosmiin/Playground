using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace StatefulApi.Controllers
{
	[ApiController]
	public class QueueController : ControllerBase
	{
		private readonly DemoQueue _queue;

		public QueueController(DemoQueue queue)
		{
			_queue = queue;
		}

		[HttpPut("add-item")]
		public ActionResult AddItemToQueue(string item)
		{
			_queue.AddItem(item);
			return Accepted();
		}

		[HttpGet("queue-size")]
		public ActionResult GetQueueSize()
		{
			return Ok(_queue.GetQueueSize().ToString());
		}
	}
}
