using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameAssistant.Core.Converters
{
    public class BoolToVisibilityConverter : MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value)
            {
                return "visible";
            }
            else
            {
                return "gone";
            }
        }
    }

    public class NotBoolToVisibilityConverter : MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!value)
            {
                return "visible";
            }
            else
            {
                return "gone";
            }
        }
    }
}
