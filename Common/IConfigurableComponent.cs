using System;

namespace Common
{
    public interface IConfigurableComponent
    {
        string Identifier { get; }
        void Init();
        void Terminate();
    }
}