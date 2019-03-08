using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Breaker : MonoBehaviour
{
    public static bool on = false;

    private void OnMouseDown()
    {
        if (on)
        {
            GameObject text = GameObject.Find("Answer");
            text.GetComponent<TextMeshPro>().text = text.GetComponent<TextMeshPro>().text.Replace(GameManager.me.selectedColor, GameManager.me.unselectedColor);
            Destroy(GameManager.me);
            Destroy(gameObject);
        }
    }
}
