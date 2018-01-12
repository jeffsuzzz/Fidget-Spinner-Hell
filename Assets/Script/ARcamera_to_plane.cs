using UnityEngine;
using System.Collections;

public class ARcamera_to_plane : MonoBehaviour {

    private Renderer mRenderer;
    private WebCamTexture mCamera;

	// Use this for initialization
	void Start () {
        mCamera = new WebCamTexture();

        mRenderer = GetComponent<MeshRenderer>();
        mRenderer.material.mainTexture = mCamera;

        mCamera.Play();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
