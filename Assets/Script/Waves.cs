using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Audio;

public class Waves : MonoBehaviour
{
    public static Waves instance = null;

    [SerializeField] private Image happinessBar;
    [SerializeField] private float maxHappiness;
    [SerializeField] private float happiness = 0;
    [SerializeField] private Animator happinessFeedBack;
    [SerializeField] private List<Request> requests;
    [SerializeField] private GameObject requestPrefab;
    [SerializeField] private Animator[] playerAnims;
    [SerializeField] private Animator crowdAnims;
    public int wave = -1;
    [Space]
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
    [SerializeField] private float marging;
    [Space]
    [SerializeField] private AudioMixer mixer;

    private float mousePos;
    private float t;

    private bool spawnRequest = false;
    private bool started = false;

    private void Awake()
    {
        instance = this;
    }

    public void Init()
    {

        Cursor.visible = false;
        wave1Line.positionCount = 50;
        wave2Line.positionCount = 50;
        wave3Line.positionCount = 50;
        wave4Line.positionCount = 50;
        happinessBar.fillAmount = happiness / maxHappiness;
        spawnRequest = true;
        started = true;
        InvokeRepeating("NewRequest", 3, 3);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!started)
            return;

        mousePos += Input.GetAxis("Mouse Y") *0.5f;
        mousePos = Mathf.Clamp(mousePos, -2, 2);
        mouseCursor.localPosition = new Vector2(0, mousePos);
        mixer.SetFloat("PitchMusic", Mathf.Lerp(0.9f, 1.1f, mousePos / 2));

    }

    private void Update()
    {
        if (!started)
            return;

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

        if (mousePos >= wave1.Evaluate(t % waveLenght) / 2 + 1.5f - marging && mousePos <= wave1.Evaluate(t % waveLenght) / 2 + 1.55f + marging)
        {
            wave1Line.transform.GetChild(0).gameObject.SetActive(true);
            wave2Line.transform.GetChild(0).gameObject.SetActive(false);
            wave3Line.transform.GetChild(0).gameObject.SetActive(false);
            wave4Line.transform.GetChild(0).gameObject.SetActive(false);
            wave = 0;
            playerAnims[0].SetBool("active", true);
            playerAnims[1].SetBool("active", false);
            playerAnims[2].SetBool("active", false);
        } 
        else if (mousePos >= wave2.Evaluate(t % waveLenght) / 2 + 0.5f - marging && mousePos <= wave2.Evaluate(t % waveLenght) / 2 + 0.5f + marging)
        {
            wave1Line.transform.GetChild(0).gameObject.SetActive(false);
            wave2Line.transform.GetChild(0).gameObject.SetActive(true);
            wave3Line.transform.GetChild(0).gameObject.SetActive(false);
            wave4Line.transform.GetChild(0).gameObject.SetActive(false);
            wave = 1;
            playerAnims[0].SetBool("active", false);
            playerAnims[1].SetBool("active", true);
            playerAnims[2].SetBool("active", false);
        } 
        else if (mousePos <= wave3.Evaluate(t % waveLenght) / 2 + -0.5f + marging && mousePos >= wave3.Evaluate(t % waveLenght) / 2 + -0.50f - marging)
        {
            wave1Line.transform.GetChild(0).gameObject.SetActive(false);
            wave2Line.transform.GetChild(0).gameObject.SetActive(false);
            wave3Line.transform.GetChild(0).gameObject.SetActive(true);
            wave4Line.transform.GetChild(0).gameObject.SetActive(false);
            wave = 2;
            playerAnims[0].SetBool("active", false);
            playerAnims[1].SetBool("active", false);
            playerAnims[2].SetBool("active", true);
        } 
        else if (mousePos <= wave4.Evaluate(t % waveLenght) / 2 + -1.5f + marging && mousePos >= wave4.Evaluate(t % waveLenght) / 2 + -1.5f - marging)
        {
            wave1Line.transform.GetChild(0).gameObject.SetActive(false);
            wave2Line.transform.GetChild(0).gameObject.SetActive(false);
            wave3Line.transform.GetChild(0).gameObject.SetActive(false);
            wave4Line.transform.GetChild(0).gameObject.SetActive(true);
            wave = 3;
            playerAnims[0].SetBool("active", true);
            playerAnims[1].SetBool("active", true);
            playerAnims[2].SetBool("active", true);
        }
        else
        {
            wave1Line.transform.GetChild(0).gameObject.SetActive(false);
            wave2Line.transform.GetChild(0).gameObject.SetActive(false);
            wave3Line.transform.GetChild(0).gameObject.SetActive(false);
            wave4Line.transform.GetChild(0).gameObject.SetActive(false);
            wave = -1;
            playerAnims[0].SetBool("active", false);
            playerAnims[1].SetBool("active", false);
            playerAnims[2].SetBool("active", false);
        }
    }

    public void Happiness(float x)
    {
        string animUi = x > 0 ? "Ui_up" : "Ui_down";
        happinessFeedBack.Play(animUi);

        happiness = Mathf.Clamp(happiness + x, 0,maxHappiness);
        happinessBar.fillAmount = happiness / maxHappiness;

        if(happiness >= maxHappiness)
        {
            EndAllRequest();
            //Win
            MainMenu.instance.Win();
            spawnRequest = false;
            started = false;
        }
        else if(happiness <= 0)
        {
            EndAllRequest();
            //Loose
            MainMenu.instance.Loose();
            spawnRequest = false;
            started = false;
            wave1Line.positionCount = 0;
            wave2Line.positionCount = 0;
            wave3Line.positionCount = 0;
            wave4Line.positionCount = 0;
            foreach(Animator p in playerAnims)
            {
                p.enabled = false;
            }
            crowdAnims.enabled = false;

        }
    }

    public void NewRequest()
    {
        if (requests.Count > 5 || !spawnRequest)
            return;
        Request r = Instantiate(requestPrefab, new Vector3(Random.Range(-7f,8f), Random.Range(-3.5f,-4f),0),  Quaternion.identity).transform.GetComponent<Request>();
        requests.Add(r);
    }

    public void EndRequest(Request r)
    {
        requests.Remove(r);
        Destroy(r.gameObject,0.6f);
    }

    private void EndAllRequest()
    {
        spawnRequest = false;
        foreach(Request r in requests)
        {
            r.anim.Play("Request_fail");
            Destroy(r.gameObject, 0.6f);
        }
    }
}
