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
        // ���� �غ�
        vidioPlayer.Prepare();

        // ������ �غ�Ǵ� ���� ��ٸ�
        while (!vidioPlayer.isPrepared)
        {
            yield return new WaitForSeconds(0.5f);
        }

        // VideoPlayer�� ��� texture�� RawImage�� texture�� �����Ѵ�
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
