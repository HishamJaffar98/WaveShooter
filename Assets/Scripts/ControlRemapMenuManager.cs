using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ControlRemapMenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI headingText;
	[SerializeField] GameObject[] currentInputTexts;
	[SerializeField] GameObject[] currentInputIcons;
	[SerializeField] BindVisualComponents bindVisualComponents;
	bool initialKeyboardSetupDone = false;
	bool initialControllerSetupDone = false;
	private void OnEnable()
	{
		DeviceManager.DeviceIsChanged += UpdateRemapMenu;
	}

	private void OnDisable()
	{
		DeviceManager.DeviceIsChanged -= UpdateRemapMenu;
	}

	void Start()
	{
		SetAppropriateBinds();
		ActivateAppropriateRebindComponents();
	}

	private static void SetAppropriateBinds()
	{
		
	}

	private void ActivateAppropriateRebindComponents()
	{
		int visualComponentIndex = 0;
		if (DeviceManager.Instance.CurrentInputScheme == "Keyboard & Mouse")
		{
			foreach (GameObject inputText in currentInputTexts)
			{
				inputText.SetActive(true);
				if(!initialKeyboardSetupDone)
				{
					//inputText.GetComponent<TextMeshProUGUI>().text = bindVisualComponents.bindingTexts[visualComponentIndex];
					visualComponentIndex++;
				}
			}

			foreach (GameObject inputIcons in currentInputIcons)
			{
				inputIcons.SetActive(false);
			}
			initialKeyboardSetupDone = true;
		}
		
		else if (DeviceManager.Instance.CurrentInputScheme == "Xbox Controller")
		{
			foreach (GameObject inputText in currentInputTexts)
			{
				inputText.SetActive(false);
			}
			foreach (GameObject inputIcons in currentInputIcons)
			{
				inputIcons.SetActive(true);
				if(!initialControllerSetupDone)
				{
					//inputIcons.GetComponent<Image>().sprite = bindVisualComponents.bindingIcons[visualComponentIndex];
					visualComponentIndex++;
				}
			}
			initialControllerSetupDone = true;
		}
	}

	private void UpdateRemapMenu(string deviceName)
	{
		headingText.text = deviceName.ToUpper();
		ActivateAppropriateRebindComponents();
	}
}
