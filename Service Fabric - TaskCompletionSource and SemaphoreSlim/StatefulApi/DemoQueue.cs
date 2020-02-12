using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StatefulApi
{
	public class DemoQueue
	{
		private SemaphoreSlim _semaphore = new SemaphoreSlim(0);
		private Queue<string> _queue = new Queue<string>();

		public void AddItem(string item)
		{
			_queue.Enqueue(item);

			_semaphore.Release();
		}

		public async Task ProcessQueueAsync(CancellationToken token) 
		{
			await _semaphore.WaitAsync(token);

			while (_queue.TryDequeue(out string item))
			{ // do something with the item
			}
		}

		public int GetQueueSize()
		{
			return _queue.Count;
		}
	}
}