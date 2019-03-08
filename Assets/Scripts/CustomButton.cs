using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomButton : MonoBehaviour
{
    public enum Type { START, QUIT, BREAKER };
    public Type type;
    public Color normal, hover, clicked;

    public bool over, click;

    private TextMeshPro text;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if(!over)
        {
            text.color = normal;
        }
        else if(over && !click)
        {
            text.color = hover;
        }
        else if(over && click)
        {
            text.color = clicked;
        }

        switch(type)
        {
            case Type.BREAKER:
                text.text = "Breaker: ";
                text.text += Breaker.on ? "ON" : "Off";
                break;
        }
    }

    private void OnMouseOver()
    {
        over = true;
    }
    private void OnMouseExit()
    {
        over = false;
    }
    private void OnMouseDown()
    {
        click = true;
        switch(type)
        {
            case Type.BREAKER:
                Breaker.on = !Breaker.on;
                break;
            case Type.START:
                TransitionHandler.me.go = true;
                break;
            case Type.QUIT:
                Application.Quit();
                break;
        }
    }
    private void OnMouseUp()
    {
        click = false;
    }
}
