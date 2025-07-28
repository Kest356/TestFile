using UnityEngine;
using System.Collections;

public class RC_Get_Texture : MonoBehaviour {

	public Camera RenderCamera;
	[Space(20)]
	public bool FreezeEnable = false;


	void Start () {
	StartCoroutine(WaitForTexture());
	}


	private IEnumerator WaitForTexture() 
	{
		yield return new WaitForEndOfFrame ();

		if (RenderCamera && RenderCamera.targetTexture)
		{
			GetComponent<Renderer> ().material.SetTexture ("_MainTex", RenderCamera.targetTexture);
		}
		else StartCoroutine(WaitForTexture());
	}
		

	void LateUpdate () 
	{
		if (FreezeEnable) RenderCamera.enabled = false;
		else RenderCamera.enabled = true;
	}
}