package crc641b2272f076415f47;


public class InterstitialAdsDroid_AdInterstitialListener
	extends com.google.android.gms.ads.AdListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAdLoaded:()V:GetOnAdLoadedHandler\n" +
			"n_onAdClosed:()V:GetOnAdClosedHandler\n" +
			"n_onAdFailedToLoad:(I)V:GetOnAdFailedToLoad_IHandler\n" +
			"";
		mono.android.Runtime.register ("MEESEES2.Droid.Renderers.InterstitialAdsDroid+AdInterstitialListener, MEESEES2.Android", InterstitialAdsDroid_AdInterstitialListener.class, __md_methods);
	}


	public InterstitialAdsDroid_AdInterstitialListener ()
	{
		super ();
		if (getClass () == InterstitialAdsDroid_AdInterstitialListener.class)
			mono.android.TypeManager.Activate ("MEESEES2.Droid.Renderers.InterstitialAdsDroid+AdInterstitialListener, MEESEES2.Android", "", this, new java.lang.Object[] {  });
	}

	public InterstitialAdsDroid_AdInterstitialListener (com.google.android.gms.ads.InterstitialAd p0)
	{
		super ();
		if (getClass () == InterstitialAdsDroid_AdInterstitialListener.class)
			mono.android.TypeManager.Activate ("MEESEES2.Droid.Renderers.InterstitialAdsDroid+AdInterstitialListener, MEESEES2.Android", "Android.Gms.Ads.InterstitialAd, Xamarin.GooglePlayServices.Ads.Lite", this, new java.lang.Object[] { p0 });
	}


	public void onAdLoaded ()
	{
		n_onAdLoaded ();
	}

	private native void n_onAdLoaded ();


	public void onAdClosed ()
	{
		n_onAdClosed ();
	}

	private native void n_onAdClosed ();


	public void onAdFailedToLoad (int p0)
	{
		n_onAdFailedToLoad (p0);
	}

	private native void n_onAdFailedToLoad (int p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
