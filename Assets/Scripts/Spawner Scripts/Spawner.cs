using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _tetrisObjects;
    public bool canSpawm;
   private void Start()
    {
        canSpawm = true;
        SpawnRandom();
    }
public void SpawnRandom()
    {if(canSpawm)
        { 
            int index = Random.Range(0, _tetrisObjects.Length);
            Instantiate(_tetrisObjects[index], transform.position, Quaternion.identity);
        }
    }
}
