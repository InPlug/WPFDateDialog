using System;
using System.Windows.Data;
using System.Windows.Media;

namespace View.ValueConverter
{
    /// <summary>
    /// ValueConverter, wandelt einen nullable bool in Farben:
    /// null: gelb, true: grün, false: rot.
    /// </summary>
    /// <remarks>
    /// File: NullableBoolToBrush.cs
    /// Autor: Erik Nagel
    ///
    /// 05.01.2013 Erik Nagel: erstellt
    /// </remarks>
    [ValueConversion(typeof(bool?), typeof(Brush))]
    public class NullableBoolToBrush : IValueConverter
    {
        /// <summary>
        /// Wandelt einen nullable bool in Farben (SolidColorBrush):
        /// null: gelb, true: grün, false: rot.
        /// </summary>
        /// <param name="value">Nullable-Bool</param>
        /// <param name="targetType">Brush-Typ</param>
        /// <param name="parameter">Konvertierparameter</param>
        /// <param name="culture">Kultur</param>
        /// <returns>SolidColorBrush (gelb, grün, rot)</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool?)value == true)
            {
                return Brushes.Green;
            }
            else
            {
                if ((bool?)value == false)
                {
                    return Brushes.Red;
                }
                else
                {
                    return Brushes.Yellow;
                }
            }
        }

        /// <summary>
        /// Wandelt die Farben gelb, grün, rot (SolidColorBrush)
        /// in null, true, false.
        /// </summary>
        /// <param name="value">SolidColorBrush (gelb, grün, rot)</param>
        /// <param name="targetType">Brush-Typ</param>
        /// <param name="parameter">Konvertierparameter</param>
        /// <param name="culture">Kultur</param>
        /// <returns>Nullable-Bool</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
