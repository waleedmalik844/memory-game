using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAMESOUND : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip, wonclip, loseclip,BTN;
    // Start is called before the first frame update
   
    public void sound()
    {
        source.PlayOneShot(clip);
    }
    public void winsound()
    {
        source.PlayOneShot(wonclip);
    }
    public void losesound()
    {
        source.PlayOneShot(loseclip);
    }

    public void btn()
    {
        source.PlayOneShot(BTN);
    }


}
