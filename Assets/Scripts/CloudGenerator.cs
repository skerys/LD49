using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public GameObject cloudObject;
    public float bigSphereOffset = 0.3f;
    public float smallSphereOffset = 0.9f;
    public bool useSeed = false;
    public int seed = 0;

    void Start()
    {
        if(useSeed)Random.InitState(seed);
        for(int i = 0; i < 20; ++i)
        {
            Vector3 randomOffset = Random.onUnitSphere * 25f;
            randomOffset = new Vector3(randomOffset.x, -Mathf.Abs(randomOffset.y), randomOffset.z);
            var cloud = GenerateCloud(randomOffset);
            cloud.transform.localScale = Vector3.one * Random.Range(1f, 2f);
            cloud.transform.parent = transform;
        }
    }

    [ExecuteInEditMode]
    [ContextMenu("GenerateCloud")]
    public GameObject GenerateCloud(Vector3 position)
    {
        GameObject cloud = new GameObject("Cloud");
        cloud.transform.position = position;
        //big bits
        for(int i = 0; i < 4; ++i)
        {
            Vector3 randomOffset = Random.onUnitSphere * bigSphereOffset;
            randomOffset = new Vector3(2f *randomOffset.x, 0.5f * randomOffset.y, 2f * randomOffset.z);
            GameObject go = Instantiate(cloudObject, position + randomOffset, Quaternion.identity);
            go.transform.localScale = Vector3.one * Random.Range(2.5f, 3.5f);
            go.transform.parent = cloud.transform;
            go.isStatic = true;
        }

        //small bits
        for(int i = 0; i < 12; ++i)
        {
            Vector3 randomOffset = Random.onUnitSphere * smallSphereOffset;
            randomOffset = new Vector3(2f *randomOffset.x, -Mathf.Abs(randomOffset.y) - 0.5f, 2f * randomOffset.z);
            GameObject go = Instantiate(cloudObject, position + randomOffset, Quaternion.identity);
            go.transform.localScale = Vector3.one * Random.Range(1.5f, 2.0f);
            go.transform.parent = cloud.transform;
            go.isStatic = true;
        }

        return cloud;
    }
}
