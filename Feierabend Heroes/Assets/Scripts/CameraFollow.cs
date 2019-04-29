using UnityEngine;
using Rewired;

public class CameraFollow : MonoBehaviour {

	public int cameraID = 0;

	public Transform target;
	public Transform tpTarget;

	public float smoothSpeed = 3.0f;
	// public float tpSmoothSpeed = 1.0f;
	public float rotateSpeed;
	public float additionalHeight = 3.0f;

	public Vector3 isoOffset;
	public Vector3 thirdPersonOffset;


	private void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}


	private void Start() {
		tpTarget.transform.parent = null;
	}


	private void FixedUpdate() {
		tpTarget.transform.position = new Vector3(
			target.transform.position.x,
			target.transform.position.y + additionalHeight,
			target.transform.position.z
		);

		// Check which camera mode is active
		if (SettingsHolder.perspectiveMode == 0) {
			Vector3 desiredPos = target.position + isoOffset;
			Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
			
			transform.position = smoothedPos;
		} else {
			float horizontal = ReInput.players.GetPlayer(cameraID).GetAxis("RS Horizontal") * rotateSpeed;
			tpTarget.Rotate(0, horizontal, 0);

			float desiredAngle = tpTarget.eulerAngles.y;
			Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
			
			// Lerping for camera movement
			// Vector3 desiredPos = target.position - (rotation * thirdPersonOffset);
			// Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, tpSmoothSpeed * Time.deltaTime);
			// transform.position = smoothedPos;

			transform.position = target.position - (rotation * thirdPersonOffset);

			transform.LookAt(tpTarget);
		}

	}
}
