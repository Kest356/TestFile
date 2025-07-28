using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuforiaDeviceOrientation : MonoBehaviour {

	[Space(15)]
	public bool FrontCamera;
	[Space(15)]
	private bool FlipX, FlipX_check;
	private bool FlipY, FlipY_check;

	void Update ()
	{
		#if !UNITY_EDITOR && !UNITY_STANDALONE

		FlipX_check = FlipX;
		FlipY_check = FlipY;

		if (FrontCamera)		// Front-Facing Camera
		{
			if (Screen.orientation == ScreenOrientation.LandscapeRight) {
				FlipX = false;
				FlipY = false;
			}

			if (Screen.orientation == ScreenOrientation.LandscapeLeft) {
				FlipX = true;
				FlipY = true;
			}

			if (Screen.orientation == ScreenOrientation.Portrait) {
				FlipX = true;
				FlipY = false;
			}

			if (Screen.orientation == ScreenOrientation.PortraitUpsideDown) {
				FlipX = false;
				FlipY = true;
			}
			
			if (Screen.orientation == ScreenOrientation.Portrait && Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown) {
				FlipX = true;
				FlipY = false;
			}
		}

		else				// Back-Facing Camera (Rear-Facing)
		{
			if (Screen.orientation == ScreenOrientation.LandscapeRight) {
				FlipX = true;
				FlipY = false;
			}

			if (Screen.orientation == ScreenOrientation.LandscapeLeft) {
				FlipX = false;
				FlipY = true;
			}

			if (Screen.orientation == ScreenOrientation.Portrait) {
				FlipX = true;
				FlipY = true;
			}

			if (Screen.orientation == ScreenOrientation.PortraitUpsideDown) {
				FlipX = false;
				FlipY = false;
			}

			if (Screen.orientation == ScreenOrientation.Portrait && Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown) {
				FlipX = true;
				FlipY = true;
			}
		}


		if (FlipX_check != FlipX || FlipY_check != FlipY)
		{
			GetComponent<RegionCapture> ().FlipX = FlipX;
			GetComponent<RegionCapture> ().FlipY = FlipY;
		}
		#endif
	}
}
