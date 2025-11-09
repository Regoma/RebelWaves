using UnityEngine;

public class Ad : MonoBehaviour
{
    [SerializeField] private float speed;

    private SpriteRenderer _render;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _render = transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _render.size  += new Vector2(0, speed);
    }
}
