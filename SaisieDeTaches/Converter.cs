using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SaisieDeTaches
{
    /// <summary>
    /// Converti le mode Edition en booléen
    /// </summary>
    public class ConvModeEditActivation : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (ModesEdition)value == ModesEdition.Edition ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception();
        }
    }

    /// <summary>
    /// Converti le mode Consultation en booléen
    /// </summary>
    public class ConvModeEditLectureSeule  : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (ModesEdition)value == ModesEdition.Consultation ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception();
        }
    }


    /// <summary>
    /// Converti un booléen en X
    /// </summary>
    public class BoolToCrossConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value == false ? " " : "X";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConvModeEditToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (ModesEdition)value == ModesEdition.Edition ? "(mode édition)" : " "; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
