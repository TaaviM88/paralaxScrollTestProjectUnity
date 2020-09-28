using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBagroundMonkey : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    [SerializeField] Vector2 parallaxEffectMultiplier;
    [SerializeField] private bool infiniteHorizontal;
    [SerializeField] private bool infiniteVertical;

    private float textureUnitySizeX;
    private float textureUnitySizeY;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitySizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitySizeY = texture.height / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y, transform.position.z);
        lastCameraPosition = cameraTransform.position;
        if(infiniteHorizontal)
        {
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitySizeX)
            {
                float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitySizeX;
                transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
            }
        }

        if(infiniteVertical)
        {
            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitySizeY)
            {
                float offsetPositionY = (cameraTransform.position.x - transform.position.x) % textureUnitySizeX;
                transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY);
            }
        }

    }
}
