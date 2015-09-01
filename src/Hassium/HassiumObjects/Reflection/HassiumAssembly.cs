using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;

namespace Hassium
{
    public class HassiumAssembly: HassiumObject
    {
        public Assembly Value { get; private set; }

        public HassiumAssembly(Assembly ass)
        {
            this.Value = ass;
            this.Attributes.Add("entryPoint", new InternalFunction(entryPoint));
            this.Attributes.Add("fullName", new InternalFunction(fullName));
            this.Attributes.Add("getFile", new InternalFunction(getFile));
            this.Attributes.Add("getFiles", new InternalFunction(getFiles));
            this.Attributes.Add("getModule", new InternalFunction(getModule));
            this.Attributes.Add("getModules", new InternalFunction(getModules));
            this.Attributes.Add("getName", new InternalFunction(getName));
            this.Attributes.Add("toString", new InternalFunction(toString));
        }

        private HassiumObject entryPoint(HassiumObject[] args)
        {
            return this.Value.EntryPoint.ToString();
        }

        private HassiumObject fullName(HassiumObject[] args)
        {
            return this.Value.FullName;
        }

        private HassiumObject getFile(HassiumObject[] args)
        {
            return new Hassium.HassiumObjects.IO.HassiumFileStream(this.Value.GetFile(args[0]));
        }

        private HassiumObject getFiles(HassiumObject[] args)
        {
            var result = new List<FileStream>();
            foreach (FileStream stream in this.Value.GetFiles())
                result.Add(stream);
            
            return new HassiumArray(result.ToArray());
        }

        private HassiumObject getModule(HassiumObject[] args)
        {
            return new HassiumModule(this.Value.GetModule(args[0]));
        }

        private HassiumObject getModules(HassiumObject[] args)
        {
            var result = new List<Module>();
            foreach (Module mod in this.Value.GetModules())
                result.Add(mod);

            return new HassiumArray(result.ToArray());
        }

        private HassiumObject getName(HassiumObject[] args)
        {
            return new HassiumString(this.Value.GetName().ToString());
        }

        private HassiumObject toString(HassiumObject[] args)
        {
            return this.Value.ToString();
        }
    }
}

