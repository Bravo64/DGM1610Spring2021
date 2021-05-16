using UnityEngine;

public class SwipeInputsBehaviour : MonoBehaviour {

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown, simpleTap, isDragging, justSwiped;
    private Vector2 startTouch, swipeDelta;
    private float _swipePixelLength = 25.0f;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool SimpleTap { get { return simpleTap; } }


    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = simpleTap = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            if (swipeDelta.magnitude < 125 && !justSwiped)
            {
                simpleTap = true;
            }
            justSwiped = false;
            Reset();
        }
        #endregion

        #region Mobile Inputs
            if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDragging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                if (swipeDelta.magnitude < 125 && !justSwiped && Input.touches[0].phase == TouchPhase.Ended)
                {
                    simpleTap = true;
                }
                justSwiped = false;
                Reset();
            }
        }
        #endregion

        swipeDelta = Vector2.zero;
            if (isDragging)
            {
                if (Input.touches.Length > 0)
                    swipeDelta = Input.touches[0].position - startTouch;
                else if (Input.GetMouseButton(0))
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }

        if (swipeDelta.magnitude > _swipePixelLength)
        {
            simpleTap = false;
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }
            justSwiped = true;
            Reset();
        }

    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }
}
