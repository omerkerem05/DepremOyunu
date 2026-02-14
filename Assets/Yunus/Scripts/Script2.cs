using UnityEngine;

public class Script2 : MonoBehaviour
{
    [Header("Movement Limits")]
    [SerializeField] private float minX = -7f;
    [SerializeField] private float maxX = 7f;
    [SerializeField] private float minY = -3.5f;
    [SerializeField] private float maxY = -1.5f;

    [Header("Click Detection")]
    [SerializeField] private float clickMaxMoveWorld = 0.15f;
    [SerializeField] private float clickMaxTime = 0.25f;

    private static Script2 _active;

    private Camera _cam;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _dragOffset;
    private Vector3 _downObjectPos;
    private float _downTime;

    private void Awake()
    {
        _cam = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_cam == null)
        {
            Debug.LogError("Main Camera bulunamadi. Script2 icin MainCamera tag gerekli.");
        }

        if (_spriteRenderer == null)
        {
            Debug.LogWarning($"[{name}] SpriteRenderer yok. Mouse ustunde kontrolu kaba fallback ile yapilacak.");
        }
    }

    private void Update()
    {
        if (_cam == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && _active == null && IsMouseOverThis())
        {
            _active = this;
            _downTime = Time.time;
            _downObjectPos = transform.position;

            Vector3 mouseWorld = GetMouseWorldAtObjectZ();
            _dragOffset = transform.position - mouseWorld;
        }

        if (_active == this && Input.GetMouseButton(0))
        {
            Vector3 target = GetMouseWorldAtObjectZ() + _dragOffset;
            target.x = Mathf.Clamp(target.x, minX, maxX);
            target.y = Mathf.Clamp(target.y, minY, maxY);
            target.z = transform.position.z;
            transform.position = target;
        }

        if (_active == this && Input.GetMouseButtonUp(0))
        {
            float heldTime = Time.time - _downTime;
            float movedDistance = Vector2.Distance(_downObjectPos, transform.position);

            if (heldTime <= clickMaxTime && movedDistance <= clickMaxMoveWorld)
            {
                Debug.Log($"[Tiklandi] {name}");
            }

            _active = null;
        }
    }

    // Script3 hala SetTable cagirsa da sistem collider/masa referansina bagli degil.
    public void SetTable(script1 area)
    {
    }

    private bool IsMouseOverThis()
    {
        Vector3 mouseWorld = GetMouseWorldAtObjectZ();

        if (_spriteRenderer != null)
        {
            return _spriteRenderer.bounds.Contains(mouseWorld);
        }

        // SpriteRenderer yoksa kucuk bir alanla yakalama fallback'i.
        float dx = Mathf.Abs(mouseWorld.x - transform.position.x);
        float dy = Mathf.Abs(mouseWorld.y - transform.position.y);
        return dx <= 0.5f && dy <= 0.5f;
    }

    private Vector3 GetMouseWorldAtObjectZ()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = Mathf.Abs(_cam.transform.position.z - transform.position.z);

        Vector3 world = _cam.ScreenToWorldPoint(mouse);
        world.z = transform.position.z;
        return world;
    }
}
