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
using Android.Gms.Ads;

using MEESEES2.Controls;
using MEESEES2.Commons;
using MEESEES2.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdmobControl),typeof(BannerAdsDroid))]
namespace MEESEES2.Droid.Renderers
{
    public class BannerAdsDroid : ViewRenderer<AdmobControl, AdView>
    {
        public BannerAdsDroid(Context context) : base(context) { }

        //public GetSmartBannerDpHeight()
        //{
        //    var dpHeight = Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density;

        //    if (dpHeight <= 400)
        //    {
        //        return 40;
        //    }
        //    if (dpHeight <= 720)
        //    {
        //        return 62;
        //    }
        //    return 102;
        //}

        //protected override void OnElementChanged(ElementChangeEventArgs<BannerAdsDroid> e)
        //{
        //    base.OnElementChanged(e);

        //    if (Control == null)
        //    {
        //        var adView = new AdView(Context)
        //        {
        //            AdSize = AdSize.SmartBanner,
        //            AdUnitId = Element.AdUnitId
        //        };

        //        var requestbuilder = new AdRequest.Builder();

        //        adView.LoadAd(requestbuilder.Build());
        //        e.NewElement.HeightRequest = GetSmartBannerDpHeight();

        //        SetNativeControl(adView);
        //    }
        //}

        string myAdID = Globals.BannerAdId;
        AdSize adSize = AdSize.SmartBanner;
        AdView adView;
        AdView CreateAdView()
        {
            if (adView != null) return adView;
            adView = new AdView(Context);
            adView.AdSize = adSize;
            adView.AdUnitId = myAdID;
            var adParams = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);

            adView.LayoutParameters = adParams;
            AdRequest adRequest = new AdRequest.Builder().Build();
            adView.LoadAd(adRequest);
            return adView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdmobControl> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                CreateAdView();
                SetNativeControl(adView);
            }
        }
    }
}