using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MEESEES2.Controls
{
    public class CustomPicker : Picker
    {
        public static BindableProperty CornerRadiusProperty =
           BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(CustomPicker), 0);

        public static BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(CustomPicker), 0);

        public static BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(CustomPicker), new Thickness(5));

        public static BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomPicker), Color.Transparent);

        public static BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomPicker), string.Empty);

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        /// <summary>
        /// This property cannot be changed at runtime in iOS.
        /// </summary>
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
    }
}
