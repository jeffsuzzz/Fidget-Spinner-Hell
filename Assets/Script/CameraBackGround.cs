﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBackGround : MonoBehaviour {

	private RawImage image;
	private WebCamTexture cam;
	private AspectRatioFitter arf;

	// Use this for initialization
	void Start () {
		image = GetComponent<RawImage> ();
		arf = GetComponent<AspectRatioFitter> ();
		cam = new WebCamTexture (Screen.width, Screen.height);
		image.texture = cam;
		cam.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (cam.width < 100) {
			return;
		}	

		float cwNeeded = -cam.videoRotationAngle;
		if (cam.videoVerticallyMirrored) {
			cwNeeded += 180f;
		}
		image.rectTransform.localEulerAngles = new Vector3 (0f, 0f, cwNeeded);

		float videoRatio = (float)cam.width / (float)cam.height;
		arf.aspectRatio = videoRatio;
	}
}
