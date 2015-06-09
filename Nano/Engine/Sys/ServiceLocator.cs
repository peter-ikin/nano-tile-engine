using System;
using Microsoft.Xna.Framework;

namespace Nano.Engine.Sys
{
    /// <summary>
    /// ServiceLocator class.
    /// Provides a wrapper around the GameServiceContainer class and allows other classes to 
    /// be programmed to the IServices interface rather than need instances of the Game 
    /// class to be passed around to get access to the service container.
    /// </summary>
    public class ServiceLocator : IServices
    {
        private GameServiceContainer m_Container;

        private static ServiceLocator instance;

        private ServiceLocator() {}

        /// <summary>
        /// Gets the instance of the ServiceLocator class as a reference to an IServices interface.
        /// </summary>
        /// <value>The services interface</value>
        public static IServices Services
        {
            get 
            {
                if (instance == null)
                {
                    instance = new ServiceLocator();
                }
                return instance;
            }
        }

        /// <summary>
        /// Gets the instance of the ServiceLocator.
        /// </summary>
        /// <value>The instance.</value>
        public static ServiceLocator Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new ServiceLocator();
                }
                return instance;
            }
        }

        /// <summary>
        /// Sets the service container. i.e. the GameServiceContainer instance
        /// Usually this instance is obtained from the XNA Game class.
        /// </summary>
        /// <param name="container">The GameServiceContainer.</param>
        public void SetServiceContainer(GameServiceContainer container)
        {
            m_Container = container;
        }

        public void AddService(Type type, object provider)
        {
            m_Container.AddService(type, provider);
        }

        public void AddService<T>(T provider)
        {
            m_Container.AddService<T>(provider);
        }

        public object GetService(Type type)
        {   
            return m_Container.GetService(type);
        }

        public T GetService<T>() where T : class
        {
            return m_Container.GetService<T>();
        }

        public void RemoveService(Type type)
        {
            m_Container.RemoveService(type);
        }
    }
}

