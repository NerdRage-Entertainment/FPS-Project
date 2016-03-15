using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour 
{
	//Glock18
	public int glockAmmo;
	public int glockMaxMagAmmo = 12;
	private float glockBulletSpeed = 500.0f;
	private int glockWeaponDamage = 10;
	private float glockBulletLife = 3.0f;
	public AudioClip glockWeaponFireSound;
	public AudioClip glockReloadSound;
	public AudioClip glockWeaponEmptySound; 
	public AudioClip glockShellDropSound;

	//Revolver
	public int revolverAmmo;
	public int revolverMaxMagAmmo = 6;
	private float revolverBulletSpeed = 500.0f;
	private int revolverWeaponDamage = 10;
	private float revolverBulletLife = 3.0f;
	public AudioClip revolverWeaponFireSound;
	public AudioClip revolverReloadSound;
	public AudioClip revolverWeaponEmptySound; 
	public AudioClip revolverShellDropSound;
	
	//Shotgun
	public int shotgunAmmo;
	public int shotgunMaxMagAmmo = 7;
	private float shotgunBulletSpeed = 500.0f;
	private int shotgunWeaponDamage = 10;
	private float shotgunBulletLife = 3.0f;
	public AudioClip shotgunWeaponFireSound;
	public AudioClip shotgunReloadSound;
	public AudioClip shotgunWeaponEmptySound; 
	public AudioClip shotgunShellDropSound;
	

	//Everybody starts with knife	
	public bool hasKnife = true;

	//Default empty active gun name
	public string activeGunName = "";

	//If this script is applied to AI, DON'T decrease ammo
	public bool isAI;


	void Start()
	{
		//setting defaults
		glockAmmo = 36;
		revolverAmmo = 0;
		shotgunAmmo = 0;
		activeGunName = "weapon_glock";

		//setting defaults in OTHER scripts
		ActiveWeapon activeWeapon = gameObject.GetComponent<ActiveWeapon>();
		activeWeapon.currentWeaponBulletSpeed = glockBulletSpeed;
		activeWeapon.currentWeaponGunDamage = glockWeaponDamage;
		activeWeapon.currentWeaponBulletLife = glockBulletLife;




	}


	void Update () 
	{


	}

	//Pickups
	void OnTriggerEnter(Collider other)
	{
		GameObject thePlayer = gameObject;
		Character character = thePlayer.GetComponent<Character>();

		if(other.gameObject.layer == 11)
		{
			Pickup pickup = other.gameObject.GetComponent<Pickup>();
		
			if(pickup.itemName == "health")
			{
				//URGENT!! Not affecting the health represented in the Health Bar. Only being affected by the steam trap function in the Player class
				Debug.Log ("Picked up Health");
				character.baseHealth += pickup.itemQuantity;
			}



			if(pickup.itemName == "glockAmmo")
			{
				Debug.Log ("Picked up Glock Ammo");

				glockAmmo += pickup.itemQuantity;
			}

			if(pickup.itemName == "revolverAmmo")
			{
				Debug.Log ("Picked up Revolver Ammo");

				revolverAmmo += pickup.itemQuantity;
			}

			if(pickup.itemName == "shotgunAmmo")
			{
				Debug.Log ("Picked up Shotgun Ammo");

				shotgunAmmo += pickup.itemQuantity;
			}

			if(pickup.itemName == "meleeAmmo")
			{
				Debug.Log ("Picked up Knife");

				hasKnife = true;
			}



			Destroy(other.gameObject);
		}
	}
}
