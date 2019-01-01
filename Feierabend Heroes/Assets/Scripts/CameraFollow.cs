using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothSpeed = 3.0f;

	public Vector3 offset;


	private void FixedUpdate() {
		Vector3 desiredPos = target.position + offset;
		Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
		
		transform.position = smoothedPos;
	}
}
