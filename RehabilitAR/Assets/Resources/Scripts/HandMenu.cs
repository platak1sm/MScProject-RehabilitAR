using UnityEngine;

public class HandMenuController : MonoBehaviour
{
    public GameObject handMenu;   
    private bool isMenuActive = false;

    void Start()
    {
        if (!handMenu) Debug.LogError("Hand menu missing!");

        // Initially inactive
        handMenu.transform.GetChild(0).gameObject.SetActive(false);
        handMenu.transform.GetChild(1).gameObject.SetActive(false);
        handMenu.transform.GetChild(2).gameObject.SetActive(false);
        isMenuActive = false;

    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch)) // Y button
        {
            if (isMenuActive)
            {
                handMenu.transform.GetChild(0).gameObject.SetActive(false);
                handMenu.transform.GetChild(1).gameObject.SetActive(false);
                handMenu.transform.GetChild(2).gameObject.SetActive(false);
                isMenuActive = false;
            }
            else
            {
                handMenu.transform.GetChild(0).gameObject.SetActive(true);
                handMenu.transform.GetChild(1).gameObject.SetActive(true);
                handMenu.transform.GetChild(2).gameObject.SetActive(true);
                isMenuActive = true;
            }
        }
    }
}