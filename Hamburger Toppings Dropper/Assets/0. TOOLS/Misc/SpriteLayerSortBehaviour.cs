using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class SpriteLayerSortBehaviour : MonoBehaviour
{
    public enum Modes { UpdateOnTriggerOnly, ConstantLayerUpdates }
    public enum Axes { X, Y }

    public Modes mode = Modes.UpdateOnTriggerOnly;
    public int layersPerUnit = 10;
    public Axes shiftAlongAxis = Axes.Y;
    public bool applyToParent = true;
    public int worldOriginLayerNumber = 0;
    public bool invertLayerPositions = false;

    private SpriteRenderer _spriteRenderer;
    private int _currentSortingLayer;
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;

        if (applyToParent)
        {
            _spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        }
        else
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (mode == Modes.ConstantLayerUpdates)
        {
            StartCoroutine(ConstantlyUpdateLayers());
        }
    }

    IEnumerator ConstantlyUpdateLayers()
    {
        while (true)
        {
            UpdateSpriteSortingLayer();
            yield return 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        UpdateSpriteSortingLayer();
    }

    public void UpdateSpriteSortingLayer()
    {
        switch (shiftAlongAxis)
        {
            case Axes.X:
                _currentSortingLayer = Mathf.RoundToInt(transform.position.x * layersPerUnit + worldOriginLayerNumber);
                break;
            case Axes.Y:
                _currentSortingLayer = Mathf.RoundToInt(transform.position.y * layersPerUnit + worldOriginLayerNumber);
                break;
        }
        
        if (invertLayerPositions)
        {
            _currentSortingLayer = -_currentSortingLayer;
        }
        _spriteRenderer.sortingOrder = _currentSortingLayer;
    }
}