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

    public static GameManager me;
    public bool done = false;

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public TheScript goodEnding, badEnding, sadEnding, creepyEnding;

    private void Awake()
    {
        me = this;
        Debug.Log(theScript.script.Count);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(theScript == goodEnding ||
            theScript == badEnding ||
            theScript == sadEnding ||
            theScript == creepyEnding)
        {
            if(currentLine>2 && finishedTyping && Input.GetKeyDown(KeyCode.Return))
            {
                TransitionHandler.me.go = true;
            }
        }

        if(currentLine == theScript.script.Count && !done)
        {
            overrideline = "";
            finishedTyping = false;
            done = true;
            currentLine = 0;
            currentCharacter = 0;

            float good = 0;
            float sad = 0;
            float creepy = 0;
            for(int i = 0; i < stats.Count; i ++)
            {
                if(stats[i].name == "Good")
                {
                    good += stats[i].value;
                }
                if(stats[i].name == "Sad")
                {
                    sad += stats[i].value;
                }
                if(stats[i].name == "Creepy")
                {
                    creepy += stats[i].value;
                }
            }
            if(good > sad + creepy)
            {
                theScript = goodEnding;
            }
            else if(sad > good + creepy)
            {
                theScript = sadEnding;
            }
            else if(creepy > good + sad)
            {
                theScript = creepyEnding;
            }
            else if(good <= 0)
            {
                theScript = badEnding;
            }
            else
            {
                theScript = badEnding;
            }
        }

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
            if (currentLine < theScript.script.Count)
            {
                Next();
            }
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
            if (theScript.script[currentLine].isQuestion == false || !finishedTyping)
            {
                if (finishedTyping)
                {
                    currentLine++;
                    spriteRenderer.sprite = theScript.script[currentLine].sprite;
                    animator.Play("Shake");
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
                spriteRenderer.sprite = theScript.script[currentLine].answers[selection].sprite;
                animator.Play("Shake");

                if (overrideline == "")
                {
                    currentLine++;
                    spriteRenderer.sprite = theScript.script[currentLine].sprite;
                    animator.Play("Shake");
                }
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
                spriteRenderer.sprite = theScript.script[currentLine].sprite;
                animator.Play("Shake");
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
        bool newStat = true;
        for (int i = 0; i < stats.Count;i ++)
        {
            if(stats[i].name == stat.name)
            {
                stats[i].value += stat.value;
                newStat = false;
                break;
            }
        }
        if(newStat)
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