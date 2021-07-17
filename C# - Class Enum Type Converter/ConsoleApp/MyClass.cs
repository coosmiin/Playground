using System;
using Microsoft.Extensions.Options;

namespace ConsoleApp
{
	public class MyClass
	{
		private readonly MyOptions _options;

		public MyClass(IOptions<MyOptions> options)
		{
			_options = options?.Value ?? throw new ArgumentNullException(nameof(options));
		}

		public string GetValue() => _options.MyEnumValue;
	}
}
