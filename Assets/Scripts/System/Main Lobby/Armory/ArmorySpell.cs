using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorySpell : MonoBehaviour {
    
    public int arrayNumber;

    public int spellIndex;

    private Image _spellIcon;

	void Start () {
        //_spellIcon = Resources.Load("/Spells" + "/" + spellIndex.ToString()).GetComponent<SpellHolder>().GetSpellIcon();
        //gameObject.GetComponent<Image>().sprite = _spellIcon.sprite;
        gameObject.transform.Find("Text").GetComponent<Text>().text = spellIndex.ToString();
	}
	
	void Update () {
        gameObject.transform.Find("Text").GetComponent<Text>().text = spellIndex.ToString();
    }

    public void AddSpellIndex()
    {
        spellIndex += 1;
    }
}
