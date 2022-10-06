using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
public static class AvailableInputSchemes
{
    public const string KEYBOARD_AND_MOUSE = "Keyboard & Mouse";
    public const string XBOX_CONTROLLER = "Xbox Controller";
}

[System.Serializable]
public struct DeviceDisplayNames
{
    public string XboxControllerDisplayName;
    public string KeyboardDisplayName;
    public string MouseDisplayName;
}

public class DeviceManager : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] TextMeshProUGUI deviceChangedNotifObject;
    [SerializeField] DeviceDisplayNames deviceDisplayNames;
    private string currentInputScheme;

    public static event Action<string> DeviceIsChanged;
    public static DeviceManager Instance;

    public string CurrentInputScheme
	{
		get
		{
            return currentInputScheme;
		}
	}

	private void Awake()
	{
		if(Instance!=null)
		{
            Destroy(gameObject);
		}
        else
		{
            Instance = this;
            DontDestroyOnLoad(gameObject);
		}
	}
	void Start()
    {
        currentInputScheme = playerInput.currentControlScheme;
        DeviceIsChanged?.Invoke(currentInputScheme);
    }

    void Update()
    {
        
    }

    public void OnDeviceChanged()
	{
        if (playerInput==null)
            return;

        if(playerInput.currentControlScheme!=currentInputScheme)
		{
            currentInputScheme = playerInput.currentControlScheme;
            if(playerInput.devices[0].displayName == deviceDisplayNames.KeyboardDisplayName && playerInput.devices[1].displayName == deviceDisplayNames.MouseDisplayName)
			{
                deviceChangedNotifObject.text = "Current Input Device: Keyboard and Mouse ";
            }
            else if(playerInput.devices[0].displayName == deviceDisplayNames.XboxControllerDisplayName)
			{
                deviceChangedNotifObject.text = "Current Input Device: Xbox Controller ";
            }
        }
        DeviceIsChanged?.Invoke(currentInputScheme);
    }
}
