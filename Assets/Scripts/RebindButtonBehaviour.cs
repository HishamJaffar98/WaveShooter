using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum RebindProcessStates {Idle, Begin, OnGoing, End};
public class RebindButtonBehaviour : MonoBehaviour
{
    [SerializeField] GameObject rebindPromptCanvas;
    [SerializeField] Animator rebindPromptAnimator;
    [SerializeField] GameObject textDisplayUnit;
    [SerializeField] GameObject iconDisplayUnit;
    [SerializeField] int compositeOffset;
    [SerializeField] BindIcons controllerBindIcons;
    [SerializeField] InputActionReference focusedInputAction;
    [SerializeField] bool isComposite=false;

    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    private RebindProcessStates currentRebindProcessState = RebindProcessStates.Idle;
    private int bindIndex = -1;
    string currentBindingInput;
    void Start()
	{
		InputController.Instance.GetActionsBasedOnActionMapName(InputActionMapNames.PLAYER_INPUT_ACTION_MAP_NAME);
	}

	// Update is called once per frame
	void Update()
    {
        if(currentRebindProcessState==RebindProcessStates.Begin && rebindPromptCanvas.activeSelf==true)
		{
            Debug.Log(gameObject.name);
            if (rebindPromptAnimator.GetCurrentAnimatorStateInfo(0).IsName("Entry") && rebindPromptAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                currentRebindProcessState = RebindProcessStates.OnGoing;
                StartRebindProcess();
            }
        }

        if (currentRebindProcessState == RebindProcessStates.End && rebindPromptCanvas.activeSelf == true)
        {
            Debug.Log(gameObject.name);
            if (rebindPromptAnimator.GetCurrentAnimatorStateInfo(0).IsName("Exit") && rebindPromptAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                currentRebindProcessState = RebindProcessStates.Idle;
                rebindPromptCanvas.SetActive(false);
                UpdateRebindUnitUIComponent();
            }
        }
    }

	private void UpdateRebindUnitUIComponent()
	{
		Sprite newIcon = null;
		currentBindingInput = InputControlPath.ToHumanReadableString(focusedInputAction.action.bindings[bindIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
		newIcon = controllerBindIcons.GetIconBasedOnBindName(currentBindingInput);

        if (newIcon != null)
		{
            iconDisplayUnit.SetActive(true);
			textDisplayUnit.SetActive(false);
			iconDisplayUnit.GetComponent<Image>().sprite = newIcon;
		}
		else
		{
            iconDisplayUnit.SetActive(false);
			textDisplayUnit.SetActive(true);
            textDisplayUnit.GetComponent<TextMeshProUGUI>().text = currentBindingInput;

        }
    }

	private void StartRebindProcess()
	{
        rebindOperation?.Cancel();
        if (DeviceManager.Instance.CurrentInputScheme == AvailableInputSchemes.KEYBOARD_AND_MOUSE)
        {
            bindIndex = 0;
            if(isComposite)
			{
                bindIndex += compositeOffset;
            }
        }
        else
		{
            bindIndex = 1;
            if(isComposite)
			{
                bindIndex = bindIndex + compositeOffset + 4;
            }
		}
        rebindOperation = focusedInputAction.action.PerformInteractiveRebinding(bindIndex)
         .WithControlsExcluding("<Mouse>/position")
         .WithControlsExcluding("<Mouse>/delta")
         .WithControlsExcluding("<Gamepad>/Start")
         .WithControlsExcluding("<Gamepad>/Select")
         .WithControlsExcluding("<Keyboard>/escape")
         .WithControlsExcluding("<Gamepad>/LeftStick/X")
         .WithControlsExcluding("<Gamepad>/LeftStick/Y")
         .WithControlsExcluding("<Gamepad>/D-Pad/X")
         .WithControlsExcluding("<Gamepad>/D-Pad/Y")
         .WithControlsExcluding("<Gamepad>/RightStick/X")
         .WithControlsExcluding("<Gamepad>/RightStick/Y")

         .OnMatchWaitForAnother(0.1f)
         .OnComplete(operation => RebindCompleted());

        rebindOperation.Start();

    }
    private void CleanRebindOperation()
    {
        rebindOperation?.Dispose();
        rebindOperation = null;
    }

    void RebindCompleted()
    {
        currentBindingInput = InputControlPath.ToHumanReadableString(focusedInputAction.action.bindings[0].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        CleanRebindOperation();
        rebindPromptAnimator.SetTrigger("ClosePromptCanvas");
        currentRebindProcessState = RebindProcessStates.End;
    }

    public void RebindAction()
	{
        rebindPromptCanvas.SetActive(true);
        currentRebindProcessState = RebindProcessStates.Begin;
    }

    public void ButtonPressedResetBinding()
    {
        ResetBinding();
    }

    private void ResetBinding()
    {
        if(bindIndex==-1)
        { return; }

        InputActionRebindingExtensions.RemoveBindingOverride(focusedInputAction.action, bindIndex);
        UpdateRebindUnitUIComponent();
    }
}
