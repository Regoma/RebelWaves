using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance = null;

    public AudioClip endSound;
    public AudioClip looseSound;
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

    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    System.Collections.IEnumerator EStart()
    {
        yield return new WaitForSeconds(0.5f);
        Audio.instance.Init();
        yield return new WaitForSeconds(1f);
        Waves.instance.Init();
    }

    public void Loose()
    {
        StartCoroutine(ELoose());
    }

    System.Collections.IEnumerator ELoose()
    {
        Audio.instance.LoosingSound();
        yield return new WaitForSeconds(0.5f);
        anim.Play("Game-end_loose");
        AudioSource.PlayClipAtPoint(looseSound, transform.position);
        yield return new WaitForSeconds(0.5f);
        Cursor.visible = true;
    }

    public void Win()
    {
        StartCoroutine(EWin());
    }

    System.Collections.IEnumerator EWin()
    {
        anim.Play("Game-end_sucess");
        AudioSource.PlayClipAtPoint(endSound, transform.position);
        yield return new WaitForSeconds(2.5f);
        Audio.instance.StopSound();
        Cursor.visible = true;
    }
}
