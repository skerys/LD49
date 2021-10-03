using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeColor : MonoBehaviour
{
    //The color of the object
    private Color MaterialColor;
    public Color minColor;
    public Color maxColor;
    public Vector2 minMaxHue;
    public Vector2 minMaxSaturation;
    public Vector2 minMaxValue;


    //The material property block we pass to the GPU
    private MaterialPropertyBlock propertyBlock;


    void Awake()
    {
        MaterialColor = Random.ColorHSV(minMaxHue.x /360f, minMaxHue.y/360f, minMaxSaturation.x/100f, minMaxSaturation.y/100f, minMaxValue.x/100f, minMaxValue.y/100f);
        //Debug.Log(MaterialColor.r);
    }

    // OnValidate is called in the editor after the component is edited
    void Start()
    {
        //create propertyblock only if none exists
        if (propertyBlock == null)
            propertyBlock = new MaterialPropertyBlock();
        //Get a renderer component either of the own gameobject or of a child
        Renderer renderer = GetComponentInChildren<Renderer>();
        //set the color property
        propertyBlock.SetColor("_Color", MaterialColor);
        //apply propertyBlock to renderer
        renderer.SetPropertyBlock(propertyBlock);
    }
}
