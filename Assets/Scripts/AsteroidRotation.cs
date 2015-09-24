using UnityEngine;
using System.Collections;

public class AsteroidRotation : MonoBehaviour
{
	public float spin = 2.0f;
	
	void Update ()
	{
		float xSpin = Random.Range (0.0f, 10.0f);
		float ySpin = Random.Range (0.0f, 10.0f);
		float zSpin = Random.Range (0.0f, 10.0f);
		
		int direction = Random.Range (-1, 0) * 2 + 1;
		
		xSpin *= direction;
		ySpin *= direction;
		zSpin *= direction;
		
		transform.Rotate(xSpin * Time.deltaTime, ySpin * Time.deltaTime, zSpin * Time.deltaTime);
		
		if (transform.parent != null)
		{
			if (direction > 0)
			{
				transform.RotateAround(transform.parent.position, Vector3.up, spin * Time.deltaTime);
				transform.RotateAround(transform.parent.position, Vector3.forward, spin * Time.deltaTime);
			}
			else
			{
				transform.RotateAround(transform.parent.position, Vector3.back, spin * Time.deltaTime);
				transform.RotateAround(transform.parent.position, Vector3.down, spin * Time.deltaTime);
			}
		}
	}
}