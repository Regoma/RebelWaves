using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    float[] spectrum = new float[512];
    float[] barValues = new float[8];

    public float smoothSpeed = 8f;
    public float heightMultiplier = 40f;

    float[] bandBoost = {24f,8f,4f,2f,1f,1f,1f,1f};

    int[] freqBands = { 2, 4, 8, 16, 32, 64, 128, 256 };

    void Update()
    {
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);

        int index = 0;

        for (int i = 0; i < 8; i++)
        {
            float sum = 0;
            int sampleCount = freqBands[i];

            for (int j = 0; j < sampleCount; j++)
            {
                sum += spectrum[index] * (index + 1);
                index++;
            }

            // Boost par bande
            float target = sum * heightMultiplier * bandBoost[i];

            barValues[i] = Mathf.Lerp(barValues[i], target, Time.deltaTime * smoothSpeed);

            transform.GetChild(i).localScale = new Vector3(1, Mathf.Max(0.1f, barValues[i]), 1);
        }
    }

}
