using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class ggAdmos : MonoBehaviour
{
    // private InterstitialAd intertitial_Ad;
    // private RewardedAd rewardedAd;
    // private BannerView banner;

    // private string banner_ID;
    // private string interstitial_Ad_ID;
    // private string rewarded_ID;

    // private void Start()
    // {
    //     banner_ID = "ca-app-pub-3940256099942544/6300978111";
    //     interstitial_Ad_ID = "ca-app-pub-3940256099942544/1033173712";
    //     rewarded_ID = "ca-app-pub-3940256099942544/5224354917";

    //     MobileAds.Initialize(initStatus => { });
    //     RequesBanner();
    //     RequestInterstitial();
    //   //  Showinterstitial();
    //     RequestRewarrdedVideo();
    //   //  ShowRewardedVideo();
    // }

    // private void RequesBanner()
    // {
    //     banner = new BannerView(banner_ID, AdSize.Banner, AdPosition.Top);
    //     AdRequest request = new AdRequest.Builder().Build();
    //     banner.LoadAd(request);
    // }

    // private void RequestInterstitial()
    // {
    //    intertitial_Ad = new InterstitialAd(interstitial_Ad_ID);
    //     intertitial_Ad.OnAdLoaded += Intertitial_Ad_OnAdLoaded; ;
    //     AdRequest request = new AdRequest.Builder().Build();
    //     intertitial_Ad.LoadAd(request);

    // }

    // public void Showinterstitial()
    // {
    //     if (intertitial_Ad.IsLoaded())
    //     {
    //         intertitial_Ad.Show();
    //         RequestInterstitial();
    //     }
    // }

    // private void Intertitial_Ad_OnAdLoaded(object sender, EventArgs e)
    // {

    // }

    //public void RequestRewarrdedVideo()
    // {
    //     rewardedAd = new RewardedAd(rewarded_ID);
    //     rewardedAd.OnUserEarnedReward += RewardedAd_OnUserEarnedReward;
    //     rewardedAd.OnAdClosed += RewardedAd_OnAdClosed;
    //     rewardedAd.OnAdFailedToShow += RewardedAd_OnAdFailedToShow;
    //     AdRequest request = new AdRequest.Builder().Build();
    //     rewardedAd.LoadAd(request);
    // }

    // private void ShowRewardedVideo()
    // {
    //     if (rewardedAd.IsLoaded())
    //     {
    //         rewardedAd.Show();

    //     }
    // }

    // public void HandleRewardedAdLoaded(object sender, EventArgs args)
    // {
    //     rewardedAd.Show();
    // }

    // private void RewardedAd_OnAdFailedToShow(object sender, AdErrorEventArgs e)
    // {
    //     RequestRewarrdedVideo();
    // }

    // private void RewardedAd_OnAdClosed(object sender, EventArgs e)
    // {
    //     RequestRewarrdedVideo();
    // }

    // private void RewardedAd_OnUserEarnedReward(object sender, Reward e)
    // {
    //     RequestRewarrdedVideo();
    // }

    private BannerView bannerView;

    private InterstitialAd interstitial;

    private RewardedAd rewardedAd;



    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
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

    public void RequestInterstitial()
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

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += Interstitial_OnAdLoaded;
    }

    private void Interstitial_OnAdLoaded(object sender, System.EventArgs e)
    {
        interstitial.Show();
    }

    public void CreateAndLoadRewardedAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnAdLoaded += RewardedAd_OnAdLoaded;
        this.rewardedAd.OnUserEarnedReward += RewardedAd_OnUserEarnedReward;
        this.rewardedAd.OnAdClosed += RewardedAd_OnAdClosed;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    private void RewardedAd_OnAdClosed(object sender, EventArgs e)
    {
       
    }

    private void RewardedAd_OnUserEarnedReward(object sender, Reward e)
    {
       
    }

    private void RewardedAd_OnAdLoaded(object sender, EventArgs e)
    {
        rewardedAd.Show();
    }
}