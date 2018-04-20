using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Common;

namespace ComponentConfigurator
{
    public class ComponentConfigurator
    {
        private CompositionContainer _container;
        private DirectoryCatalog catalog;
        
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<IConfigurableComponent>> _componentRepository;

        public ComponentConfigurator()
        {
            catalog = new DirectoryCatalog("../Plugins/netcoreapp2.0/");
            _container = new CompositionContainer(catalog);
            _container.ComposeParts(this);
            _container.ExportsChanged += (sender, args) =>
                Console.WriteLine($"New component Registered!");
        }
        
        private void RefreshCatalog()
        {
            catalog.Refresh();
        }

        public void Run()
        {
            Console.WriteLine("-----\n\tComponent Configurator 1.0\n-----");
            Console.Write(">");
            
//            _componentRepository.ToList().ForEach(c=>c.Value.Init());
            
            while (true)
            {
                var s = Console.ReadLine();
                HandleCommand(s);
                Console.Write(">");
            }
        }

        private void HandleCommand(string s)
        {
            RefreshCatalog();
            switch (s)
            {
                case "show":
                    Console.WriteLine("Showing all registered Components:\n-------------");
                    if (!_componentRepository.Any())
                    {
                        Console.WriteLine("No Components registered\n-------------");
                        break;
                    }
                    _componentRepository.ToList().ForEach(c => Console.WriteLine(c.Value.ToString()));
                    Console.WriteLine("-------------");
                    break;
                case "init all":
                    _componentRepository.ToList().ForEach(c=>c.Value.Init());
                    break;
                case "remove":
                    HandleRemove();
                    break;
                case "quit":
                    Console.WriteLine("Quitting application...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Command not recognised. ");
                    break;
            }
        }

        private void HandleRemove()
        {
            Console.WriteLine("Type which Components you want to be removed (Nr):");
            for (int i = 0; i < _componentRepository.Count(); i++)
            {
                Console.WriteLine($"\t({i}){_componentRepository.ToList()[i].Value.Identifier}");    
            }

            var strIndex = Console.ReadLine();
            if (!int.TryParse(strIndex, out var intIndex))
            {
                Console.WriteLine("Invalid input.. returning");
                return;
            };
            if (intIndex < _componentRepository.Count() && intIndex >= 0)
            {
                _componentRepository.ToList()[intIndex].Value.Terminate();
            }
            else
            {
                Console.WriteLine("Given number was out of range");
            }

        }
    }
}