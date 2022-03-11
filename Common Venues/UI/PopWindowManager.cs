using Common_Venues.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopWindowManager : MonoBehaviour
{
    [SerializeField] StatusUI[] popwindows;
    StatusUI currentActivePop;
    // Start is called before the first frame update
    void Start()
    {
        popwindows = GetComponentsInChildren<StatusUI>(true);
    }
    internal void PopWindowStatus(StatusUI pop)
    {
        currentActivePop = pop;
        foreach (var item in popwindows)
        {
            if (item != currentActivePop)
                item.ClearWindow();
        }
    }
}
