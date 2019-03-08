using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TheScript theScript;
    public int currentLine;
    public int currentCharacter;
    public bool finishedTyping;
    public int selection;
    public string overrideline;
    public List<Stat> stats = new List<Stat>();
    public int meowFrequency;
    [Header("GameObject references")]
    public TextMeshPro dialogText;
    public TextMeshPro answerText;
    public Animator answerBox;
    public string unselectedColor, selectedColor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Type character by character until you're done
        if(!finishedTyping)
        {
            if (currentCharacter % meowFrequency == 0)
            {
                MeowBox.me.Meow();
            }
            currentCharacter++;

            if (overrideline == "")
            {
                dialogText.text = theScript.script[currentLine].line.Substring(0, currentCharacter);

                if (dialogText.text.Equals(theScript.script[currentLine].line))
                {
                    finishedTyping = true;
                }
            }
            else
            {
                dialogText.text = overrideline.Substring(0, currentCharacter);
                if(dialogText.text.Equals(overrideline))
                {
                    finishedTyping = true;
                }
            }
        }
        else
        {
            if (overrideline == "")
            {
                dialogText.text = theScript.script[currentLine].line;
                if (theScript.script[currentLine].isQuestion && answerBox.GetBool("Visible") == false)
                {
                    selection = 0;
                    answerBox.SetBool("Visible", true);
                }
            }
            else
            {
                dialogText.text = overrideline;
            }
        }

        // Show the answer box if the line is a question
        if(theScript.script[currentLine].isQuestion)
        {
            string answers = "";
            for (int i = 0; i < theScript.script[currentLine].answers.Length; i ++)
            {
                answers += "<color=#";
                answers += i == selection ? selectedColor : unselectedColor;
                answers += ">•" + theScript.script[currentLine].answers[i].text + "</color>\r\n";
            }
            answerText.text = answers;
        }

        // Key inputs
        // Next line on enter press
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Next();
        }
        // Up and down
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            selection++;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            selection--;
        }

        if (selection < 0)
        {
            selection = theScript.script[currentLine].answers.Length - 1;
        }
        if(selection >= theScript.script[currentLine].answers.Length)
        {
            selection = 0;
        }
    }

    public void Next()
    {
        if (overrideline == "")
        {
            if (theScript.script[currentLine].isQuestion == false)
            {
                if (finishedTyping)
                {
                    currentLine++;
                    currentCharacter = 0;
                    finishedTyping = false;
                }
                else
                {
                    finishedTyping = true;
                }
            }
            else
            {
                for (int i = 0; i < theScript.script[currentLine].answers[selection].statImpacts.Length; i++)
                {
                    AddStat(theScript.script[currentLine].answers[selection].statImpacts[i]);
                }
                overrideline = theScript.script[currentLine].answers[selection].response;
                currentCharacter = 0;
                finishedTyping = false;
                answerBox.SetBool("Visible", false);
            }
        }
        else
        {
            if(finishedTyping)
            {
                currentLine++;
                currentCharacter = 0;
                finishedTyping = false;
                overrideline = "";
            }
            else
            {
                finishedTyping = true;
            }
        }
    }

    public void AddStat(Stat stat)
    {
        for (int i = 0; i < stats.Count;i ++)
        {
            if(stats[i].name == stat.name)
            {
                stats[i].value += stat.value;
                break;
            }
        }
        stats.Add(stat);
    }
}

[System.Serializable]
public class Stat
{
    public string name;
    public float value;

    public Stat(string n, float v)
    {
        name = n;
        value = v;
    }
}