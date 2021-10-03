using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChapterText : MonoBehaviour
{
    TMP_Text text;

    public float fadeInOutTime;
    float timer;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if(timer < fadeInOutTime) timer += Time.deltaTime;
        text.color = new Color(1f,1f,1f, timer/(fadeInOutTime));
    }
}
