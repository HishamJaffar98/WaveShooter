using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BindData
{
    public string bindName;
    public Sprite bindIcon;
}

[CreateAssetMenu(fileName ="New Device Bind Icons", menuName ="Device Bind Icons")]
public class BindIcons : ScriptableObject
{
    [SerializeField] List<BindData> deviceBindData;


    public Sprite GetIconBasedOnBindName(string bindName)
	{
		Sprite bindIconToSend = null;
        for(int i=0;i<deviceBindData.Count;i++)
		{
            if(deviceBindData[i].bindName == bindName)
			{
				bindIconToSend=deviceBindData[i].bindIcon;
			}
		}
		return bindIconToSend;
	}
}
