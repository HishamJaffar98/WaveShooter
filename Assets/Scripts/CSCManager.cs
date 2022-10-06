using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CSCManager : MonoBehaviour
{
    [SerializeField] GameObject toolTip;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] GameObject[] characterSpriteModels;
    [SerializeField] Slider[] statSliders;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverOverCharacterIcons(string characterName)
	{
        toolTip.SetActive(true);
        toolTip.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = characterName;
    }

    public void OnHoverExitCharacterIcons()
    {
        toolTip.SetActive(false);
        toolTip.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void OnIconSelected(string characterName)
	{
        title.text = characterName;
	}

    public void ShowIcon(int iconIndex)
	{
        foreach(GameObject characterSprite in characterSpriteModels)
		{
            characterSprite.SetActive(false);
        }
        characterSpriteModels[iconIndex].SetActive(true);
    }

    public void GetHealthStat(int statValue)
	{
        statSliders[0].value = statValue;

    }
    public void GetDamageStat(int statValue)
    {
        statSliders[1].value = statValue;
    }
    public void GetStaminaStat(int statValue)
    {
        statSliders[2].value = statValue;
    }

    public void ShowStatTooltip(int statIndex)
	{
        switch(statIndex)
		{
            case 0:
                toolTip.SetActive(true);
                toolTip.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = statSliders[0].value.ToString() + "/100";
                break;
            case 1:
                toolTip.SetActive(true);
                toolTip.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = statSliders[1].value.ToString() + "/100";
                break;
            case 2:
                toolTip.SetActive(true);
                toolTip.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = statSliders[2].value.ToString() + "/100";
                break;
		}
	}
}
