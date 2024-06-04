using UnityEngine;

public class WalkAround : MonoBehaviour
{
    public float speed = 1.5f; // Adjust the speed as needed
    public float flipDelay = 1f; // Adjust the delay before flipping
    private bool facingRight = true;
    private float flipTimer = 0f;

    void Update()
    {
        // Move to the left
        if (facingRight)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        // Move to the right
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        // Increment flip timer
        flipTimer += Time.deltaTime;

        // Flip the sprite when changing direction after the delay
        if (flipTimer >= flipDelay)
        {
            Flip();
            flipTimer = 0f; // Reset the timer
        }
    }

    // Flip the sprite
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
