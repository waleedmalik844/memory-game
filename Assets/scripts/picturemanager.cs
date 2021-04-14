using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class picturemanager : MonoBehaviour
{
    public picture_prefab picture_prefab;
    public Transform picspwanposion;
    public Vector2 startpos = new Vector2(-2.15f, 3.62f);
    [Space]
    [Header("GamEendpanal")]
    public GameObject EndGamepanal;

    public GameObject newbestscore;
    public GameObject yourscoretxt;
    public GameObject EndTimetext;

    public enum Gamestate
    {
        NoAction,
        movingonposition,
        deletepuzzel,
        filpback,
        checking,
        gameEnd,

    };
    public enum puzzelstate
    {
        puzzelrotating,
        canRotate,
    };
    public enum Revaldstate
    {
        noreveald,
        onreveald,
        tworeveald,
    };

    [HideInInspector]
    public Gamestate currentgamestate;
    [HideInInspector]
    public puzzelstate currentpuzzelstate;
    [HideInInspector]
    public Revaldstate puzzelreveldnumber;



    [HideInInspector]
    public List<picture_prefab> picturelist;

    private Vector2 _offset = new Vector2(1.5f, 1.3f);


    private List<Material> _materialList = new List<Material>();
    private List<string> _texturepathlist = new List<string>();
    private Material _firstmaterial;
    private string _firsttexturepath;

    private int _firstreveldpic;
    private int _secondreveldpic;
    private int _reveldpicnumber = 0;
    private int _pictodestroy1;
    private int _pictodestroy2;
    private bool _corutinestarted;
    private int _pairnum;
    private int _removedpair;
    private timer _gametimer;

    private void Awake()
    {
        _pairnum = PlayerPrefs.GetInt("key"); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
        // currentgamestate = Gamestate.NoAction;
        currentpuzzelstate = puzzelstate.canRotate;
        puzzelreveldnumber = Revaldstate.noreveald;
        _reveldpicnumber = 0;
        _firstreveldpic = -1;
        _secondreveldpic = -1;

        _gametimer = GameObject.Find("Main Camera").GetComponent<timer>();

        loadMaterial();
        currentgamestate = Gamestate.movingonposition;
        _removedpair = 0;
       // _pairnum = 10;
            //

        if (_pairnum==10)
        {
            
            spawnpicturemesh(4, 5, startpos, _offset, false);
            movePicture(4, 5, startpos, _offset);
        }
        else if (_pairnum == 12)
        {

            spawnpicturemesh(4, 6, startpos, _offset, false);
            movePicture(4, 6, startpos, _offset);
        }
        else if (_pairnum == 14)
        {

            spawnpicturemesh(4, 7, startpos, _offset, false);
            movePicture(4, 7, startpos, _offset);
        }

    }

    private void Update()
    {

        if (currentgamestate == Gamestate.deletepuzzel)
        {
            if (currentpuzzelstate == puzzelstate.canRotate)
            {
                destroypic();
                checkgameend();
            }
        }


        if (currentgamestate == Gamestate.filpback)
        {
            if (currentpuzzelstate == puzzelstate.canRotate && _corutinestarted==false)
            {
                  StartCoroutine(routine: Filpback()) ;
                print(1);
            }
        }

        if (currentgamestate == Gamestate.gameEnd)
        {
           if(picturelist[_firstreveldpic].gameObject.activeSelf==false &&
                picturelist[_secondreveldpic].gameObject.activeSelf==false &&
                EndGamepanal.activeSelf==false)
            {
                showEndgameInfo();
            }
        }
    }

    private void showEndgameInfo()
    {
        EndGamepanal.SetActive(true);
        yourscoretxt.SetActive(true);
        var timer = _gametimer.GetCurrentTime();
        var minuts = Mathf.Floor(f: timer / 60);
        var second = Mathf.RoundToInt(f: timer % 60);
        var newtext = minuts.ToString(format: "00") + ":" + second.ToString(format: "00");
        EndTimetext.GetComponent<Text>().text = newtext;
    }

    private bool checkgameend()
    {
        if(_removedpair==_pairnum && currentgamestate != Gamestate.gameEnd)
        {
               //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BT>();
            currentgamestate = Gamestate.gameEnd;
            _gametimer.Stoptimer();
        }
        return (currentgamestate == Gamestate.gameEnd);
    }

    public void checkpicture()
    {
        currentgamestate = Gamestate.checking;
        _reveldpicnumber = 0;

        for(int id=0; id < picturelist.Count; id++)
        {
            if(picturelist[id].Reveld && _reveldpicnumber < 2)
            {
                if (_reveldpicnumber==0)
                {
                   
                    _firstreveldpic = id;
                    _reveldpicnumber++;

                }
                else if (_reveldpicnumber==1)
                {
                   
                    _secondreveldpic = id;
                    _reveldpicnumber++;
                }
            }
        }

        if (_reveldpicnumber == 2)
        {
            if (picturelist[_firstreveldpic].getindex() == picturelist[_secondreveldpic].getindex() && _firstreveldpic != _secondreveldpic) 
            {
                currentgamestate = Gamestate.deletepuzzel;
                _pictodestroy1 = _firstreveldpic;
                _pictodestroy2 = _secondreveldpic;

            }
            else
            {
                currentgamestate = Gamestate.filpback;
            }
            
        }
        currentpuzzelstate = picturemanager.puzzelstate.canRotate;
        if (currentgamestate == Gamestate.checking)
        {
            currentgamestate = Gamestate.NoAction;
        }
    }
    private void destroypic()
    {

        puzzelreveldnumber = Revaldstate.noreveald;
        System.Threading.Thread.Sleep(200);
        picturelist[_pictodestroy1].Deactivate();
        picturelist[_pictodestroy2].Deactivate();
        _reveldpicnumber = 0;
        _removedpair++;
        currentgamestate = Gamestate.NoAction;
        currentpuzzelstate = puzzelstate.canRotate;
    }


    private IEnumerator Filpback()
    {
        _corutinestarted = true;
        yield return new WaitForSeconds(0.5f);
        picturelist[_firstreveldpic].flipback();
        picturelist[_secondreveldpic].flipback();

        picturelist[_firstreveldpic].Reveld = false;
        picturelist[_secondreveldpic].Reveld = false;

        puzzelreveldnumber = Revaldstate.noreveald;
        currentgamestate = Gamestate.NoAction;
        _corutinestarted = false;

        print(0);
    }

    public void loadMaterial()
    {
        var materialpathfile = gamesetting.Instance.GetmeterialDictoryryName();
        var texturepathfile = gamesetting.Instance.getpuzzelcategorydirctoryname();
        var pairNumber = (int)gamesetting.Instance.GetEPairNumber();
        const string matbasename = "pic";
        var firstmaterialName = "Back";

        for(var index=1; index <= _pairnum; index++)
        {
            var currentfilepath = materialpathfile + matbasename + index;
            Material mat = Resources.Load(currentfilepath, typeof(Material)) as Material;
            _materialList.Add(mat);

            var currenttexxturefilepath = texturepathfile + matbasename + index;
            _texturepathlist.Add(currenttexxturefilepath);
            
        }

        _firsttexturepath = texturepathfile + firstmaterialName;
        _firstmaterial = Resources.Load(materialpathfile + firstmaterialName, typeof(Material)) as Material;

         
    }
   

    private void spawnpicturemesh(int rown, int colom, Vector2 pos, Vector2 offset, bool scaledown)
    {
        for (int col = 0; col < colom; col++)
        {
            for (int row = 0; row < rown; row++)
            {

                var tempPicture = (picture_prefab)Instantiate(picture_prefab, picspwanposion.position, picspwanposion.transform.rotation);

                tempPicture.name = tempPicture.name + 'c' + col + 'r' + row;
                picturelist.Add(tempPicture);
            }
        }

        applyTexture();

    }

   public void applyTexture()
    { 
        var randmaindex = Random.Range(0, _materialList.Count);
        var appliedtimes = new int[_materialList.Count];
        for(int  i=0; i < _materialList.Count; i++)

        {
            appliedtimes[i]=0;
        }
         
        foreach(var o in picturelist)
        {
            var randprevious = randmaindex;
            var counter = 0;
            var forcemat = false;
            while (appliedtimes[randmaindex] >= 2 || ((randprevious== randmaindex)&& !forcemat))
            {
                randmaindex = Random.Range(0, _materialList.Count);
                counter++;
                if (counter > 100)
                {
                    for(var j=0; j<_materialList.Count; j++)
                    {
                        if (appliedtimes[j] < 2)
                        {

                            randmaindex = j;
                            forcemat = true;

                        }
                       
                    }
                    if (forcemat == false)
                        return;
                }



            }

            o.set1stmat(_firstmaterial, _firsttexturepath);
            o.applyFirstmat();
            o.set2ndmat(_materialList[randmaindex], _texturepathlist[randmaindex]);
            o.setindex(randmaindex);
            o.Reveld = false;
            appliedtimes[randmaindex] += 1;
            forcemat = false;

        }
       
    }


    private void movePicture(int rown, int colom, Vector2 pos, Vector2 offset)
    {
        var index = 0;
        for (int col = 0; col < colom; col++)
        {
            for (int row = 0; row < rown; row++)
            {

                var targetposition = new Vector3((pos.x + (offset.x * row)), (pos.y - (offset.y * col)), 0.0f);
                StartCoroutine(movetoposition(targetposition, picturelist[index]));
                index++;
            }
        }

    }

    private IEnumerator movetoposition(Vector3 target, picture_prefab obj)
    {
        var randomdis = 7;
        while(obj.transform.position  != target)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target, randomdis * Time.deltaTime);
            yield return 0;
        }
    }
}
