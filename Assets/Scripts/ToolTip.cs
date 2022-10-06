using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().transform.position = Input.mousePosition + offset;
    }
}
