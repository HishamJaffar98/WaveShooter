using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum Scene { None, StartMenu, CharacterSelect, Game, RemapControls, QuitGame };
public class LevelLoader : MonoBehaviour
{
    private Scene sceneToTransitionTo = Scene.None;
    [SerializeField] Animator faderAnimator;

	public static event Action<string> OnNewLevelLoaded;
	public static LevelLoader Instance;

	public void Awake()
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

	private void Update()
	{
		TransitionToSceneAfterFadeOut();
	}

	private void TransitionToSceneAfterFadeOut()
	{
		if (faderAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeOut") && faderAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
		{
			switch (sceneToTransitionTo)
			{
				case Scene.StartMenu:
					SceneManager.LoadScene(0);
					break;
				case Scene.CharacterSelect:
					SceneManager.LoadScene(1);
					break;
				case Scene.RemapControls:
					SceneManager.LoadScene(2);
					break;
				case Scene.Game:
					SceneManager.LoadScene(3);
					break;
				case Scene.QuitGame:
					Application.Quit();
					break;

			}
		}
	}

	private void SetSceneToTransitionTo(Scene nextScene, string actionMapToSwitchTo)
	{
		sceneToTransitionTo = nextScene;
		faderAnimator.SetTrigger("fadeOutStart");
		if (actionMapToSwitchTo == "")
			return;
		OnNewLevelLoaded?.Invoke(actionMapToSwitchTo);
	}

	public void GoToRemapControlsScene()
	{
		SetSceneToTransitionTo(Scene.RemapControls, InputActionMapNames.UI_INPUT_ACTION_MAP_NAME);
	}

	public void GoToGame()
	{
		SetSceneToTransitionTo(Scene.Game, InputActionMapNames.PLAYER_INPUT_ACTION_MAP_NAME);
	}

	public void QuitGame()
	{
		SetSceneToTransitionTo(Scene.QuitGame,"");
	}

	public void GoToStartMenu()
	{
		SetSceneToTransitionTo(Scene.StartMenu,InputActionMapNames.UI_INPUT_ACTION_MAP_NAME);
	}

	public void GoToCharacterSelect()
	{
		SetSceneToTransitionTo(Scene.CharacterSelect, InputActionMapNames.UI_INPUT_ACTION_MAP_NAME);
	}
}

