using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Bind Visual Component File", menuName = "Bind Visual Component")]
public class BindVisualComponents : ScriptableObject
{
	public string[] bindingTexts;
	public Sprite[] bindingIcons;
}
