using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    private BannerView bannerView;

    private InterstitialAd interstitial;

    private RewardedAd rewardedAd;

    public Action acVideoComplete;


    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
        this.RequestInterstitial();
        this.RequesRewarded();
    }


    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void ShowVideoReward()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }
    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }
    //public void GameOver()
    //{
    //    if (this.interstitial.IsLoaded())
    //    {
    //        this.interstitial.Show();
    //    }
    //}


    private void RequesRewarded()
    {
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        this.rewardedAd = new RewardedAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.OnUserEarnedReward += RewardedAd_OnUserEarnedReward;
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    private void RewardedAd_OnUserEarnedReward(object sender, Reward e)
    {
        if (acVideoComplete != null)
        {
            acVideoComplete();
        }
    }
    //public void GameOver2()
    //{
    //    if (this.rewardedAd.IsLoaded())
    //    {
    //        this.rewardedAd.Show();
    //    }
    //}
}
