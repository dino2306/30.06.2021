using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleAdMod
{

  public  class QuanCaoAdMods
    {
       public InterstitialAd interstitial;

         void InterstitialAD()
        {
            // Unity_ANDROID
            string adUnitID = "ca-app-pub-8296383146698662/7250772939";

            // Initialize an InterstitialAd.
            this.interstitial = new InterstitialAd(adUnitID);

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();

            // Load the interstitial with the request.
            this.interstitial.LoadAd(request);
        }

        public  void GameOver2()
        {
           
            if (this.interstitial.IsLoaded())
            {
                this.interstitial.Show();

            }
        }

    }


    
}
