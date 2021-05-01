using UnityEngine;
using UnityEngine.UI;

public class ColorData : MonoBehaviour
{
    public Color value;
    
    public void ChangeImageColor(Image imgObj)
    {
        imgObj.color = value;
    }

    public void ChangeMeshRendererColor(MeshRenderer meshObj)
    {
        meshObj.material.color = value;
    }
    
    public void ChangeSpriteRendererColor(SpriteRenderer spriteObj)
    {
        spriteObj.color = value;
    }
    
    public void ChangeCameraBackgroundColor(Camera cameraObj)
    {
        cameraObj.backgroundColor = value;
    }
}
