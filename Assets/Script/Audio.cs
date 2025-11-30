using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance = null;

    [SerializeField] private AudioClip[] playListe;
    [SerializeField] private AudioSource sourceMusic;
    [SerializeField] private AudioSource[] sourceCrowd;

    private int currentSong = 0;

    private void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        //currentSong = Random.Range(0, playListe.Length);
        StartCoroutine(FadeOut(1.5f));
        StartCoroutine(StartCrowd());
    }

    private void NextSong()
    {
        sourceMusic.Stop();
        sourceMusic.clip = playListe[currentSong];
        sourceMusic.Play();
        Invoke("NextSong", playListe[currentSong].length);

        currentSong++;
        if (currentSong >= playListe.Length)
            currentSong = 0;
    }

    public void StopSound()
    {
        sourceMusic.Stop();
        foreach (AudioSource source in sourceCrowd)
        {
            source.Stop();

        }
    }

    System.Collections.IEnumerator StartCrowd()
    {
        foreach(AudioSource source in sourceCrowd)
        {
            yield return new WaitForSeconds(2f);
            source.Play();
            yield return new WaitForSeconds(Random.Range(2, 6));

        }
    }

    System.Collections.IEnumerator FadeOut(float duration)
    {
        float startVolume = sourceMusic.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            sourceMusic.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        sourceMusic.volume = 0f;

        sourceMusic.Stop();

        sourceMusic.volume = 1f;

        NextSong();

    }

}
