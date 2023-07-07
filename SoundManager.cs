using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // ∞Ó¿« ¿Ã∏ß
    public AudioSource clip; // ∞Ó
}

public class SoundManager : MonoBehaviour
{
    public Sound BGM;

    public void PlaySE(string name)
    {

    }
}
