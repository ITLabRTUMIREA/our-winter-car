using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer)), ExecuteInEditMode]
public class Hint : MonoBehaviour
{
	[Header("Set by yourself")]
	public string HintText = "No hints applied";
	public Text TextField;
	[Header("Don't touch if you're not a developer")]
	public Material material;
	public Transform target;
	private LineRenderer _lineRenderer;
	
	private Vector3 _offset;
	
	void Start ()
	{
		_offset = target.position - transform.position;
		
		_lineRenderer = GetComponent<LineRenderer>();
		_lineRenderer.positionCount = 2;
		_lineRenderer.SetWidth(0.01f, 0.01f);
		_lineRenderer.material = material;
		////////////////////////////
		TextField.text = HintText;
	}
	
	void FixedUpdate () 
	{
		if (target == null)
		{
			return;
		}
		
		_lineRenderer.SetPosition(0, transform.position);
		_lineRenderer.SetPosition(1, target.position);

		transform.position = target.position + 1.0f * Vector3.up;
	}
}
