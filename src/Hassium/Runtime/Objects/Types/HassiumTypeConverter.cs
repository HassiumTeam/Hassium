using System;

namespace Hassium.Runtime.Objects.Types
{
	public class HassiumTypeConverter: HassiumObject
	{
		public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("TypeConverter");

		public HassiumTypeConverter()
		{
			AddType(TypeDefinition);
		}

		public HassiumList getBytes(VirtualMachine vm, params HassiumObject[] args)
		{
			HassiumList result = new HassiumList(new HassiumObject[0]);
			byte[] bytes;
			return result;
		}
	}
}