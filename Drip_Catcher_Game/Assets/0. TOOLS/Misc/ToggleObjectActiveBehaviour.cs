using UnityEngine;

public class ToggleObjectActiveBehaviour : MonoBehaviour
{
    public GameObject objectToToggle;
    public bool toggleOnStart = true;
    
    void Start()
    {
        if (toggleOnStart)
        {
            ToggleObjectActivation();
        }
    }

    public void ToggleObjectActivation()
    {
        if (objectToToggle.activeSelf)
        {
            objectToToggle.SetActive(false);
        }
        else
        {
            objectToToggle.SetActive(true);
        }
    }
}
