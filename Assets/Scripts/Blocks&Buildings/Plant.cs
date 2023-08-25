using UnityEngine;

public class Plant : MonoBehaviour
{
    [Tooltip("Bitkinin t�m fazlar�d�r.")]
    public Sprite[] phaseSprites;

    [Tooltip("Olmas� gereken optimum s�cakl�k de�eridir.")]
    public float optimumTemperatureValue;
    [Tooltip("Olmas� gereken optimum atmosfer de�eridir.")]
    public float optimumAtmosphereValue;

    [Tooltip("Bitkini ka� saniyede faz atlad���n� belirten de�erdir.")]
    public float changePhaseTime = 120f;
}
