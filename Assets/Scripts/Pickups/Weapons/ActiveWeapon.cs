using UnityEngine;
using System.Collections;

public class ActiveWeapon : MonoBehaviour 
{

	public int currentWeaponMagazineAmmo;
	public float currentWeaponBulletSpeed;
	public int currentWeaponGunDamage;
	public int currentWeaponProjectileQuantity;
	public float currentWeaponBulletLife;

	public GameObject defaultBulletObject;
	public GameObject activeWeapon;

	public AudioClip activeWeaponFire;

	AudioSource audio;

	//Do I add the sound files and play them in here? Or do I add them do each individual weapon script and play them from here but link them?
	

	Inventory inventory;

	// Use this for initialization
	void Start () 
	{

		inventory = gameObject.GetComponent<Inventory>();
		audio = GetComponent<AudioSource> ();

		ReloadGun();

	}
	
	// Update is called once per frame
	void Update () 
	{
	
		if (Input.GetKeyDown (KeyCode.R)) {
		

			if (inventory.activeGunName == "weapon_glock")
			{
				if(inventory.glockMaxMagAmmo != currentWeaponMagazineAmmo)
				{
					ReloadGun ();
				}
			}

			else if (inventory.activeGunName == "weapon_revolver")
			{
				if(inventory.revolverMaxMagAmmo != currentWeaponMagazineAmmo)
				{
					ReloadGun ();
				}
			}

			else if (inventory.activeGunName == "weapon_shotgun")
			{
				if(inventory.shotgunMaxMagAmmo != currentWeaponMagazineAmmo)
				{
					ReloadGun ();
				}
			}

		}

		if (Input.GetMouseButtonDown (0)) 
		{

			//Fire gun
			if(currentWeaponMagazineAmmo > 0)
			{

				GameObject tempBullet = Instantiate(defaultBulletObject, activeWeapon.transform.position, activeWeapon.transform.rotation)as GameObject;
				tempBullet.GetComponent<Rigidbody>().AddForce (transform.forward * currentWeaponBulletSpeed);

				Bullet bullet = tempBullet.GetComponent<Bullet>();
				bullet.gunDamage = currentWeaponGunDamage;
				bullet.bulletLife = currentWeaponBulletLife;

				audio.PlayOneShot(activeWeaponFire);
				currentWeaponMagazineAmmo -= 1;


				/*if(currentWeaponMagazineAmmo <= 0)
				{
					ReloadGun();
				}*/
				
			}
		}



	}

	void ReloadGun()
	{

		//Timers
		//Animations


		if (inventory.activeGunName == "weapon_glock") 
		{
			currentWeaponMagazineAmmo = inventory.glockAmmo;
			if(currentWeaponMagazineAmmo > inventory.glockMaxMagAmmo)
			{
				currentWeaponMagazineAmmo = inventory.glockMaxMagAmmo;

			}

			inventory.glockAmmo -= currentWeaponMagazineAmmo; 
		}

		else if (inventory.activeGunName == "weapon_revolver") 
		{
			currentWeaponMagazineAmmo = inventory.revolverAmmo;
			if(currentWeaponMagazineAmmo > inventory.revolverMaxMagAmmo)
			{
				currentWeaponMagazineAmmo = inventory.revolverMaxMagAmmo;
			}
			
			inventory.revolverAmmo -= currentWeaponMagazineAmmo; 
		}

		else if (inventory.activeGunName == "weapon_shotgun") 
		{
			currentWeaponMagazineAmmo = inventory.shotgunAmmo;
			if(currentWeaponMagazineAmmo > inventory.shotgunMaxMagAmmo)
			{
				currentWeaponMagazineAmmo = inventory.shotgunMaxMagAmmo;
			}
			
			inventory.shotgunAmmo -= currentWeaponMagazineAmmo; 
		}

		else if (inventory.activeGunName == "weapon_melee") 
		{
			//Play cool melee animation  
		}

		if (currentWeaponMagazineAmmo <= 0) 
		{
			Debug.Log("Gun Empty Bitches!");
		}
	}

}
