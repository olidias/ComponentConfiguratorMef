using System;
using System.ComponentModel.Composition;
using Common;

namespace HelloWorldComponent
{
    [Export(typeof(IConfigurableComponent))]
    public class HelloWorldComponent : IConfigurableComponent
    {
        public void Init()
        {
            Console.WriteLine("Hello World Component initialized!");
        }
    }
}