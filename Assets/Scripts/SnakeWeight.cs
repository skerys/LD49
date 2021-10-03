using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeWeight : DestroyableWeight
{
    void Start()
    {
        LevelManager.Instance.AddSnake();
    }

    public override void KillWeight(Vector3 projectilePos)
    {
        base.KillWeight(projectilePos);
        LevelManager.Instance.RemoveSnake(transform.position);
        
    }
}
