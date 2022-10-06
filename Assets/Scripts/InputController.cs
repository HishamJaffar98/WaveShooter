using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public static class InputActionMapNames
{
    public const string PLAYER_INPUT_ACTION_MAP_NAME = "Player_Input";
    public const string UI_INPUT_ACTION_MAP_NAME = "UI_Input";
}
public class InputController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] InputActionAsset currentInputActionAsset;

    List<InputAction> currentInputActions = new List<InputAction>();
    InputActionMap currentInputActionMap;

    List<InputActionMap> allInputActionMaps = new List<InputActionMap>();
    public static InputController Instance;

    public PlayerInput PlayerInput
	{
		get
		{
            return playerInput;
		}
	}

	public List<InputAction> CurrentInputActions
    {
        get
        {
            return currentInputActions;
        }
    }

    public InputActionMap CurrentInputActionMap
    {
        get
        {
            return currentInputActionMap;
        }
        set
		{
            currentInputActionMap = value;
		}
    }

	private void OnEnable()
	{
        LevelLoader.OnNewLevelLoaded += ChangeCurrentInputActionMap;
    }

	private void Awake()
	{
        if (Instance != null)
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
		GetAllInputActionMaps();
		currentInputActionMap = playerInput.currentActionMap;
		UpdateCurrentInputActionsList();
	}

	private void OnDisable()
	{
        LevelLoader.OnNewLevelLoaded -= ChangeCurrentInputActionMap;

    }

    private void ChangeCurrentInputActionMap(string newInputActionMap)
	{
        playerInput.SwitchCurrentActionMap(newInputActionMap);
        UpdateCurrentLocalInputActionMap(playerInput.currentActionMap);
    }

	private void GetAllInputActionMaps()
	{
		for (int i = 0; i < currentInputActionAsset.actionMaps.Count; i++)
		{
			allInputActionMaps.Add(currentInputActionAsset.actionMaps[i]);
		}
	}

	private void UpdateCurrentLocalInputActionMap(InputActionMap newInputActionMap)
	{
        if(currentInputActionMap!=newInputActionMap)
		{
            currentInputActionMap = newInputActionMap;
            UpdateCurrentInputActionsList();
		}
     
    }

    private void UpdateCurrentInputActionsList()
	{
        if(currentInputActions!=null)
		{
            currentInputActions.Clear();
        }

        for (int i = 0; i < currentInputActionMap.actions.Count; i++)
        {
            currentInputActions.Add(currentInputActionMap.actions[i]);
        }
    }

    public void GetActionsBasedOnActionMapName(string actionMapName)
    { 
        switch(actionMapName)
		{
            case InputActionMapNames.PLAYER_INPUT_ACTION_MAP_NAME:
                UpdateCurrentLocalInputActionMap(allInputActionMaps[0]);
                break;
            case InputActionMapNames.UI_INPUT_ACTION_MAP_NAME:
                UpdateCurrentLocalInputActionMap(allInputActionMaps[1]);
                break;
		}
    }
}
