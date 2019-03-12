using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowBox : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource source;
    public static MeowBox me;
    [Range(1,3)]
    public float maxPitch;

    public char[] alphabet = new char[26];

    public void Awake()
    {
        alphabet = new char[26];
        for (int i = 0; i < 26; i ++)
        {
            alphabet[i] = (char)(i + 97);
        }
        source = GetComponent<AudioSource>();
        me = this;
    }

    public void Meow()
    {
        int selection = Random.Range(0, clips.Length);
        source.clip = clips[selection];
        source.pitch = Random.Range(1f,maxPitch);
        source.Play();
    }
}
