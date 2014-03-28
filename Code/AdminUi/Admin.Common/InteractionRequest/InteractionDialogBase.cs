namespace Common.InteractionRequest
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class InteractionDialogBase : UserControl
    {
        public static readonly DependencyProperty MessageTemplateProperty =
            DependencyProperty.Register(
                "MessageTemplate", 
                typeof(DataTemplate), 
                typeof(InteractionDialogBase), 
                new PropertyMetadata(null));

        public event EventHandler Closed;

        public DataTemplate MessageTemplate
        {
            get
            {
                return (DataTemplate)this.GetValue(MessageTemplateProperty);
            }

            set
            {
                this.SetValue(MessageTemplateProperty, value);
            }
        }

        public void Close()
        {
            this.OnClose(EventArgs.Empty);
        }

        protected virtual void OnClose(EventArgs e)
        {
            EventHandler handler = this.Closed;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}