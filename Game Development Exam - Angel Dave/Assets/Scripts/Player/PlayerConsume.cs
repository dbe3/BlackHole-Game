using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerConsume : MonoBehaviour
{
    [Header("Player Growing Attributes")]
    public float GrowSize;
    public float GrowSpeed;
    public float MaxMass;
    bool Grow;

    Vector2 NewScale;

    [Space(5)]
    public Rigidbody2D PlayerRb;

    [Header("Scripts")]
    public Score ScoreScript;
    public CameraController CameraScript;
    public GameManager GameManagerScript;

    public delegate void Consumption();
    public static event Consumption OnConsumption;

    public int AmountToPool;
    public void Start()
    {
        GameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Consumable")
        {
            if (coll.gameObject.GetComponent<Consumable>() != null)
            {
                Consumable consumableScript = coll.gameObject.GetComponent<Consumable>();

                if (PlayerRb.mass > coll.gameObject.GetComponent<Rigidbody2D>().mass)
                {
                    OnConsumption();
                    Consume(consumableScript);
                }
            }
        }
    }

    public void Update()
    {
        if (Grow == true)
        { 
            if (transform.localScale.magnitude != NewScale.magnitude)
            {      
                transform.localScale = Vector3.Lerp(transform.localScale, NewScale, GrowSpeed * Time.deltaTime);
            }
            else
            {
                Grow = false;
            }
        }
    }

    public void Consume(Consumable consumableScript)
    {
        ScoreScript.AddScore(consumableScript.PointAmt);

        NewScale = new Vector3(transform.localScale.x + GrowSize, transform.localScale.y + GrowSize, transform.localScale.z + GrowSize);
        PlayerGrow();

        consumableScript.gameObject.SetActive(false);
        GameManagerScript.SpawnConsumable();
    }

    public void PlayerGrow()
    {
        if (PlayerRb.mass > MaxMass)
        {
            Grow = false;
        }
        else
        {
            Grow = true;
        }
    }

}
