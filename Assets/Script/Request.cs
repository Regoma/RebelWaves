using UnityEngine;

public class Request : MonoBehaviour
{
    [SerializeField] private float requestTime;
    [SerializeField] private Sprite[] type;
    [SerializeField] private Color[] typeColor;
    [SerializeField] private int typeNb;

    [SerializeField] private float requestComplition = 0;
    private float startTime = 0;

    public Animator anim;
    private bool completed = false;

    void Start()
    {
        startTime = Time.time;
        typeNb = Random.Range(0, type.Length);
        InitRequest(typeNb);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime > requestTime &&!completed && Waves.instance.wave != typeNb)
        {
            completed = true;
            Waves.instance.Happiness(-10);
            Waves.instance.EndRequest(this);
            anim.Play("Request_fail");
        }
        else if (Waves.instance.wave == typeNb )
        {
            RequestUpdate(Time.deltaTime);
        }
    }

    public void RequestUpdate(float x)
    {
        requestComplition += x;
        if(requestComplition >= 5 && !completed)
        {
            completed = true;
            Waves.instance.Happiness(10);
            Waves.instance.EndRequest(this);
            anim.Play("Request_sucess");
        }
    }

    public void InitRequest(int x)
    {
        SpriteRenderer _render = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _render.sprite = type[x];
        _render.color = typeColor[x];

        anim = transform.GetComponent<Animator>();

    }


}
