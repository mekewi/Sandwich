using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CellSwipedEvent", menuName = "Events/CellSwipedEvent")]
public class CellSwipedEvent : Observable<CellSwipedEvent>
{
    public Cell targetCell;
    public override void SetData(params object[] data)
    {
        targetCell = (Cell)data[0];
    }
}
