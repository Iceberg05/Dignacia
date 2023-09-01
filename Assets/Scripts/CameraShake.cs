using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private float shakeDuration = 0f;
    private float shakeAmplitude = 1.2f;
    private float shakeFrequency = 2.0f;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = 0f;
    }

    public void ShakeCamera(float duration)
    {
        shakeDuration = duration;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            noise.m_AmplitudeGain = shakeAmplitude;
            noise.m_FrequencyGain = shakeFrequency;

            shakeDuration -= Time.deltaTime;
        }
        else
        {
            noise.m_AmplitudeGain = 0f;
            shakeDuration = 0f;
        }
    }
}