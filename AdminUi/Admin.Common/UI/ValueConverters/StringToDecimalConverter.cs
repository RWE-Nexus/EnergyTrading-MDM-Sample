namespace Common.UI.ValueConverters
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// Solves the case where because of the "round trip" binding when the user has "0.0001" in the text box and presses backspace
    /// everything is deleted up to the "0".
    /// 
    /// What happens...
    /// ConvertBack "0.000"-> 0
    /// Convert 0 -> "0"
    /// 
    /// This is tedious if the user is trying to change, for example, "0.0001" to "0.0002" which is a common scenario.
    /// 
    /// It saves away the input string, if valid and sends it back to the UI
    /// </summary>
    [ValueConversion(typeof(Decimal), typeof(string))]
    public class StringToDecimalConverter : IValueConverter
    {
        private string stringValueFromConvertBack;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue;

            if (stringValueFromConvertBack != null)
            {
                stringValue = stringValueFromConvertBack;
                stringValueFromConvertBack = null;
            }
            else
            {
                var decimalValue = (decimal)value;
                stringValue = decimalValue.ToString("G28");
            }

            return stringValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal result;
            if (decimal.TryParse(value.ToString(), out result))
            {
                stringValueFromConvertBack = value.ToString();
                return result;
            }

            return 0M;
        }
    }

    /// <summary>
    /// Required validation rule now that we're not using the default behaviour
    /// </summary>
    public class DecimalValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal decimalValue;
            if (!decimal.TryParse(value.ToString(), out decimalValue))
            {
                return new ValidationResult(false, string.Empty);
            }

            return new ValidationResult(true, null);
        }
    }
}