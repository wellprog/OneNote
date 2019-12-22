using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OneNote.Helpers
{
    public class ClassLoader
    {
        //Create Singleton
        private ClassLoader() { }
        public static ClassLoader Instance { get; } = new ClassLoader();

        Dictionary<Type, object> storage { get; } = new Dictionary<Type, object>();

        public void Register(object element)
        {
            Type t = element.GetType();
            storage[t] = element;
        }

        public void Register(Type T)
        {
            object o = Activator.CreateInstance(T);
            storage[T] = o;
        }

        public void Register(Type T, object o)
        {
            if (o.GetType().GetInterface(T.FullName) == null)
                return;

            storage[T] = o;
        }

        public void Register<T>(object o) where T : class
        {
            var type = typeof(T);

            if (o.GetType().GetInterface(type.FullName) == null)
                return;

            storage[type] = o;
        }

        public T GetElement<T>() where T : class
        {
            var type = typeof(T);
            if (storage.Keys.Contains(type))
            {
                return storage[type] as T;
            }

            return null;
        }
    }
}
