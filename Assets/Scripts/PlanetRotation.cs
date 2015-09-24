using UnityEngine;
using System.Collections;

public class PlanetRotation : MonoBehaviour
{
	public float spin = 2.0f;
	
	void Update ()
	{
		float ySpin = Random.Range (0.0f, 10.0f);
		
		int direction = Random.Range (-1, 0) * 2 + 1;
		
		ySpin *= direction;
		
		transform.Rotate(0f, ySpin * Time.deltaTime, 0f);
		
		if (transform.parent != null)
		{
			if (direction > 0)
			{
				transform.RotateAround(transform.parent.position, Vector3.up, spin * Time.deltaTime);
			}
			else
			{
				transform.RotateAround(transform.parent.position, Vector3.down, spin * Time.deltaTime);
			}
		}
	}
}