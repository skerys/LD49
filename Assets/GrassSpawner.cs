using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    public Transform grass;
    void Start()
    {
        for(int i = 0; i < 15;++i)
        {
            Transform spawnGrass = Instantiate(grass, transform.position, Quaternion.identity);
            spawnGrass.parent = transform;
            spawnGrass.localPosition = new Vector3(Random.Range(-4.5f, 4.5f), 0.3f, Random.Range(-4.5f, 4.5f));
            spawnGrass.localScale = Vector3.one * Random.Range(0.5f, 1f);
            spawnGrass.localRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        }
    }
}
