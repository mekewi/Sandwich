using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WinLoseEvent", menuName = "Events/WinLoseEvent")]
public class WinLoseEvent : Observable<WinLoseEvent>
{
    public bool isWin;
    public override void SetData(params object[] data)
    {
        isWin = (bool) data[0];
    }
}
