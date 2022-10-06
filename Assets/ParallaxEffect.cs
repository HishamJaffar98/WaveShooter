using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] [Range(0, 1)] float parallaxFactor;
    private float spriteLength;
    private float spriteStartPos;
    void Start()
    {
        spriteStartPos = transform.position.x;
        spriteLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distanceToMove = mainCamera.transform.position.x * parallaxFactor;
        float remainingDistanceOfSprite = mainCamera.transform.position.x * (1 - parallaxFactor);
        transform.position = new Vector3(spriteStartPos+distanceToMove, transform.position.y, transform.position.z);
        if (remainingDistanceOfSprite > spriteStartPos + spriteLength) 
            spriteStartPos += spriteLength;
    }
}
