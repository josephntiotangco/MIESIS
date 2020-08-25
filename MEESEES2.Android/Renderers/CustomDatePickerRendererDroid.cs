using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MEESEES2.Controls;
using MEESEES2.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRendererDroid))]
namespace MEESEES2.Droid.Renderers
{
    public class CustomDatePickerRendererDroid : DatePickerRenderer
    {
        public CustomDatePickerRendererDroid(Context context) : base(context) { }
        public CustomDatePicker ElementV2 => Element as CustomDatePicker;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
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
                gd.SetCornerRadius(Context.ToPixels(ElementV2.CornerRadius));
                gd.SetStroke((int)Context.ToPixels(ElementV2.BorderThickness), ElementV2.BorderColor.ToAndroid());

                var padTop = (int)Context.ToPixels(ElementV2.Padding.Top);
                var padBottom = (int)Context.ToPixels(ElementV2.Padding.Bottom);
                var padLeft = (int)Context.ToPixels(ElementV2.Padding.Left);
                var padRight = (int)Context.ToPixels(ElementV2.Padding.Right);

                Control.SetPadding(padLeft, padTop, padRight, padBottom);

                Control.Gravity = GravityFlags.Center;
                Control.SetBackground(gd);
                //Control.SetBackgroundDrawable(gd);
                Control.LayoutParameters = layoutParams;
                //Control.SetPadding(0, 0, 0, 0);
                //SetPadding(0, 0, 0, 0);
            }
        }

    }
}