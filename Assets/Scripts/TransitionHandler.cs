using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour
{
    public float time;
    public float timeToChange;
    public int goal;
    public bool go;
    public bool ah;

    private Image fade;
    public static TransitionHandler me;

    private void Awake()
    {
        me = this;
        fade = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ah)
        {
            fade.color = Color.Lerp(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), Time.time);
            if(Time.time > 1)
            {
                ah = false;
            }
        }
        if(go)
        {
            time += Time.deltaTime;
            fade.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), time / timeToChange);
            if (time > timeToChange)
                SceneManager.LoadScene(goal);
        }
    }
}
