using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMaterial : MonoBehaviour
{
    public Material material;
    public Color fromTop;
    public Color toTop;
    public Color fromBottom;
    public Color toBottom;

    public float timeToLerp;
    public float timeToNextScene;

    float timer = -2f;
    float sceneTimer = 0f;

    void Update()
    {
        if(timer < timeToLerp)
        {
            timer += Time.deltaTime;
            material.SetColor("_Top", Color.Lerp(fromTop, toTop, timer/timeToLerp));
            material.SetColor("_Bottom", Color.Lerp(fromBottom, toBottom, timer/timeToLerp));
        }

        if(sceneTimer < timeToNextScene)
        {
            sceneTimer += Time.deltaTime;
            if(sceneTimer > timeToNextScene)
            {
                LevelManager.Instance.NextLevel();
            }
        }
    }
}
