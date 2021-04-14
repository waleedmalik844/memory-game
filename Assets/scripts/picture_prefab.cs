
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class picture_prefab : MonoBehaviour
{
    public Material _firstmaterial;
    public Material _secondmaterial;

    [HideInInspector] public bool Reveld = false;
    private picturemanager _picturemanager;
    private bool _clicked = false;

    private Quaternion currentrotation;

    private int _index;
    public void setindex(int id) { _index = id; }
    public int getindex() { return _index; }

    // Start is called before the first frame update
    void Start()
    {
        Reveld = false;
        _clicked = false;
        _picturemanager = GameObject.Find("picturemanager").GetComponent<picturemanager>();
        currentrotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnMouseDown()
    {
        if (_clicked==false)
        {
           
            _picturemanager.currentpuzzelstate = picturemanager.puzzelstate.puzzelrotating;
            StartCoroutine(routine: looprotation(angle: 45, fistmat: false));
            _clicked = true;
        }
        StartCoroutine(routine: looprotation(angle: 45, fistmat: false));
    }

    public void flipback()
    {
        if (gameObject.activeSelf)
        {
            _picturemanager.currentpuzzelstate = picturemanager.puzzelstate.puzzelrotating;
            Reveld = false;
            StartCoroutine(routine: looprotation(angle: 45, fistmat: true));
        }
    }

    IEnumerator looprotation( float angle, bool fistmat)
    {

        var rot = 0f;
        const float dir = 1f;
        const float rotspeed = 180.0f;
        const float rotspeed1= 90.0f;
        var startangle = angle;
        var assigned = false;


        if (fistmat)
        {
            while(rot < angle)
            {
                var step = Time.deltaTime * rotspeed1;
                gameObject.GetComponent<Transform>().Rotate(eulers: new Vector3(x: 0, y: 2, z: 0) * step * dir);
                if(rot>=(startangle-2)&& assigned==false)
                {
                    applyFirstmat();
                    assigned = true;
                }

                rot += (1 * step * dir);
                yield return null;
            }

        }
        else
        {
            while (angle > 0)
            {
                float step = Time.deltaTime * rotspeed;
                gameObject.GetComponent<Transform>().Rotate(eulers: new Vector3(x: 0, y: 2, z: 0) * step * dir);
                angle -= (1 * step * dir);
                yield return null;
            }
        }
        gameObject.GetComponent<Transform>().rotation = currentrotation;

        if (!fistmat)
        {
            Reveld = true;
            apply2ndmat();
            _picturemanager.checkpicture();
            
        }
        else
        {
            _picturemanager.puzzelreveldnumber = picturemanager.Revaldstate.noreveald;
            _picturemanager.currentpuzzelstate = picturemanager.puzzelstate.canRotate;
        }

        _clicked = false;
    }
    public void set1stmat(Material mat, string texturepath)
    {
        _firstmaterial = mat;
        _firstmaterial.mainTexture = Resources.Load(texturepath, typeof(Texture2D)) as Texture2D; 
    }

    public void set2ndmat(Material mat, string texturepath)
    {
        _secondmaterial = mat;
        _secondmaterial.mainTexture = Resources.Load(texturepath, typeof(Texture2D)) as Texture2D;
    }

    public void applyFirstmat()
    {
        gameObject.GetComponent<Renderer>().material = _firstmaterial;

    }

    public void apply2ndmat()
    {
        gameObject.GetComponent<Renderer>().material = _secondmaterial;

    }

    public void Deactivate()
    {

        StartCoroutine(routine: Deactivatiecorutine());
    }

    private IEnumerator Deactivatiecorutine()
    {
        Reveld = false;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }


}
