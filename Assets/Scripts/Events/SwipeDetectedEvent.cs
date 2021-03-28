using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SwipeDetectedEvent", menuName = "Events/SwipeDetectedEvent")]
public class SwipeDetectedEvent : Observable<SwipeDetectedEvent>
{
	public SwipeData swipeData;
    public override void SetData(params object[] data)
    {
	    swipeData = (SwipeData)data[0];
    }
}
