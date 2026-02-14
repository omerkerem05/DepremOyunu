using UnityEngine;

public class script1 : MonoBehaviour
{
    [SerializeField] private Collider2D tableCollider;
    [SerializeField] private SpriteRenderer tableRenderer;

    private Bounds _bounds;

    private void Awake()
    {
        if (tableCollider == null)
        {
            tableCollider = GetComponent<Collider2D>();
        }

        if (tableRenderer == null)
        {
            tableRenderer = GetComponent<SpriteRenderer>();
        }

        if (tableCollider != null)
        {
            _bounds = tableCollider.bounds;
        }
        else if (tableRenderer != null)
        {
            _bounds = tableRenderer.bounds;
        }
        else
        {
            _bounds = new Bounds(transform.position, Vector3.one * 100f);
            Debug.LogWarning($"[{name}] Masa icin Collider2D veya SpriteRenderer bulunamadi. Varsayilan genis alan kullanildi.");
        }
    }

    public Vector3 ClampToTable(Vector3 worldPosition, Vector2 halfSize)
    {
        float minX = _bounds.min.x + halfSize.x;
        float maxX = _bounds.max.x - halfSize.x;
        float minY = _bounds.min.y + halfSize.y;
        float maxY = _bounds.max.y - halfSize.y;

        worldPosition.x = Mathf.Clamp(worldPosition.x, minX, maxX);
        worldPosition.y = Mathf.Clamp(worldPosition.y, minY, maxY);
        worldPosition.z = transform.position.z - 0.1f;

        return worldPosition;
    }
}
