using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTilt : MonoBehaviour
{
    public float maxRotationAngle = 30f;
    public float maxPosition = 4.5f;

    Weight[] weights;
    Quaternion targetRotation;

    void Start()
    {
        
    }

    void Update()
    {
        weights = GetComponentsInChildren<Weight>();

        Vector2 combinedVector = Vector2.zero;
        foreach(Weight w in weights)
        {
            combinedVector += new Vector2(w.transform.localPosition.x, w.transform.localPosition.z);
        }
        //Average out the vector, idk if its needed
        //combinedVector /= weights.Length;

        RemapWeightVector(ref combinedVector, maxPosition, maxRotationAngle);

        targetRotation = Quaternion.Euler(combinedVector.y, 0f, -combinedVector.x);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
    }

    void RemapWeightVector(ref Vector2 value, float from, float to)
    {
        value.x = -to + (value.x + from) * (2*to)/(2*from);
        value.y = -to + (value.y + from) * (2*to)/(2*from);
    }
}
