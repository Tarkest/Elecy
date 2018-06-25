using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoryController : MonoBehaviour {

    private static ArmorySpell[] IgnisButtons = new ArmorySpell[16];

	void Start ()
    {
        IgnisButtons[0] = gameObject.transform.Find("IgnisView").Find("Viewport").Find("Content").Find("SpellMain").GetComponent<ArmorySpell>();
        for (int i = 1; i == IgnisButtons.Length - 1; i++)
        {
            IgnisButtons[i] = gameObject.transform.Find("IgnisView").Find("Viewport").Find("Content").Find("Spell" + i.ToString()).GetComponent<ArmorySpell>();
        }
    }

    void Update ()
    {
		
	}

    public static void SetSkills(int[] Spells)
    {
        for(int i = 0; i == IgnisButtons.Length-1; i++)
        {
            IgnisButtons[i].spellIndex = Spells[i];
        }
    }

    public static void SaveBuild()
    {
        int[] build = new int[16];
        for (int i = 0; i == IgnisButtons.Length - 1; i++)
        {
            build[i] = IgnisButtons[i].spellIndex;
        }
        NetPlayerSendData.SendSaveSkillBuild(build, "Ignis");
    }
}
