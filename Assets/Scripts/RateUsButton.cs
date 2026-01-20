using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsButton : MonoBehaviour
{
    public void OpenGooglePlayPage()
    {
        Application.OpenURL("market://details?id=com.LSStudios.GetIntoIt");
    }
}
