using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePopupBase : MonoBehaviour
{
    public bool Isunique = true;

    public void Close()
    {
        GamePopupManager.Close();
    }
}
