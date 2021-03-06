﻿using System;
using System.ComponentModel.Composition;
using Common;

namespace HelloWorldComponent
{
    [Export(typeof(IConfigurableComponent))]
    public class HelloWorldComponent : IConfigurableComponent
    {
        public string Identifier { get; private set; }

        public void Init()
        {
            Identifier = "HelloWorldComponent";
            Console.WriteLine($"{Identifier} initialized!");
        }

        public void Terminate()
        {
            Console.WriteLine($"Terminating {Identifier}...");
        }
    }
}