namespace Common.InteractionRequest
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

    public class InteractionDialogAction : TriggerAction<Grid>
    {
        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register(
                "ContentTemplate", 
                typeof(DataTemplate), 
                typeof(InteractionDialogAction), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty DialogProperty = DependencyProperty.Register(
            "Dialog", 
            typeof(InteractionDialogBase), 
            typeof(InteractionDialogAction), 
            new PropertyMetadata(null));

        public DataTemplate ContentTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(ContentTemplateProperty);
            }

            set
            {
                this.SetValue(ContentTemplateProperty, value);
            }
        }

        public InteractionDialogBase Dialog
        {
            get
            {
                return (InteractionDialogBase)this.GetValue(DialogProperty);
            }

            set
            {
                this.SetValue(DialogProperty, value);
            }
        }

        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                return;
            }

            InteractionDialogBase dialog = this.GetDialog(args.Context);
            Action callback = args.Callback;

            EventHandler handler = null;

            handler = (s, e) =>
                {
                    dialog.Closed -= handler;
                    this.AssociatedObject.Children.Remove(dialog);
                    callback();
                };

            dialog.Closed += handler;
        }

        private InteractionDialogBase GetDialog(Notification notification)
        {
            InteractionDialogBase dialog = this.Dialog;
            dialog.DataContext = notification;
            dialog.MessageTemplate = this.ContentTemplate;

            dialog.SetValue(
                Grid.RowSpanProperty, 
                this.AssociatedObject.RowDefinitions.Count == 0 ? 1 : this.AssociatedObject.RowDefinitions.Count);
            dialog.SetValue(
                Grid.ColumnSpanProperty, 
                this.AssociatedObject.ColumnDefinitions.Count == 0 ? 1 : this.AssociatedObject.ColumnDefinitions.Count);
            this.AssociatedObject.Children.Add(dialog);
            return dialog;
        }
    }
}