using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticles : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab = null;
    [SerializeField]
    private Transform _where = null;
    
    public void Spawn()
    {
        if(_prefab)
            Instantiate(_prefab, _where.position, Quaternion.identity);
    }
}
