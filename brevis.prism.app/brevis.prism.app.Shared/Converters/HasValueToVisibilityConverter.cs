using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace brevis.prism.app.Converters
{
    public class HasValueToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Visibility visibility = Visibility.Visible;
            try
            {
                if (value == null)
                {
                    visibility = Visibility.Collapsed;
                }

                if (parameter != null)
                {
                    bool InvertResult;
                    bool.TryParse(parameter.ToString(), out InvertResult);
                    if (InvertResult == true)
                    {
                        if (visibility == Visibility.Visible)
                        {
                            visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            visibility = Visibility.Visible;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Visibility.Convert", ex));
            }
            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
