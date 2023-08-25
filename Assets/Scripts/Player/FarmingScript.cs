using System.Collections.Generic;
using UnityEngine;

public class FarmingScript : MonoBehaviour
{
    RaycastHit2D hit;

    AtmosphereManager atmosphere;

    [SerializeField] List<Plant> plants = new List<Plant>();

    [SerializeField] bool holdingPlantSeed;
    [SerializeField] bool holdingWaterCan;
    [SerializeField] bool holdingReinforcementItem;
    [SerializeField] bool holdingAutoWaterSystemItem;
    void Start()
    {
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastSystem();
        }
    }
    public void RaycastSystem()
    {
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Dirt" && GetComponent<Character>().modeName == "Farming")
            {
                Dirt dirt = hit.collider.gameObject.GetComponent<Dirt>();
                if (!dirt.isHoed && !dirt.isPlanted && !holdingPlantSeed && !holdingWaterCan && !holdingReinforcementItem)
                {
                    dirt.isHoed = true;
                }
                else if(dirt.isHoed && !dirt.isPlanted && !holdingPlantSeed && !holdingWaterCan && !holdingReinforcementItem)
                {
                    dirt.isHoed = false;
                }
                else if (!dirt.isPlanted && dirt.isWeatherSituationGood && dirt.isHoed && holdingPlantSeed)
                {
                    dirt.isPlanted = true;
                    dirt.plant.GetComponent<SpriteRenderer>().sprite = dirt.plant.phaseSprites[0];
                }
                else if (dirt.isPlanted && holdingWaterCan)
                {
                    dirt.waterValue += 25f;
                }
                else if (!holdingPlantSeed && !holdingWaterCan && holdingReinforcementItem && dirt.isHoed && !dirt.isReinforced)
                {
                    dirt.isReinforced = true;
                }
                else if(!holdingPlantSeed && !holdingWaterCan && !holdingReinforcementItem && !dirt.isWaterAuto && dirt.isHoed && holdingAutoWaterSystemItem)
                {
                    dirt.isWaterAuto = true;
                }
                if (dirt.isCuttable)
                {
                    //objeyi kes tohum ve ürün ver objeyi de destroyla...
                    dirt.coroutineAlreadyStarted = false;
                }
            }
        }
    }
}
