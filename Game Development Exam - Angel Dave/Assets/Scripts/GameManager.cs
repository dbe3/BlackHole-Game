using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Spawnables Attributes")]
    public List<GameObject> ConsumablePrefabs;
    public Vector2 XConstraints;
    public Vector2 YConstraints;
    public int MaxSpawn;

    [Space(5)]
    public List<GameObject> SpawnedConsumables;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i <= MaxSpawn; i++)
        {
            SpawnConsumable();
        }
    }

    public void SpawnConsumable()
    {
        Vector2 SpawnPos = new Vector2(Random.Range(XConstraints.x, XConstraints.y), Random.Range(YConstraints.x, YConstraints.y));

        //GameObject ConsumableCopy = Instantiate(ConsumablePrefabs[Random.Range(0, ConsumablePrefabs.Count)], SpawnPos, Quaternion.identity);
        GameObject ConsumableCopy = Instantiate(ConsumablePrefabs[0], SpawnPos, Quaternion.identity);
        SpawnedConsumables.Add(ConsumableCopy);
    }
}
