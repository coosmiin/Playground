using System.ComponentModel;

namespace ConsoleApp
{
	[TypeConverter(typeof(MyEnumValueTypeConverter))]
	public class MyEnumValue
	{
		public static MyEnumValue EnumTypeValue1 = new("enumTypeValue1");

		public string Name { get; set; }

		public MyEnumValue(string name) => Name = name;

		public static implicit operator string(MyEnumValue enumValue) => enumValue.Name;
	}
}
