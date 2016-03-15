using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class Player : Character
{

    CharacterController characterController;

    public float actualMovementSpeed = 5.0f;
    public float strafeSpeed = 4.0f;

	//Mouse Look
    public float mouseSensitivity = 5.0f;
    float verticalRotation = 0.0f;
    public float upDownRange = 60.0f;

	//Jumping
    float jumpSpeed = 5.0f;
    float verticalVelocity = 0.0f;


	//Healthbar Variables
	public RectTransform healthTransform;
	private float cachedY;
	private float minXValue;
	private float maxXValue;
	private int currentHealth;

	private int CurrentHealth
	{
		get { return currentHealth; }
		set 
		{ 
			currentHealth = value; 
			HandleHealthBar();
		}
	}

	public int maxHealth;
	public Text healthText;
	public Image visualHealth;
	public float coolDown;
	private bool onCoolDown;

	Character character;



    void Start()
    {
		//WARNING: CURSED CODEBLOCK. Causes Character Controller to stop moving and shooting and what not
		//Healthbar, perhaps look at exporting to another script and getting player health from there
		currentHealth = maxHealth;
		cachedY = healthTransform.position.y;
		maxXValue = healthTransform.position.x;
		minXValue = healthTransform.position.x - healthTransform.rect.width;
		//END OF CURSED CODEBLOCK


        characterController = GetComponent<CharacterController>();
		//Check for errors
        if(characterController == null)
        {
            Debug.Log("YOU FAG FUCKING CUNT SUCKING WHORE BITCH TIT ILL RAPE AND KILL YOU");
        }

    }

    void Update()
    {
		//ROTATION
		float rotX = Input.GetAxis("Mouse X") * mouseSensitivity;
		transform.Rotate(0, rotX, 0);
		
		
		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
		
		
		
		//MOVEMENT
		float forwardSpeed = Input.GetAxis("Vertical") * actualMovementSpeed;
		float strafeSpeed = Input.GetAxis("Horizontal") * actualMovementSpeed;
		
		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		
		if(characterController.isGrounded && Input.GetButtonDown("Jump"))
		{
			verticalVelocity = jumpSpeed;
		}
		
		Vector3 speed = new Vector3(strafeSpeed, verticalVelocity, forwardSpeed);
		
		speed = transform.rotation * speed;
		
		characterController.Move(speed * Time.deltaTime);
		
    }


	private void HandleHealthBar()
	{
		healthText.text = "Health: " + currentHealth;

		float currentXValue = MapValues(currentHealth, 0, maxHealth, minXValue, maxXValue);

		//translate position of health bar
		healthTransform.position = new Vector3 (currentXValue, cachedY);

		if (currentHealth > maxHealth / 2) 
		{ //I have more than 50% health
			visualHealth.color = new Color32((byte)MapValues(currentHealth,maxHealth/2,maxHealth,255,0),255,0,255);
		} 

		else //I have less than 50% health
		{
			visualHealth.color = new Color32(255,(byte)MapValues(currentHealth,0,maxHealth/2,0,255),0,255);
		}
	}

	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
	{
		//algorithm to get coordinates of health status and position healthbar accordingly
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	IEnumerator CoolDownDamage()
	{
		onCoolDown = true;
		yield return new WaitForSeconds (coolDown);
		onCoolDown = false;
	}



	//Quick trap steam test for damage to check health bar
	void OnTriggerStay(Collider other)
	{
		if (other.name == "trap_steam") {
			if(!onCoolDown && currentHealth > 0)

			{
				StartCoroutine(CoolDownDamage());
				CurrentHealth -= 1;
			}
		}
	}


	
};


