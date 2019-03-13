using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtonSelector : MonoBehaviour
{
    public enum Buttons { PLAY, QUIT, BREAKER};
    public Buttons button;
    public CustomButton play, quit, breaker;
    public CustomButton selection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(button == Buttons.PLAY || button == Buttons.QUIT)
            {
                button = Buttons.BREAKER;
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (button == Buttons.BREAKER)
            {
                button = Buttons.PLAY;
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(button == Buttons.PLAY)
            {
                button = Buttons.QUIT;
            }
            else if(button == Buttons.QUIT)
            {
                button = Buttons.PLAY;
            }
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (button == Buttons.PLAY)
                button = Buttons.QUIT;
            else if (button == Buttons.QUIT)
                button = Buttons.PLAY;
        }

        play.over = false;
        quit.over = false;
        breaker.over = false;
        switch(button)
        {
            case Buttons.PLAY:
                play.over = true;
                selection = play;
                break;
            case Buttons.QUIT:
                quit.over = true;
                selection = quit;
                break;
            case Buttons.BREAKER:
                breaker.over = true;
                selection = breaker;
                break;
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            selection.click = true;
            selection.Click();
        }
        else if(Input.GetKeyUp(KeyCode.Return))
        {
            selection.click = false;
        }
    }
}
