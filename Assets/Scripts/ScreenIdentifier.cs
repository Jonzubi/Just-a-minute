using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenIdentifier : MonoBehaviour
{
    public EScreenIdentifier screenIndex;
    public void SetActive(EScreenIdentifier index)
    {
        gameObject.SetActive(index == screenIndex);
    }
}
