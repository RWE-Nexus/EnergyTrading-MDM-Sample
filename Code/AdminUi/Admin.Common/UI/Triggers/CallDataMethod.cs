namespace Common.UI.Triggers
{
    // (c) Copyright Microsoft Corporation.
    // This source is subject to the Microsoft Permissive License.
    // See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
    // All other rights reserved.
    using System.Reflection;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Interactivity;

    /// <summary>
    /// Invokes a method on the data context of this.
    /// </summary>
    public class CallDataMethod : TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// Backing DependencyProperty for Method.
        /// </summary>
        public static readonly DependencyProperty MethodProperty = DependencyProperty.Register(
            "Method", 
            typeof(string), 
            typeof(CallDataMethod), 
            new PropertyMetadata(null));

        /// <summary>
        /// Backing DependencyProperty for Target.
        /// </summary>
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            "Target", 
            typeof(Binding), 
            typeof(CallDataMethod), 
            new PropertyMetadata(null, HandleTargetBindingChanged));

        private readonly BindingListener listener = new BindingListener { Binding = new Binding(), };

        /// <summary>
        /// Name of the method to be called
        /// </summary>
        public string Method
        {
            get
            {
                return (string)this.GetValue(MethodProperty);
            }

            set
            {
                this.SetValue(MethodProperty, value);
            }
        }

        /// <summary>
        /// Binding to the object on which the method is to be called.
        /// </summary>
        public Binding Target
        {
            get
            {
                return (Binding)this.GetValue(TargetProperty);
            }

            set
            {
                this.SetValue(TargetProperty, value);
            }
        }

        /// <summary>
        /// Implementation of Invoke
        /// </summary>
        /// <param name="parameter"></param>
        protected override void Invoke(object parameter)
        {
            object bindingTarget;
            if (this.Target == null)
            {
                bindingTarget = this.AssociatedObject.DataContext;
            }
            else
            {
                bindingTarget = this.listener.Value;
            }

            if (bindingTarget != null)
            {
                MethodInfo method = bindingTarget.GetType().GetMethod(this.Method);
                if (method != null)
                {
                    ParameterInfo[] parameters = method.GetParameters();
                    if (parameters.Length == 0)
                    {
                        method.Invoke(bindingTarget, null);
                    }
                    else if (parameters.Length == 2 && this.AssociatedObject != null && parameter != null)
                    {
                        if (parameters[0].ParameterType.IsAssignableFrom(this.AssociatedObject.GetType())
                            && parameters[1].ParameterType.IsAssignableFrom(parameter.GetType()))
                        {
                            method.Invoke(bindingTarget, new[] { this.AssociatedObject, parameter });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Implementation of OnAttached.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            this.listener.Element = this.AssociatedObject;
        }

        /// <summary>
        /// Implementation of OnDetaching.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.listener.Element = null;
        }

        /// <summary>
        /// Notification that the TargetBinding property has changed.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTargetBindingChanged(DependencyPropertyChangedEventArgs e)
        {
            this.listener.Binding = (Binding)e.NewValue;
        }

        private static void HandleTargetBindingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((CallDataMethod)sender).OnTargetBindingChanged(e);
        }
    }
}