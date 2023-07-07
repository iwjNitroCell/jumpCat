using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Ending : MonoBehaviour
{
    public VideoPlayer vidioPlayer = null;



    private void Update()
    {
        if (!vidioPlayer.isPlaying)
        {
            Application.Quit();
        }
    }

}
