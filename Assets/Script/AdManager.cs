using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using admob;

public class AdManager : MonoBehaviour
{
    private static AdManager _instance;

    public static AdManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AdManager>();
            }

            return _instance;
        }
    }
    Admob ad;
    string appID = "";
    string bannerID = "";
    string interstitialID = "";
    string videoID = "";

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
#if UNITY_IOS
        		 appID="ca-app-pub-3940256099942544~1458002511";
				 bannerID="ca-app-pub-3940256099942544/2934735716";
				 interstitialID="ca-app-pub-3940256099942544/4411468910";
				 videoID="ca-app-pub-3940256099942544/1712485313";
				 nativeBannerID = "ca-app-pub-3940256099942544/3986624511";
#elif UNITY_ANDROID
        appID = "ca-app-pub-7931299501522570~1049780687";
        bannerID = "ca-app-pub-7931299501522570/1888042023";
        interstitialID = "ca-app-pub-7931299501522570/5061000279";
        videoID = "ca-app-pub-3940256099942544/8691691433";
#endif
        AdProperties adProperties = new AdProperties();


        ad = Admob.Instance();
        ad.bannerEventHandler += onBannerEvent;
        ad.interstitialEventHandler += onInterstitialEvent;
        ad.rewardedVideoEventHandler += onRewardedVideoEvent;


    }


    public void ShowBanner()
    {
        Debug.Log("Showing Banner");
        Admob.Instance().showBannerRelative(bannerID, AdSize.SMART_BANNER, AdPosition.TOP_CENTER);
    }

    public void ShowInterstitial()
    {
        Debug.Log("Showing Video");
        if (ad.isInterstitialReady())
        {
            ad.showInterstitial();
        }
        else
        {
            ad.loadInterstitial(interstitialID);
        }
    }



    public void ShowRewardedVideo()
    {

        if (ad.isRewardedVideoReady())
        {
            ad.showRewardedVideo();
        }
        else
        {
            ad.loadRewardedVideo(videoID);
        }
    }

    void onInterstitialEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
        if (eventName == AdmobEvent.onAdLoaded)
        {
            Admob.Instance().showInterstitial();
        }
    }
    void onBannerEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobBannerEvent---" + eventName + "   " + msg);
    }
    void onRewardedVideoEvent(string eventName, string msg)
    {

        //Give Reward Here
        PlayerPrefs.SetInt("amount", 11);


    }
}
