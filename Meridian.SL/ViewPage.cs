﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Meridian.SL.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;

namespace Meridian.SL
{
    public class ViewPage : UserControl
    {
        private DelegateCommand<Request> _submitCommand;

        public DelegateCommand<Request> SubmitCommand
        {
            get
            {
                if (_submitCommand == null)
                {
                    _submitCommand = new DelegateCommand<Request>(Submit, CanSubmit);
                }
                return _submitCommand;
            }
            set { _submitCommand = value; }
        }

        public IMvcHandler Handler { get; set; }

        public ViewDataDictionary ViewData { get; set; }

        public object Model
        {
            get
            {
                if (ViewData != null)
                {
                    return ViewData.Model;
                }
                return null;
            }
        }

        public bool CanSubmit(object parameter)
        {
            return true;
        }

        public void Submit(Request request)
        {
            if (request != null)
            {
                NavigationService.Default().Navigate(request.Url, 
                    request.Parameters.ToRequestParameters(this),
                    RequestVerbs.Submit);
            }
        }

        public string Title
        {
            get { return JournalEntry.GetName(this); }
            set { JournalEntry.SetName(this, value); }
        }
    }
}