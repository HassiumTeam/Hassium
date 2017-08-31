using System;

using Hassium.Compiler;
using Hassium.Runtime;
using Hassium.Runtime.Types;

namespace HassiumTestDll
{
    public class TestClass : HassiumObject
    {
        public TestClass()
        {
            AddAttribute("testfunc", testfunc, 1);
        }

        [DocStr(
            "@desc Tests the DLL importing features of Hassium.",
            "@param nothing A test parameter that does nothing.",
            "@returns 'I return things!'."
            )]
        [FunctionAttribute("func testfunc (nothing : object) : string")]
        public static HassiumString testfunc(VirtualMachine vm, HassiumObject self, SourceLocation location, params HassiumObject[] args)
        {
            Console.WriteLine("Hello from TestModule.TestClass.testfunc!");

            return new HassiumString("I return things!");
        }
    }
}

