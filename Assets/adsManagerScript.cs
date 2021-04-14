using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adsManagerScript : MonoBehaviour
{
    void Start()
    {
        AdmobAds.instance.requestInterstital();
        AdmobAds.instance.loadRewardVideo();
       // callBanner();
    }

    public void callBanner()
    {
        AdmobAds.instance.reqBannerAd();
    }
    public void rewarded()
    {
         
           AdmobAds.instance.showVideoAd();
    }
    public void interstitial()
    {
      
        AdmobAds.instance.ShowInterstitialAd();
    }
    public void hideBanner()
    {
        AdmobAds.instance.hideBanner();
    }
}
