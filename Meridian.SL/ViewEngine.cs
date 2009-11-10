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

        private void LoadViews(Assembly assembly)
        {
            foreach (var viewType in assembly.GetTypes())
            {
                if (typeof (ViewPage).IsAssignableFrom(viewType) 
                    && (!viewType.IsAbstract))
                {
                    _viewTypeCache.Add(viewType.Name, new View() {ViewType = viewType});
                }
            }
        }

        public IView GetView(string viewName)
        {
            Requires.NotNullOrEmpty(viewName, "viewName");

            if (_viewTypeCache.ContainsKey(viewName))
            {
                return _viewTypeCache[viewName];
            }
            return null;
        }
    }
}