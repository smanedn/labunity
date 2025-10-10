using UnityEngine;

public class MenuToggle : MonoBehaviour
{
    public GameObject menuObject;
    public void ToggleMenu()
    {
        if (menuObject != null)
        {
            bool isActive = menuObject.activeSelf;
            menuObject.SetActive(!isActive);
        }

    }
}
