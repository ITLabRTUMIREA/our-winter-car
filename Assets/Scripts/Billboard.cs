using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	public Camera camera;

	private void FixedUpdate()
	{
		transform.LookAt(camera.transform);
		
	}
}
