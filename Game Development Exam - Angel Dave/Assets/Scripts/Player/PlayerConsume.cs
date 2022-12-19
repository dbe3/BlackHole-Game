using UnityEngine;
using System.Collections;

public class PlayerConsume : MonoBehaviour
{
    [Header("Player Growing Attributes")]
    public float Mass;
    public float GrowSize;
    public float GrowSpeed;
    bool Grow;
    Vector2 NewScale;

    [Space(5)]
    public Rigidbody2D PlayerRb;

    [Header("Scripts")]
    public Score ScoreScript;
    public CameraController CameraScript;

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Consumable")
        {
            if (coll.gameObject.GetComponent<Consumable>() != null)
            {
                Consumable consumableScript = coll.gameObject.GetComponent<Consumable>();

                if (PlayerRb.mass > coll.gameObject.GetComponent<Rigidbody2D>().mass)
                {
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
        NewScale = new Vector3(transform.localScale.x + GrowSize, transform.localScale.y + GrowSize, transform.localScale.z + GrowSize);
        Grow = true;

        ScoreScript.AddScore(consumableScript.PointAmt);
        Destroy(consumableScript.gameObject);
    }

    //public void Grow()
    //{
    //    transform.localScale = new Vector2(transform.localScale.x + GrowSize, transform.localScale.y + GrowSize);
    //}

}
