using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Markup;

namespace TaskList.Converters
{
    public class EnumDisplayNameConverter : MarkupExtension, IValueConverter
    {
        private static EnumDisplayNameConverter _instance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var enumValue = (Enum)value;
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.Name ?? enumValue.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new EnumDisplayNameConverter());
        }
    }
}
