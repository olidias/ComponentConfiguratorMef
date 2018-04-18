using System;
using System.ComponentModel.Composition;
using Common;

namespace HelloUniverseComponent
{
    [Export(typeof(IConfigurableComponent))]
    public class HelloUniverseComponent : IConfigurableComponent
    {
        public string Identifier { get; private set; }
        public void Init()
        {
            Identifier = "HelloUniverseComponent";
            Console.WriteLine($"{Identifier} initialized...");
        }

        public void Terminate()
        {
            Console.WriteLine($"terminating {Identifier}...");
        }
    }
}