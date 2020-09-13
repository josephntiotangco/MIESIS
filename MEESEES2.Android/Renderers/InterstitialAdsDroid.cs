using System;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Ads;

using MEESEES2.Droid;
using MEESEES2.Droid.Renderers;
using MEESEES2.Services;
using Xamarin.Forms;
using MEESEES2.Commons;

[assembly: Dependency(typeof(InterstitialAdsDroid))]
namespace MEESEES2.Droid.Renderers
{
    public class InterstitialAdsDroid : IInterstitialAds
    {
        public Task Display(string adID)
        {
            var displayTask = new TaskCompletionSource<bool>();
            InterstitialAd AdInterstitial = new InterstitialAd(context: Android.App.Application.Context)
            {
                AdUnitId = adID
            };

            {
                var adInterstitialListener = new AdInterstitialListener(AdInterstitial)
                {
                    AdClosed = () =>
                    {
                        if (displayTask != null)
                        {
                            displayTask.TrySetResult(AdInterstitial.IsLoaded);
                            displayTask = null;
                        }
                    },
                    AdFailed = () =>
                    {
                        if (displayTask != null)
                        {
                            displayTask.TrySetResult(AdInterstitial.IsLoaded);
                            displayTask = null;
                        }
                    }
                };

                AdRequest.Builder requestBuilder = new AdRequest.Builder();
                AdInterstitial.AdListener = adInterstitialListener;
                AdInterstitial.LoadAd(requestBuilder.Build());
            }

            return Task.WhenAll(displayTask.Task);
        }

        public class AdInterstitialListener : AdListener
        {
            private readonly InterstitialAd _interstitialAd;

            public AdInterstitialListener(InterstitialAd interstitialAd)
            {
                _interstitialAd = interstitialAd;
            }

            public Action AdLoaded { get; set; }
            public Action AdClosed { get; set; }
            public Action AdFailed { get; set; }

            public override void OnAdLoaded()
            {
                base.OnAdLoaded();

                if (_interstitialAd.IsLoaded)
                {
                    _interstitialAd.Show();
                }
                AdLoaded?.Invoke();
                Globals.PopAdCount = 1;
            }

            public override void OnAdClosed()
            {
                base.OnAdClosed();
                AdClosed?.Invoke();
                Globals.isAdClosed = true;
            }

            public override void OnAdFailedToLoad(int errorCode)
            {
                base.OnAdFailedToLoad(errorCode);
                AdFailed?.Invoke();
                Globals.isAdClosed = false;
            }
        }
    }
}