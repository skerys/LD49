using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAnimation : MonoBehaviour
{
    public float minSize = 0.5f;
    public float maxSize = 0.7f;
    public float timerSpeed = 1f;
    public float minSizeY = 0.5f;
    public float maxSizeY= 0.7f;
    public float timerSpeedY = 1f;

    float from;
    float to;
    float timer;

    float fromY;
    float toY;
    float timerY;

    void Start()
    {
        from = minSize;
        to = maxSize;
        timer = Random.Range(0f,1f);

        fromY = minSizeY;
        toY = maxSizeY;
        timerY = Random.Range(0f,1f);
    }

    void Update()
    {
        timer += Time.deltaTime * timerSpeed;
        transform.localScale = Vector3.one * EaseInOutBack(from, to, timer);
        if(timer > 1f)
        {
            var temp = from;
            from = to;
            to = temp;
            timer = 0f;
        }

        timerY += Time.deltaTime * timerSpeedY;
        transform.localScale = new Vector3(transform.localScale.x, EaseInOutBack(fromY, toY, timerY), transform.localScale.z);
        if(timerY > 1f)
        {
            var tempY = fromY;
            fromY = toY;
            toY = tempY;
            timerY = 0f;
        }
    }

   public static float EaseInBack(float start, float end, float value)
    {
        end -= start;
        value /= 1;
        float s = 1.70158f;
        return end * (value) * value * ((s + 1) * value - s) + start;
    }

    public static float EaseOutBack(float start, float end, float value)
    {
        float s = 1.70158f;
        end -= start;
        value = (value) - 1;
        return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
    }

    public static float EaseInOutBack(float start, float end, float value)
    {
        float s = 1.70158f;
        end -= start;
        value /= .5f;
        if ((value) < 1)
        {
            s *= (1.525f);
            return end * 0.5f * (value * value * (((s) + 1) * value - s)) + start;
        }
        value -= 2;
        s *= (1.525f);
        return end * 0.5f * ((value) * value * (((s) + 1) * value + s) + 2) + start;
    }


}
