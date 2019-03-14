using System.Runtime.Serialization;

namespace ActorNetFramework.Interfaces
{
	[DataContract]
	public class FrameworkMessage
	{
		[DataMember]
		public string Content { get; set; }

		[DataMember]
		public int Index { get; set; } 
	}
}
