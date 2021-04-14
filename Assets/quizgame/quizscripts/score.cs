using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Text scoretext;
    private int _currentscore;



    // Start is called before the first frame update
    void Start()
    {
        _currentscore = 0;
        scoretext.text = _currentscore.ToString();

        
    }

    public void addscore()
    {
        _currentscore += 10;
        scoretext.text = _currentscore.ToString();
    }

    public void detuctscore()
    {
        _currentscore = _currentscore > 0 ? _currentscore - 10 : 0;
        scoretext.text = _currentscore.ToString();
    }

    
}
