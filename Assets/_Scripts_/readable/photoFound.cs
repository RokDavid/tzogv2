using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class photoFound : MonoBehaviour
{
    public bool photoFounded;
    public Image photoUI;
    private void Awake()
    {
        photoFounded = false;
    }
    public void hidepQ3()
    {
        photoUI.enabled = false;
    }
    public void showpQ3()
    {

        if ((!photoUI.enabled))
        {
            photoUI.enabled = true;
            photoFounded = true;
            // add to letters
        }
        else
            photoUI.enabled = false;

    }
}
