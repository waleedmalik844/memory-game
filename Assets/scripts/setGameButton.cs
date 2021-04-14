using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setGameButton : MonoBehaviour
{


    public enum Ebuttontype
    {
        notset,
        pairnumbutton,
        puzzelcategorybtn,
    }

    [SerializeField] public  Ebuttontype buttontype = Ebuttontype.notset;
    [HideInInspector] public gamesetting.EPairNumber pairNumber = gamesetting.EPairNumber.NotSet;
    [HideInInspector] public gamesetting.EpuzzelCategotries puzzelcategory = gamesetting.EpuzzelCategotries.Notset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void setgameoptn(string gamescene)
    {
        
        var comp = gameObject.GetComponent<setGameButton>();
        switch (comp.buttontype)
        {
            case setGameButton.Ebuttontype.pairnumbutton:
                gamesetting.Instance.setPairNum(comp.pairNumber);
                break;
            case Ebuttontype.puzzelcategorybtn:
                gamesetting.Instance.SetpuzzleCategory(comp.puzzelcategory);
                break;
        }

     //   if(gamesetting.Instance.allsettingReady())
     //   {
            SceneManager.LoadScene(gamescene);
      //  }
    }

    public void btnnum(int num)
    {
        PlayerPrefs.SetInt("key", num);
    }

}
