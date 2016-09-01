using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hassium.Runtime.Objects.Types
{
    public class HassiumLabel: HassiumObject
    {
        public static new HassiumTypeDefinition TypeDefinition = new HassiumTypeDefinition("label");

        public int Position { get; private set; }

        public HassiumLabel(int position)
        {
            Position = position;
            AddType(TypeDefinition);
            AddAttribute("position", new HassiumProperty(get_position, set_position));
            AddAttribute(HassiumObject.TOINT, ToInt, 0);
        }

        public HassiumInt get_position(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Position);
        }
        public HassiumNull set_position(VirtualMachine vm, params HassiumObject[] args)
        {
            Position = (int)args[0].ToInt(vm).Int;
            return HassiumObject.Null;
        }

        public override HassiumInt ToInt(VirtualMachine vm, params HassiumObject[] args)
        {
            return new HassiumInt(Position);
        }
    }
}
