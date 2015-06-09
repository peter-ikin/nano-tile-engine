using System;

namespace Nano.Engine.Sys
{
    public interface IServices
    {
        void AddService(Type type, object provider);

        void AddService<T>(T provider);

        object GetService(Type type);

        T GetService<T>() where T : class;

        void RemoveService(Type type);
    }
}

