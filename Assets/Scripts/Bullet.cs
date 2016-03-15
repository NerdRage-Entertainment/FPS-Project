using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public int bulletDamage;
	public int gunDamage;
	public float bulletLife;



	void Start()
	{

	}

	void Update()
	{
		//Reduce Timer
		bulletLife = bulletLife - Time.deltaTime;
		if(bulletLife <= 0.0f)
		{
			Destroy(this.gameObject);
		}

	}

	//Bullet detecting other object it hits
	void OnCollisionEnter(Collision other)
	{
		Debug.Log ("Bullet hit lel");
		if(other.gameObject.layer == 10)
		{
			Debug.Log ("I KILLZ YOU MUTHUFUCKA!!");
		}
	}


}
