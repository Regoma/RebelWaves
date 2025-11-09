using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private AnimationCurve wave1;
    [SerializeField] private LineRenderer wave1Line;
    [SerializeField] private AnimationCurve wave2;
    [SerializeField] private LineRenderer wave2Line;
    [SerializeField] private AnimationCurve wave3;
    [SerializeField] private LineRenderer wave3Line;
    [SerializeField] private AnimationCurve wave4;
    [SerializeField] private LineRenderer wave4Line;
    [SerializeField] private Transform mouseCursor;
    [Space]
    [SerializeField] private float waveLenght;
    [SerializeField] private float lineLenght;
    [SerializeField] private float lineVertex;
    [SerializeField] private float speed;

    private float mousePos;
    private float t;


    void Start()
    {
        Cursor.visible = false;
        wave1Line.positionCount = 50;
        wave2Line.positionCount = 50;
        wave3Line.positionCount = 50;
        wave4Line.positionCount = 50;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mousePos += Input.GetAxis("Mouse Y") *0.5f;
        mousePos = Mathf.Clamp(mousePos, -2, 2);
        mouseCursor.localPosition = new Vector2(0, mousePos);
    }

    private void Update()
    {
        t += Time.deltaTime * speed;

        if (t > waveLenght)
            t -= waveLenght;


        for(int i = 0; i<lineVertex; i++)
        {
            float ratio = lineLenght / (lineVertex - 1) * i;
            float amplitude1 = wave1.Evaluate((t + ratio) % waveLenght)/2 + 1.5f;
            wave1Line.SetPosition(i, transform.position + new Vector3(ratio, amplitude1, 0));
            float amplitude2 = wave2.Evaluate((t + ratio) % waveLenght) / 2 + 0.5f;
            wave2Line.SetPosition(i, transform.position + new Vector3(ratio, amplitude2, 0));
            float amplitude3 = wave3.Evaluate((t + ratio) % waveLenght) / 2 + -0.5f;
            wave3Line.SetPosition(i, transform.position + new Vector3(ratio, amplitude3, 0));
            float amplitude4 = wave4.Evaluate((t + ratio) % waveLenght) / 2 + -1.5f;
            wave4Line.SetPosition(i, transform.position + new Vector3(ratio, amplitude4, 0));


        }

        wave1Line.transform.localPosition = new Vector2(0, wave1.Evaluate( t % waveLenght)/2 + 1.5f);
        wave2Line.transform.localPosition = new Vector2(0, wave2.Evaluate(t % waveLenght) / 2 + 0.5f);
        wave3Line.transform.localPosition = new Vector2(0, wave3.Evaluate(t % waveLenght) / 2 + -0.5f);
        wave4Line.transform.localPosition = new Vector2(0, wave4.Evaluate(t % waveLenght) / 2 + -1.5f);
    }
}
