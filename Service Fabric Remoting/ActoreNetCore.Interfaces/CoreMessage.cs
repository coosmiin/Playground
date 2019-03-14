using System.Runtime.Serialization;

namespace ActoreNetCore.Interfaces
{
	[DataContract]
	public class CoreMessage
	{
		[DataMember]
		public string Content { get; set; }

		[DataMember]
		public int Index { get; set; } 
	}
}
