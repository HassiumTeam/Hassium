using System;

using Hassium.Runtime;

namespace HassiumTestDll
{
    public class TestModule : InternalModule
    {
        public TestModule() : base("TestModule")
        {
            AddAttribute("TestClass", new TestClass());
        }
    }
}

