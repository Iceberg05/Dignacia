using UnityEngine;

public class Plant : MonoBehaviour
{
    [Tooltip("Bitkinin tüm fazlarýdýr.")]
    public Sprite[] phaseSprites;

    [Tooltip("Olmasý gereken optimum sýcaklýk deðeridir.")]
    public float optimumTemperatureValue;
    [Tooltip("Olmasý gereken optimum atmosfer deðeridir.")]
    public float optimumAtmosphereValue;

    [Tooltip("Bitkini kaç saniyede faz atladýðýný belirten deðerdir.")]
    public float changePhaseTime = 120f;
}
