using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoryController : MonoBehaviour {

    private static ArmorySpell[] IgnisButtons = new ArmorySpell[9];

	void Start ()
    {
        IgnisButtons[0] = gameObject.transform.Find("IgnisView").Find("Viewport").Find("Content").Find("SpellMain").GetComponent<ArmorySpell>();
        for (int i = 1; i < IgnisButtons.Length; i++)
        {
            IgnisButtons[i] = gameObject.transform.Find("IgnisView").Find("Viewport").Find("Content").Find("Spell" + i.ToString()).GetComponent<ArmorySpell>();
        }
    }

    public static void SetSkills(short[] Spells, short[] Variations)
    {
        for(int i = 0; i < IgnisButtons.Length; i++)
        {
            IgnisButtons[i].spellIndex = Spells[i];
            IgnisButtons[i].spellVariation = Variations[i];
        }
    }

    public static void SaveBuild()
    {
        short[] _build = new short[9];
        short[] _variation = new short[9];
        for (int i = 0; i < IgnisButtons.Length; i++)
        {
            _build[i] = IgnisButtons[i].spellIndex;
            _variation[i] = IgnisButtons[i].spellVariation;
        }
        MainLobbyController.GetInProcess("Saving...");
        SendDataTCP.SendSaveSkillBuild(_build, _variation, "Ignis");
    }
}
