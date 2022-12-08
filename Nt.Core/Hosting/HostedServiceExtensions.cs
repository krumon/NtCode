using System;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Extension method for <see cref="IHostedService"/> objects.
    /// </summary>
    public static class HostedServiceExtensions
    {
        /// <summary>
        /// Gets a specific object in array passed by parameter. If the object is find returns true, otherwise return false and the object is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects">The object collection where find a specific type of object.</param>
        /// <param name="findObject">The find object. If the type of the object doesn't exist returns null.</param>
        /// <returns>True if the type of the object is find, otherwise false.</returns>
        public static bool TryGet<T>(this IHostedService hostedService, object[] objects, out T findObject)
            where T : class
        {
            T searchObject = null;
            bool find = false;
            try
            {
                if (objects == null)
                    throw new ArgumentNullException(nameof(objects));

                if (objects.Length == 0)
                    throw new ArgumentException("The ninjascript objects array cannot be empty.");

                foreach (var o in objects)
                    if (o is T t)
                    {
                        searchObject = t;
                        find = true;
                        break;
                    }
            }
            catch
            {
            }

            findObject = searchObject;

            return find;
        }
    }
}
