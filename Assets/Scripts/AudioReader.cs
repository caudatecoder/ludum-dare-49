using UnityEngine;

public class AudioReader : MonoBehaviour
{
    public AudioSource[] audioSources;
    public float output;

    float[] spectrum = new float[256];
    float resultBand;
    float resultBandBuffer = 0;
    float bufferDecrease;
    float highestValue = 0;

    private void BufferOutput()
    {
        if (resultBand > resultBandBuffer)
        {
            resultBandBuffer = resultBand;
            bufferDecrease = 0.001f;
        }

        if (resultBand < resultBandBuffer)
        {
            resultBandBuffer -= bufferDecrease;
            bufferDecrease *= 1.1f;
        }
    }

    private void Update()
    {
        ReadSpectrumData();
        BufferOutput();
        NormalizeValue();
    }

    private void NormalizeValue()
    {
        if (resultBand > highestValue)
        {
            highestValue = resultBand;
        }

        output = resultBandBuffer / highestValue;
    }

    private void ReadSpectrumData()
    {
        resultBand = 0;

        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

            float average = 0;
            for (int j = 0; j < spectrum.Length; j++)
            {
                average += spectrum[j] * (j + 1);
            }

            average /= spectrum.Length;

            resultBand += average;
        }
    }
}
