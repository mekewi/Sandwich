using DG.Tweening;
using UnityEngine;

public class MatchWidth : MonoBehaviour {
	[ContextMenu("TestCamera")]
	public static void PositionCamera()
	{
		Bounds bounds = newBounds;
		float cameraDistance = 2.0f; // Constant factor
		Vector3 objectSizes = bounds.max - bounds.min;
		float objectSize = Mathf.Max(objectSizes.x, objectSizes.y, objectSizes.z);
		float cameraView = 2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * Camera.main.fieldOfView); // Visible height 1 meter in front
		float distance = cameraDistance * objectSize / cameraView; // Combined wanted distance from the object
		distance += 0.5f * objectSize; // Estimated offset from the center to the outside of the object
		Camera.main.transform.DOMove(bounds.center - distance * Camera.main.transform.forward,1);
	}

	public GameObject ObjectToView;
	public static Bounds newBounds;
}
