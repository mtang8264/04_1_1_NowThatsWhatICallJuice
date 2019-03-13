using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TheScript : ScriptableObject
{
    public List<Dialog> script;
}

[System.Serializable]
public class Dialog
{
    public string line;
    public bool isQuestion;
    public Answer[] answers;
    public Sprite sprite;
}

[System.Serializable]
public class Answer
{
    public string text;
    public Stat[] statImpacts;
    public string response;
    public Sprite sprite;
}