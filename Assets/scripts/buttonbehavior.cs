using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonbehavior : MonoBehaviour
{
    
   
  
   public void loadsceen(string sceen_name)
    {

        AdmobAds.instance.ShowInterstitialAd();
        SceneManager.LoadScene(sceen_name);
    }
    
    

   
    public void ONEXIT()
    {
        Application.Quit();
    }
}
