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
            catalog = new DirectoryCatalog("/home/od/HSR/APF/Uebung/ComponentConfigurator/Plugins/netcoreapp2.0/");
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
            
            InitComponents();
            
            
            while (true)
            {
                var s = Console.ReadLine();
                HandleCommand(s);
                Console.Write(">");
            }
        }

        private void InitComponents()
        {
            foreach (var component in _componentRepository)
            {
                component.Value.Init();
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
                case "quit":
                    Console.WriteLine("Quitting application...");
                    Environment.Exit(0);
                    break;
                default: 
                    Console.WriteLine("Command not recognised. ");
                    break;
            }
        }
    }
}