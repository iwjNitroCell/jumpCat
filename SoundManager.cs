using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // ���� �̸�
    public AudioSource clip; // ��
}

public class SoundManager : MonoBehaviour
{
    public Sound BGM;

    public void PlaySE(string name)
    {

    }
}
