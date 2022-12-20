using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Spawnables Attributes")]
    public List<GameObject> ConsumablePrefabs;
    public List<float> ConsumablePrefabsMass;
    public int MaxSpawnAsteroid;
    public int MaxSpawnMoon;
    public int MaxSpawnEarth;

    public Vector2 XConstraints;
    public Vector2 YConstraints;
    public int MaxSpawn;

    [Space(5)]
    public GameObject SpawnedConsumablesParent;
    public List<GameObject> SpawnedConsumables;
    public GameObject ConsumableToRemove = null;

    [Header("Object Pooling Attributes")]
    public List<GameObject> PooledObjects;
    public int AmountToPool;

    public GameObject Player;
    Rigidbody2D PlayerRb;
    float PlayerMass;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //PlayerConsume.OnConsumption += SpawnConsumable;
        PlayerConsume.OnConsumption += UpdateMass;

        PlayerRb = Player.GetComponent<Rigidbody2D>();
        PlayerMass = PlayerRb.mass;

        for (int i = 0; i < ConsumablePrefabs.Count; i++)
        {
            ConsumablePrefabsMass.Add(ConsumablePrefabs[i].GetComponent<Rigidbody2D>().mass);
        }

        /*
        for (int i = 0; i <= AmountToPool; i++)
        {
            SpawnConsumable();
        }
        */

        for (int i = 0; i < ConsumablePrefabs.Count; i++)
        {
            SpawnConsumablePool(ConsumablePrefabs[i]);
        }

        AmountToPool = PooledObjects.Count;
    }

    public void SpawnConsumablePool(GameObject consumable)
    {

        for (int i = 0; i < consumable.GetComponent<Consumable>().MaxSpawn; i++)
        {
            Vector2 SpawnPos = new Vector2(Random.Range(XConstraints.x, XConstraints.y), Random.Range(YConstraints.x, YConstraints.y));

            GameObject ConsumableCopy = Instantiate(consumable, SpawnPos, Quaternion.identity);
            ConsumableCopy.SetActive(true);
            PooledObjects.Add(ConsumableCopy);
        }
        /*
        for (int i = ConsumablePrefabsMass.Count - 1; i >= 0; i--)
        {
            if (i != 0)
            {
                if (PlayerMass > ConsumablePrefabsMass[i])
                {
                    GameObject ConsumableCopy = Instantiate(ConsumablePrefabs[Random.Range(0, i + 1)], SpawnPos, Quaternion.identity);
                    ConsumableCopy.transform.parent = SpawnedConsumablesParent.transform;
                    SpawnedConsumables.Add(ConsumableCopy);

                    break;
                }
            }
            else
            {
                GameObject ConsumableCopy = Instantiate(ConsumablePrefabs[0], SpawnPos, Quaternion.identity);
                ConsumableCopy.transform.parent = SpawnedConsumablesParent.transform;
                SpawnedConsumables.Add(ConsumableCopy);
            }
        }
        */
    }

    public void SpawnConsumable()
    {
        Vector2 SpawnPos = new Vector2(Random.Range(XConstraints.x, XConstraints.y), Random.Range(YConstraints.x, YConstraints.y));

        GameObject consumable = GetPooledObject();
        consumable.transform.position = SpawnPos;

        consumable.SetActive(true);
        
    }

    public void UpdateMass()
    {
        PlayerMass = PlayerRb.mass;
        Debug.Log(PlayerMass);
    }

    public void RemoveFromList(GameObject consumable)
    {
            Debug.Log("Working");
            SpawnedConsumables.Remove(consumable);
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < AmountToPool; i++)
        {
            if (!PooledObjects[i].activeInHierarchy)
            {
                return PooledObjects[i];
            }
        }

        return null;
    }
}
