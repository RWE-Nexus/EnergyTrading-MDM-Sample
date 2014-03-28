namespace Common.AttachedProperties
{
    using System.Windows;
    using System.Windows.Controls;

    public class EventFocusExtension
    {
        public static readonly DependencyProperty ElementToFocusProperty =
            DependencyProperty.RegisterAttached(
                "ElementToFocus", 
                typeof(Control), 
                typeof(EventFocusExtension), 
                new UIPropertyMetadata(null, ElementToFocusPropertyChanged));

        public static void ElementToFocusPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                button.Click += (s, args) =>
                    {
                        Control control = GetElementToFocus(button);
                        if (control != null)
                        {
                            control.Focus();
                        }
                    };
            }
        }

        public static Control GetElementToFocus(Button button)
        {
            return (Control)button.GetValue(ElementToFocusProperty);
        }

        public static void SetElementToFocus(Button button, Control value)
        {
            button.SetValue(ElementToFocusProperty, value);
        }
    }
}