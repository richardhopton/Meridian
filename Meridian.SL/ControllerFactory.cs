using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Resources;

namespace Meridian.SL
{
    public class ControllerFactory : IControllerFactory
    {
        private Dictionary<string,Type> _controllerTypeCache = new Dictionary<string, Type>();

        public ControllerFactory()
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (AssemblyPart asmPart in Deployment.Current.Parts)
            {
                StreamResourceInfo sri = Application.GetResourceStream(new Uri(asmPart.Source, UriKind.Relative));
                Assembly asm = new AssemblyPart().Load(sri.Stream);
                LoadControllers(asm);
            } 
        }

        private void LoadControllers(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(IController).IsAssignableFrom(type)
                    && (type.Name.EndsWith("Controller"))
                    && (!type.IsAbstract))
                {
                    _controllerTypeCache.Add(type.Name, type);
                }
            }
        }

        public IController CreateController(string controllerName)
        {
            Requires.NotNullOrEmpty(controllerName, "controllerName");

            if (!controllerName.EndsWith("controller", StringComparison.InvariantCultureIgnoreCase))
            {
                controllerName += "Controller";
            }
            if (_controllerTypeCache.ContainsKey(controllerName))
            {
                return Activator.CreateInstance(_controllerTypeCache[controllerName]) as IController;
            }
            else
            {
                return null;
            }
        }
    }
}
