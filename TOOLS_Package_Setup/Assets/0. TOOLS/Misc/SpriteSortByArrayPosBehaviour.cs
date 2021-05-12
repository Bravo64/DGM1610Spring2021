using UnityEngine;

public class SpriteSortByArrayPosBehaviour : MonoBehaviour
{
    public bool beginOnStart = true;
    public SpriteRenderer[] spriteSet;
    public int layerAddition = 0; 
    public int layerMultiplier = 1;
    public bool invertLayerPositions = false;

    private int _inverter = 1;

    void Start()
    {
        if (invertLayerPositions)
        {
            _inverter = -1;
        }
        else
        {
            _inverter = 1;
        }
        
        if (beginOnStart)
        {
            SortSpritesByArrayPosition();
        }
    }
    
    public void SortSpritesByArrayPosition()
    {
        for (int i = 0; i < spriteSet.Length; i++)
        {
            spriteSet[i].sortingOrder += (i * layerMultiplier + layerAddition) * _inverter;
        }
    }
}
