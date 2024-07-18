using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [Header(" Settings ")]
    [SerializeField] private Wave[] waves;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public struct Wave
{
    public string name;
    public List<WaveSegment> sgements;

}


[System.Serializable]
public struct WaveSegment
{
    public GameObject prefab;
}

