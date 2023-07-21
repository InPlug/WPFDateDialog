using System;
using System.Windows;
using System.Windows.Input;

namespace View.Behaviors
{
    /// <summary>
    /// Bindet das Close-X des Windows an ein Command.
    /// </summary>
    /// <remarks>
    /// File: WindowBehaviours.cs
    /// Autor: Erik Nagel
    ///
    /// 18.04.2015 Erik Nagel: erstellt
    /// </remarks>
    public static class WindowBehaviours
    {
        private static DependencyProperty? _windowClosedCommandProperty;

        /// <summary>
        /// Attached Property für das Closed-Event eines Windows.
        /// </summary>
        public static readonly DependencyProperty WindowClosedCommandProperty =
            DependencyProperty.RegisterAttached("WindowClosedCommand",
            typeof(ICommand), typeof(WindowBehaviours),
            new PropertyMetadata(new PropertyChangedCallback(WindowClosedCallBack)));

        /// <summary>
        /// WPF-Setter für das WindowClosedCommand.
        /// </summary>
        /// <param name="obj">Das besitzende Control.</param>
        /// <param name="value">Das Command.</param>
        public static void SetWindowClosedCommand(UIElement obj, ICommand value)
        {
            obj.SetValue(WindowClosedCommandProperty, value);
        }

        /// <summary>
        /// WPF-Getter für das WindowClosedCommand.
        /// </summary>
        /// <param name="obj">Das besitzende Control.</param>
        /// <returns>Das Command.</returns>
        public static ICommand GetWindowClosedCommand(UIElement obj)
        {
            return (ICommand)obj.GetValue(WindowClosedCommandProperty);
        }

        static void WindowClosedCallBack(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Window element = (Window)obj;

            _windowClosedCommandProperty = args.Property;

            if (element != null)
            {
                if (args.OldValue != null)
                {
                    element.Closed += new EventHandler(WindowClosedEventHandler);
                }
                if (args.NewValue != null)
                {
                    element.Closed += new EventHandler(WindowClosedEventHandler);
                }
            }
        }

        /// <summary>
        /// Event-Handler für das Expanded-Event des Expanders.
        /// </summary>
        /// <param name="sender">Das Command.</param>
        /// <param name="e">Argumente.</param>
        public static void WindowClosedEventHandler(object? sender, EventArgs e)
        {
            DependencyObject? obj = (DependencyObject?)sender;

            ICommand? command = (ICommand?)obj?.GetValue(_windowClosedCommandProperty);
            if (command != null)
            {
                if (command.CanExecute(e))
                {
                    command.Execute(null);
                }
            }
        }
    }
}
