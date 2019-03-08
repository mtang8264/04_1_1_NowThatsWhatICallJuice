using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowBox : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource source;
    public static MeowBox me;

    public void Awake()
    {
        source = GetComponent<AudioSource>();
        me = this;
    }

    public void Meow()
    {
        int selection = Random.Range(0, clips.Length);
        source.clip = clips[selection];
        source.Play();
    }
}
