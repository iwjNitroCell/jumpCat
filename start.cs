using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class start : MonoBehaviour
{
    public GameObject vidioHandler;
    private RawImage rawimage = null;
    public VideoPlayer vidioPlayer = null;

    private bool isStart = false;

    private void Awake()
    {
        rawimage = vidioHandler.GetComponent<RawImage>();
    }

    private void Start()
    {        
        if (rawimage != null && vidioPlayer != null)
        {
            StartCoroutine(prepareVidio());
        }
    }

    private void Update()
    {
        if(isStart && !vidioPlayer.isPlaying)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }


    IEnumerator prepareVidio()
    {
        // 비디오 준비
        vidioPlayer.Prepare();

        // 비디오가 준비되는 것을 기다림
        while (!vidioPlayer.isPrepared)
        {
            yield return new WaitForSeconds(0.5f);
        }

        // VideoPlayer의 출력 texture를 RawImage의 texture로 설정한다
        rawimage.texture = vidioPlayer.texture;
    }

    public void changeScene()
    {
        startVidio();           
    }

    public void startVidio()
    {
        if (vidioPlayer != null && vidioPlayer.isPrepared)
        {
            vidioHandler.SetActive(true);
            vidioPlayer.Play();
            isStart = true;
        }
    }
}
