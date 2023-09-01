using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSkillUse : MonoBehaviour
{
    public RogueLiteCharacter rogueLitecharacter;
    public int Roomnumber;
    public bool healthChip;
    public bool healthpill;
    public bool ArmorUpgrader;
    public bool Soulpill;
    //SÝLAH YETENEKLERÝ SONRA EKLENECEK


    private void Start()
    {
        Roomnumber = 0;
    }
    private void HealtChip()
    {     
            rogueLitecharacter.MaxHealthValue = rogueLitecharacter.MaxHealthValue + 20;
            rogueLitecharacter.HealthValue = rogueLitecharacter.HealthValue + 20;
    }

    private void HealtPill()
    {
        rogueLitecharacter.HealthValue = rogueLitecharacter.HealthValue + 20;
    }

    private void ArmorUpgrade()
    {
        rogueLitecharacter.ArmorValue = rogueLitecharacter.ArmorValue + 20;
    }

    private void SoulPill()
    {
        rogueLitecharacter.SoulControlTalisman = rogueLitecharacter.SoulControlTalisman + 1;
    }
}
