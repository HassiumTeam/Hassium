using System.Linq;
using System.Reflection;
using Hassium.Functions;
using Hassium.HassiumObjects.IO;
using Hassium.HassiumObjects.Types;

namespace Hassium.HassiumObjects.Reflection
{
    public class HassiumAssembly: HassiumObject
    {
        public Assembly Value { get; private set; }

        public HassiumAssembly(Assembly ass)
        {
            Value = ass;
            Attributes.Add("entryPoint", new InternalFunction(entryPoint));
            Attributes.Add("fullName", new InternalFunction(fullName));
            Attributes.Add("getFile", new InternalFunction(getFile));
            Attributes.Add("getFiles", new InternalFunction(getFiles));
            Attributes.Add("getModule", new InternalFunction(getModule));
            Attributes.Add("getModules", new InternalFunction(getModules));
            Attributes.Add("getName", new InternalFunction(getName));
            Attributes.Add("toString", new InternalFunction(toString));
        }

        private HassiumObject entryPoint(HassiumObject[] args)
        {
            return Value.EntryPoint.ToString();
        }

        private HassiumObject fullName(HassiumObject[] args)
        {
            return Value.FullName;
        }

        private HassiumObject getFile(HassiumObject[] args)
        {
            return new HassiumFileStream(Value.GetFile(args[0]));
        }

        private HassiumObject getFiles(HassiumObject[] args)
        {
            return new HassiumArray(Value.GetFiles().ToArray());
        }

        private HassiumObject getModule(HassiumObject[] args)
        {
            return new HassiumModule(Value.GetModule(args[0]));
        }

        private HassiumObject getModules(HassiumObject[] args)
        {
            return new HassiumArray(Value.GetModules().ToArray());
        }

        private HassiumObject getName(HassiumObject[] args)
        {
            return new HassiumString(Value.GetName().ToString());
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return Value.ToString();
        }
    }
}

