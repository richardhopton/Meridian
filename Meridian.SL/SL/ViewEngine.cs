using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Resources;

namespace Meridian.SL
{
    public class ViewEngine : IViewEngine
    {
        private Dictionary<string,View> _viewTypeCache = new Dictionary<string, View>();

        public ViewEngine()
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (AssemblyPart asmPart in Deployment.Current.Parts)
            {
                StreamResourceInfo sri = Application.GetResourceStream(new Uri(asmPart.Source, UriKind.Relative));
                Assembly asm = new AssemblyPart().Load(sri.Stream);
                LoadViews(asm);
            } 
        }

        //ToDo: Need to improve how we get assemblies
        private void LoadViews(Assembly assembly)
        {
            foreach (var viewType in assembly.GetTypes())
            {
                if (typeof (ViewPage).IsAssignableFrom(viewType) 
                    && (!viewType.IsAbstract))
                {
                    string[] names = viewType.FullName.Split('.');
                    string viewName = string.Format("{0}{1}", names[names.Length-2], names[names.Length - 1]);
                    _viewTypeCache.Add(viewName, new View() {ViewType = viewType});
                }
            }
        }

        public IView GetView(ControllerContext controllerContext, string viewName)
        {
            Requires.NotNullOrEmpty(viewName, "viewName");

            string controllerName = controllerContext.RequestContext.RouteData.GetRequiredString("controller");
            string fullViewName = string.Format("{0}{1}", controllerName, viewName);

            if (_viewTypeCache.ContainsKey(fullViewName))
            {                
                return _viewTypeCache[fullViewName];
            }
            return null;
        }
    }
}