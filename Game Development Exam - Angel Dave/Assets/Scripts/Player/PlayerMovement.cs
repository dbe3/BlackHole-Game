using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 7f;

    float HorizontalDirection = 0f;
    float VerticalDirection = 0f;

    void Update()
    {
        HorizontalDirection = Input.GetAxisRaw("Horizontal")* MovementSpeed;
        VerticalDirection = Input.GetAxisRaw("Vertical") * MovementSpeed;
    }

    void FixedUpdate()
    {
        Move(HorizontalDirection, VerticalDirection);
    }

    public void Move(float horizontalDir, float verticalDir)
    {
        transform.Translate(Vector2.right * horizontalDir * Time.deltaTime);
        transform.Translate(Vector2.up * verticalDir * Time.deltaTime);
    }
}
