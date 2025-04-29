using UnityEngine;

public class VizelizerEffect : AudioManager
{
   [SerializeField] RectTransform visualizerObj;
    public float updateSendivity;
    Vector3 newSize;
    public int intenity = 0;
    private void Awake()
    {
        newSize = visualizerObj.sizeDelta;
    }
    private void Update()
    {
        float[] spectrumData = new float[visualizerSimples];
        AudioListener.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);
        float intensity = spectrumData[intenity] * 100;
        Vector3 targetSize = minSize + (intensity * (MaxSize - minSize) * 5.0f);

        newSize.x = Mathf.Clamp(
            Mathf.Lerp(newSize.x, targetSize.x, updateSendivity),
            minSize.x,
            MaxSize.x
        );

        newSize.y = Mathf.Clamp(
            Mathf.Lerp(newSize.y, targetSize.y, updateSendivity),
            minSize.y,
            MaxSize.y
        );

        visualizerObj.sizeDelta = newSize;
    }
}
