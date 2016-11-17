using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reolin.Web.Api.Infra.AppEvents
{
    public static class AppEventManager
    {
        public static void ExecuteHandlers(AppEventType type)
        {
            ExecuteHandlers(Assembly.GetExecutingAssembly(), type);
        }

        /// <summary>
        /// this method finds all classes that implement IAppEventHandler and executes the ones that match EventType
        /// </summary>
        /// <param name="assembly">the assembly to search from types</param>
        /// <param name="eventType">event type we are going to execute</param>
        public static void ExecuteHandlers(Assembly assembly, AppEventType eventType)
        {
            GetHandlers(assembly, eventType).ForEach(h => h.Execute());
        }

        private static IEnumerable<IAppEventHandler> GetHandlers(Assembly assembly, AppEventType eventType)
        {
            List<IAppEventHandler> result = new List<IAppEventHandler>();

            // find all types that implement IAppEventHandler interface and instantiate them based on interface
            assembly.GetTypes()
                    .Where(t => !t.IsInterface && typeof(IAppEventHandler).IsAssignableFrom(t))
                        .ForEach(t => result.Add((IAppEventHandler)Activator.CreateInstance(t)));
            return result;
        }

    }
}