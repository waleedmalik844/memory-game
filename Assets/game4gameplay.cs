using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game4gameplay : MonoBehaviour
{

    public Image[] imagearay;
    public GameObject rstimg;
    public int randomval, randomval1, randomval2, randomval3;

    public bool b1, b2, b3, b4;
    public score scre;
    // Start is called before the first frame update
    void Start()
    {
        b1 = b2 = b3 = b4 = false;
        rstimg.SetActive(true);
        rANDOMCOLOR();
    }


    public void rANDOMCOLOR()
    {
        rstimg.SetActive(true);
        b1 = b2 = b3 = b4 = false;
        for (int i=0; i<imagearay.Length; i++)
        {
            imagearay[i].GetComponent<Button>().interactable = true;
        }
        randomval = Random.RandomRange(0, 3);
        randomval1 = Random.RandomRange(3, 6);
        randomval2 = Random.RandomRange(6, 9);
        randomval3 = Random.RandomRange(9, 11);
        
        //imagearay[randomval].GetComponent<Button>().interactable = true;
        //imagearay[randomval1].GetComponent<Button>().interactable = true;
        //imagearay[randomval2].GetComponent<Button>().interactable = true;
        //imagearay[randomval3].GetComponent<Button>().interactable = true;

        imagearay[randomval].color =Color.red;
        imagearay[randomval1].color = Color.red;
        imagearay[randomval2].color = Color.red;
        imagearay[randomval3].color = Color.red;


        Invoke("reset", 1.5f);

    }


    public void btn_pres(int a)
    {


        if(a==randomval )
        {
            print(1);
            b1 = true;
            imagearay[randomval].GetComponent<Button>().interactable = false;


        }
        else
        {
            b1 = b2 = b3 = b4 = false;

            rANDOMCOLOR();
        }
    }
    public void btn_pres2(int b)
    {


        if (b == randomval1 )
        {
            print(2);
            imagearay[randomval1].GetComponent<Button>().interactable = false;
            b2 = true;
        }
        else
        {
            
            rANDOMCOLOR();
        }
    }
    public void btn_pres3(int c)
    {


        if (c == randomval2)
        {
            print(3);
            b3 = true;
            imagearay[randomval2].GetComponent<Button>().interactable = false;
        }
        else
        {
           
            rANDOMCOLOR();

        }
    }
    public void btn_pres4(int d)
    {


        if (d== randomval3)
        {
            print(4);
            b4 = true;
            imagearay[randomval3].GetComponent<Button>().interactable = false;
        }
        else
        {
            
            rANDOMCOLOR();
        }
    }

    public void reset()
    {
        rstimg.SetActive(false);
        imagearay[randomval].color = Color.yellow;
        imagearay[randomval1].color = Color.yellow;
        imagearay[randomval2].color = Color.yellow;
        imagearay[randomval3].color = Color.yellow;

        
    }

    public void rndomI()
    {
        rANDOMCOLOR();
    }



    // Update is called once per frame
    void Update()
    {

        if (b1 == true && b2 == true && b3 == true && b4 == true)
        {
            rstimg.SetActive(true);

            b1 = b2 = b3 = b4 = false;
            scre.addscore();
            print(13);
            //rANDOMCOLOR();
            Invoke("rndomI",1f);
            
        }
    }
}
