﻿using System.Windows;
using System.Windows.Controls;
using Meridian.SL.Navigation;

namespace Meridian.SL
{
    public class ViewPage : UserControl
    {
        private DelegateCommand<string> _submitCommand;

        public DelegateCommand<string> SubmitCommand
        {
            get
            {
                if (_submitCommand == null)
                {
                    _submitCommand = new DelegateCommand<string>(Submit, CanSubmit);
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

        public void Submit(string url)
        {
            NavigationService.Default().Navigate(url);
        }

        public string Title
        {
            get { return JournalEntry.GetName(this); }
            set { JournalEntry.SetName(this, value); }
        }
    }
}