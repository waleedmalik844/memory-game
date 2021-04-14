using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamesetting : MonoBehaviour
{
    private readonly Dictionary<EpuzzelCategotries, string> _puzzelcatdictory = new Dictionary<EpuzzelCategotries, string>();
    private int _setting;
    private int settingNum = 2;

    public enum EPairNumber
    {
        NotSet=0,
        Pair10=10,
        Pair15=15,
        Pair20 =20,
    }
    public enum EpuzzelCategotries
    {
        Notset,
        Fruits,
        Vegitables,
    }
    public struct setting
    {
       public EPairNumber PairNumber;
       public EpuzzelCategotries puzzelCategory;
    }
    public setting _gameSetting;
    public static gamesetting Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(target: this);
            Instance = this;
        }
        else
        {
            Destroy(obj: this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        setpuzzelcatdictory();
        _gameSetting = new setting();
        gamerestSetting(); 


    }

    public void setPairNum(EPairNumber Number)
    {
        if(_gameSetting.PairNumber == EPairNumber.NotSet)
        {
            _setting++;
        }
        else
        {
            _gameSetting.PairNumber = Number;
        }
        
    }
    public void SetpuzzleCategory(EpuzzelCategotries cat)
    {
        if (_gameSetting.puzzelCategory == EpuzzelCategotries.Notset)
        {
            _setting++;

        }
        _gameSetting.puzzelCategory = cat;
    }

    public EPairNumber GetEPairNumber()
    {
        return _gameSetting.PairNumber;
    }
    public EpuzzelCategotries GetEpuzzelCategotries()
    {
        return _gameSetting.puzzelCategory;
    }

    public void gamerestSetting()
    {
        _setting = 0;
        _gameSetting.puzzelCategory = EpuzzelCategotries.Notset;
        _gameSetting.PairNumber = EPairNumber.NotSet; 
    }
    


    public bool allsettingReady()
    {
        return _setting == settingNum;
    }

    public string GetmeterialDictoryryName()
    {
        return "Materials/";
    }

    private  void setpuzzelcatdictory()
    {
        _puzzelcatdictory.Add(EpuzzelCategotries.Fruits, "Fruits");
        _puzzelcatdictory.Add(EpuzzelCategotries.Vegitables, "Vegetables"); 
    }

    public string getpuzzelcategorydirctoryname()
    {
     //   if (_puzzelcatdictory.ContainsKey(_gameSetting.puzzelCategory))
       // {
            return "Graphics/PuzzleCat/Vegetables/";
            
      //  }
     //   else
      //  {
         ///   Debug.LogError("cant get directory name");
       ///     return "";
      //  }
    }
    public void btnnum(int num)
    {
        PlayerPrefs.SetInt("key", num);
    }

}
