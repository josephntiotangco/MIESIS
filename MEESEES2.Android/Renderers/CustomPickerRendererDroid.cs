using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;
using MEESEES2.Controls;
using MEESEES2.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRendererDroid))]
namespace MEESEES2.Droid.Renderers
{
    public class CustomPickerRendererDroid : PickerRenderer
    {
        public CustomPickerRendererDroid(Context context) : base(context) { }

        public CustomPicker picker => Element as CustomPicker;
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {

                Control.Background = null;
                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                GradientDrawable gd = new GradientDrawable();
                //gd.SetStroke(0, Android.Graphics.Color.Transparent);
                gd.SetColor(Element.BackgroundColor.ToAndroid());
                gd.SetCornerRadius(Context.ToPixels(picker.CornerRadius));
                gd.SetStroke((int)Context.ToPixels(picker.BorderThickness), picker.BorderColor.ToAndroid());

                var padTop = (int)Context.ToPixels(picker.Padding.Top);
                var padBottom = (int)Context.ToPixels(picker.Padding.Bottom);
                var padLeft = (int)Context.ToPixels(picker.Padding.Left);
                var padRight = (int)Context.ToPixels(picker.Padding.Right);

                Control.SetPadding(padLeft, padTop, padRight, padBottom);

                Control.Gravity = GravityFlags.Center;
                Control.SetBackground(gd);
                //Control.SetBackgroundDrawable(gd);
                Control.LayoutParameters = layoutParams;
                //Control.SetPadding(0, 0, 0, 0);
                //SetPadding(0, 0, 0, 0);
                ReflectPlaceholder();
            }
        }

        void ReflectPlaceholder()
        {
            if(picker.Placeholder != null)
            {
                Control.Hint = picker.Placeholder;
            }
        }
    }
}