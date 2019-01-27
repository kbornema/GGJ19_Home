using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPos;

    private void OnTriggerEnter(Collider collision)
    {   
        if(collision.GetComponent<PlayerController>())
        {
            GameManager.Instance.SetActiveCheckpoint(this);
        }
    }

    public Vector3 GetSpawnPos()
    {
        return _spawnPos.position;
    }
}
