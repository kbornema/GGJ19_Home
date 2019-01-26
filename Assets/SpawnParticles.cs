using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticles : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private Transform _where;
    
    public void Spawn()
    {
        if(_prefab)
            Instantiate(_prefab, _where.position, Quaternion.identity);
    }
}
