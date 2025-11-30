using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance = null;

    public AudioClip endSound;
    private Animator anim;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {

        anim.Play("Game_play");
        StartCoroutine(EStart());

    }

    System.Collections.IEnumerator EStart()
    {
        yield return new WaitForSeconds(0.25f);
        Audio.instance.Init();
        yield return new WaitForSeconds(1f);
        Waves.instance.Init();
    }

    public void Win()
    {
        StartCoroutine(EWin());
    }

    System.Collections.IEnumerator EWin()
    {
        anim.Play("Game-end_sucess");
        AudioSource.PlayClipAtPoint(endSound, transform.position);
        yield return new WaitForSeconds(3.5f);
        Audio.instance.StopSound();
    }
}
