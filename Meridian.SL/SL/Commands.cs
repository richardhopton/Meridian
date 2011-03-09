using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Meridian.SL
{
    public static class Commands
    {
        private static readonly DependencyProperty CommandButtonBehaviorProperty =
            DependencyProperty.RegisterAttached("CommandButtonBehavior", typeof(CommandButtonBehavior),
                                                typeof(Commands), null);

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(Commands),
            new PropertyMetadata(CommandPropertyChanged));

        public static ICommand GetCommand(ButtonBase buttonBase)
        {
            return (ICommand)buttonBase.GetValue(CommandProperty);
        }

        public static void SetCommand(ButtonBase buttonBase, ICommand value)
        {
            buttonBase.SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(Object), typeof(Commands),
            null);

        public static Object GetCommandParameter(ButtonBase buttonBase)
        {
            return buttonBase.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(ButtonBase buttonBase, Object value)
        {
            buttonBase.SetValue(CommandParameterProperty, value);                           
        }

        private static void CommandPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var element = o as ButtonBase;
            if (element != null)
            {
                if (e.OldValue != null)
                {
                    UnhookCommand(element, (ICommand)e.OldValue);
                }
                if (e.NewValue != null)
                {
                    HookCommand(element, (ICommand)e.NewValue);
                }
            }
        }

        private static void HookCommand(ButtonBase element, ICommand command)
        {
            var behavior = new CommandButtonBehavior(element, command);
            behavior.Attach();
            element.SetValue(CommandButtonBehaviorProperty, behavior);
        }

        private static void UnhookCommand(ButtonBase element, ICommand command)
        {
            var behavior = (CommandButtonBehavior)element.GetValue(CommandButtonBehaviorProperty);
            behavior.Dettach();
            element.ClearValue(CommandButtonBehaviorProperty);
        }

        private class CommandButtonBehavior
        {
            private readonly WeakReference elementReference;
            private readonly ICommand command;

            public CommandButtonBehavior(ButtonBase element, ICommand command)
            {
                this.elementReference = new WeakReference(element);
                this.command = command;
            }

            public void Attach()
            {
                ButtonBase element = GetElement();
                if (element != null)
                {
                    element.Click += element_Clicked;
                    command.CanExecuteChanged += command_CanExecuteChanged;
                    SetIsEnabled(element);
                }
            }

            public void Dettach()
            {
                command.CanExecuteChanged -= command_CanExecuteChanged;
                ButtonBase element = GetElement();
                if (element != null)
                {
                    element.Click -= element_Clicked;
                }
            }

            void command_CanExecuteChanged(Object sender, EventArgs e)
            {
                ButtonBase element = GetElement();
                if (element != null)
                {
                    SetIsEnabled(element);
                }
                else
                {
                    Dettach();
                }
            }

            private void SetIsEnabled(ButtonBase element)
            {
                element.IsEnabled = command.CanExecute(element.GetValue(Commands.CommandParameterProperty));
            }

            private static void element_Clicked(Object sender, EventArgs e)
            {
                DependencyObject element = (DependencyObject)sender;
                ICommand command = (ICommand)element.GetValue(CommandProperty);
                Object commandParameter = element.GetValue(CommandParameterProperty);
                command.Execute(commandParameter);
            }

            private ButtonBase GetElement()
            {
                return elementReference.Target as ButtonBase;
            }
        }
    }
}