using UnityEngine;

public class FitColliderToSprite : MonoBehaviour
{
    void Start()
    {
        TryGetComponent(out SpriteRenderer spriteRenderer);

        if (spriteRenderer == null) {
            // If we don't have a sprite component, find the sprite in our children
            Transform childSpriteTransform = transform.Find("Sprite");
            if (childSpriteTransform == null) {
                Debug.LogError($"No Sprite found on {gameObject.name} and no child named 'Sprite'");
                return;
            }
            childSpriteTransform.gameObject.TryGetComponent(out SpriteRenderer childSpriteRenderer);
            if (childSpriteRenderer == null) {
                Debug.LogError($"No SpriteRenderer found on {childSpriteTransform.gameObject.name}, child of {gameObject.name}");
                return;
            }
        }

        TryGetComponent(out BoxCollider2D boxCollider2D);

        if (boxCollider2D == null) {
            Debug.LogError("No BoxCollider2D found on " + gameObject.name);
            return;
        }

        boxCollider2D.size = spriteRenderer.size;
    }
}
