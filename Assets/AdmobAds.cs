using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class AdmobAds : MonoBehaviour
{
    public string GameID;

    // Sample ads : replace them with the actual ad ids
   public string bannerAdId;
   public string InterstitialAdID;
   public string rewarded_Ad_ID;


    public BannerView bannerAd;
    public InterstitialAd interstitial;
    public RewardBasedVideoAd rewardedAd;
   
    public static AdmobAds instance;

    private void Awake()
    {
       
        //Optional 
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

        rewardedAd = RewardBasedVideoAd.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(GameID);

    }

    #region rewarded Video Ads

    public void loadRewardVideo()
    {
        rewardedAd.LoadAd(new AdRequest.Builder().Build(), rewarded_Ad_ID);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdRewarded += HandleUserEarnedReward;
        rewardedAd.OnAdLeavingApplication += HandleOnRewardAdleavingApp;

    }

    /// rewarded video events //////////////////////////////////////////////

    public event EventHandler<EventArgs> OnAdLoaded;

    public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

    public event EventHandler<EventArgs> OnAdOpening;

    public event EventHandler<EventArgs> OnAdStarted;

    public event EventHandler<EventArgs> OnAdClosed;

    public event EventHandler<Reward> OnAdRewarded;

    public event EventHandler<EventArgs> OnAdLeavingApplication;

    public event EventHandler<EventArgs> OnAdCompleted;

    /// Rewared events //////////////////////////



    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Video Loaded");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Video not loaded");
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("Video Loading");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.Log("Video Loading failed"); 
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("Video Loading failed");
        if (this.bannerAd != null)
        {
            this.bannerAd.Show();
        }
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        /// reward the player here --------------------
        
        PlayerPrefs.SetInt("GameScore", PlayerPrefs.GetInt("GameScore")+50);
    }

    public void HandleOnRewardAdleavingApp(object sender, EventArgs args)
    {
        Debug.Log("when user clicks the video and open a new window");
    }


     
    public void showVideoAd()
    {
       if (this.bannerAd != null)
        {
            this.bannerAd.Hide();
        }
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        else
        {
           
        }
    }

    #endregion

    #region banner

    public void reqBannerAd()
    {
        if (this.bannerAd != null)
        {
            this.bannerAd.Destroy();
        }
        this.bannerAd = new BannerView(bannerAdId, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        this.bannerAd.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerAd.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;

        AdRequest request = new AdRequest.Builder().Build();

        this.bannerAd.LoadAd(request);

    }


    public void hideBanner()
    {
        this.bannerAd.Hide();
    }

    #endregion

    #region interstitial

    public void requestInterstital()
    {
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }
        this.interstitial = new InterstitialAd(InterstitialAdID);

        this.interstitial.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.interstitial.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.interstitial.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();

        this.interstitial.LoadAd(request);
    }

    public void ShowInterstitialAd()
    {
        if (this.bannerAd != null)
        {
            this.bannerAd.Hide();
        }
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
           
        }
    }

    #endregion

    #region adDelegates

    //Delegates that i dont know
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Ad Loaded");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("couldnt load ad" + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Debug.Log("Ad Closed");
        requestInterstital(); // Optional : in case you want to load another interstial ad rightaway
        if (this.bannerAd != null)
        {
            this.bannerAd.Show();
        }
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    #endregion
}
