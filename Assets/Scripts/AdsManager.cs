﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    public BannerView bannerView;

    private InterstitialAd interstitial;

    private RewardedAd rewardedAd;

    public Action acVideoComplete;

    public Vector3 vRevive;

   // public bool traped; // dính bẫy
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

    private void RewardedAd_OnAdClosed(object sender, EventArgs e)
    {
        RequesRewarded();
    }

    private void RewardedAd_OnAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        
    }

    private void RewardedAd_OnAdOpening(object sender, EventArgs e)
    {
        
    }

    private void RewardedAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        
    }

    private void RewardedAd_OnAdLoaded(object sender, EventArgs e)
    {
        
    }


    public void RequestBanner()
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

        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += BannerView_OnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

  public void BannerView_OnAdClosed(object sender, EventArgs e)
    {
        bannerView.Destroy();
       
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
        else
        {
            Debug.Log("Video_Reward is not ready yet");
        }
    }
    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
        else
        {
            Debug.Log("Interstitial is not ready yet");
        }
    }
    
    private void RequesRewarded()
    {
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        this.rewardedAd = new RewardedAd(adUnitId);

        rewardedAd.OnUserEarnedReward += RewardedAd_OnUserEarnedReward;

        this.rewardedAd.OnAdLoaded += RewardedAd_OnAdLoaded;

        this.rewardedAd.OnAdFailedToLoad += RewardedAd_OnAdFailedToLoad;

        this.rewardedAd.OnAdOpening += RewardedAd_OnAdOpening;

        this.rewardedAd.OnAdFailedToShow += RewardedAd_OnAdFailedToShow;

        this.rewardedAd.OnAdClosed += RewardedAd_OnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    private void RewardedAd_OnUserEarnedReward(object sender, Reward e)
    {
        if (acVideoComplete !=null)
        {
            acVideoComplete();
        }
        //string type = e.Type;
        //double amount = e.Amount;
    }
    
}
