using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorySpell : MonoBehaviour {
    
    public int arrayNumber;

    public short spellIndex;

    public short spellVariation;

    private Image _spellIcon;
    private Button _variationButton;

	void Start ()
    {
        //_spellIcon = Resources.Load("/Spells" + "/" + spellIndex.ToString()).GetComponent<SpellHolder>().GetSpellIcon();
        //gameObject.GetComponent<Image>().sprite = _spellIcon.sprite;
        gameObject.transform.Find("Text").GetComponent<Text>().text = spellIndex.ToString();
        gameObject.transform.Find("Variation").Find("Text").GetComponent<Text>().text = spellVariation.ToString();
    }
	
	void Update ()
    {
        gameObject.transform.Find("Text").GetComponent<Text>().text = spellIndex.ToString();
        gameObject.transform.Find("Variation").Find("Text").GetComponent<Text>().text = spellVariation.ToString();
    }

    public void AddSpellIndex()
    {
        spellIndex += 1;
        if (spellIndex > 5)
            spellIndex = 0;
    }

    public void AddVariationIndex()
    {
        spellVariation += 1;
        if (spellVariation > 3)
            spellVariation = 1;
    }
}
