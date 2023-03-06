using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Define public and private variables for the Player class
    public bool isPaused; // Public boolean variable to check if the game is paused
    [SerializeField] public float speed; // Serialized private float variable for the player's base speed
    [SerializeField] public float runSpeed; // Serialized private float variable for the player's running speed
    [SerializeField] public float rollSpeed; // Serialized private float variable for the player's rolling speed
    private Rigidbody2D rig; // Private Rigidbody2D component variable for the player's Rigidbody2D component
    private Player_Itens playerItens; // Private Player_Itens component variable for the player's Player_Itens component
    private float initialSpeed; // Private float variable for the player's initial speed
    private bool _isRunning; // Private boolean variable to check if the player is running
    private bool _isRolling; // Private boolean variable to check if the player is rolling
    private bool _isCutting; // Private boolean variable to check if the player is cutting
    private bool _isAttaking; // Private boolean variable to check if the player is attacking
    private bool _isDigging; // Private boolean variable to check if the player is digging
    private bool _isWatering; // Private boolean variable to check if the player is watering
    private Vector2 _direction; // Private Vector2 variable to store the player's movement direction
    public int handlingObj; // Public integer variable to store the player's handling object index

    // Define public properties for the private variables
    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }
    }
    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }
    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }
    public bool isCutting
    {
        get { return _isCutting; }
        set { _isCutting = value; }
    }
    public bool isAttaking
    {
        get { return _isAttaking; }
        set { _isAttaking = value; }
    }
    public bool isWatering
    {
        get { return _isWatering; }
        set { _isWatering = value; }
    }
    public bool isDigging
    {
        get { return _isDigging; }
        set { _isDigging = value; }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Get the player's Rigidbody2D component and Player_Itens component
        rig = GetComponent<Rigidbody2D>();
        playerItens = GetComponent<Player_Itens>();

        // Set the initial speed variable to the player's base speed
        initialSpeed = speed;
    }

	private void Update()
	{
		// Check if the game is not paused
		if (!isPaused)
		{
			// Check if the "1" key is pressed
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
            // Set the handling object to 0
				handlingObj = 0;
			}
	
			// Check if the "2" key is pressed
			if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				// Set the handling object to 1
				handlingObj = 1;
			}
	
			// Check if the "3" key is pressed
			if(Input.GetKeyDown(KeyCode.Alpha3))
			{
				// Set the handling object to 2
				handlingObj = 2;
			}

			// Check if the "4" key is pressed
			if(Input.GetKeyDown(KeyCode.Alpha4))
			{
				// Set the handling object to 3
				handlingObj = 3;
			}

			// Call the various methods on Unity
			OnInput();
			OnRun();
			OnRolling();
			OnCutting();
			OnAttaking();
			OnDig();
			OnWatering();
		}
	
		/* if(Input.GetKeyDown(KeyCode.Space))
		{
			SceneManager.LoadScene("teste");
		} */
	}

	// This method is called at fixed intervals, useful for physics calculations
	private void FixedUpdate()
	{
		// check if the game is not paused
		if (!isPaused)
		{
			// call the OnMove method
			OnMove();
		}
	}

	#region Movement

	void OnWatering()
	{
		// check if the current handling object is a watering can
		if(handlingObj == 2)
		{
			// check if the left mouse button is pressed and the player has water
			if(Input.GetMouseButtonDown(0) && playerItens.currentWater > 0)
			{
				// set the isWatering flag to true and stop the player's movement
				isWatering = true;
				speed = 0f;
			}
			// check if the left mouse button is released or the player has run out of water
			if(Input.GetMouseButtonUp(0) || playerItens.currentWater < 0)
			{
				// set the isWatering flag to false and restore the player's movement speed
				isWatering = false;
				speed = initialSpeed;
			}
			// check if the player has run out of water and set the current water level to 0
			if(playerItens.currentWater < 0)
			{
				playerItens.currentWater = 0;
			}
			// if the player is watering, decrement the current water level
			if(isWatering)
			{
				playerItens.currentWater -= 0.01f;
			}
		}
		// if the current handling object is not a watering can, set the isWatering flag to false
		else
		{
			isWatering = false;
		}
	}

	void OnDig()
	{	
		// Check if the player is handling the right object for digging
		if(handlingObj == 1)
		{
			// Check if the player is pressing the left mouse button
			if(Input.GetMouseButtonDown(0))
			{
				// Set digging to true and stop the player's movement
				_isDigging = true;
				speed = 0f;
			}
			
			// Check if the player has released the left mouse button
			if(Input.GetMouseButtonUp(0))
			{
				// Set digging to false and resume the player's movement
				_isDigging = false;
				speed = initialSpeed;
			}
		}
		else
		{
			// If the player is not handling the right object, set digging to false
			isDigging = false;
		}
	}
	
	void OnCutting()
	{
		// Check if the player is handling the right object for cutting
		if(handlingObj == 0)
		{
			// Check if the player is pressing the left mouse button
			if(Input.GetMouseButtonDown(0))
			{
				// Set cutting to true and stop the player's movement
				_isCutting = true;
				speed = 0f;
			}
			
			// Check if the player has released the left mouse button
			if(Input.GetMouseButtonUp(0))
			{
				// Set cutting to false and resume the player's movement
				_isCutting = false;
				speed = initialSpeed;
			}
		}
		else
		{
			// If the player is not handling the right object, set cutting to false
			isCutting = false;
		}
	}
	
	void OnAttaking()
	{
		// Check if the object being handled is set to 3 (attacking)
		if (handlingObj == 3)
		{
			// If left mouse button is pressed, set attacking flag and stop movement
			if (Input.GetMouseButtonDown(0))
			{
				_isAttaking = true;
				speed = 0f;
			}
			// If left mouse button is released, unset attacking flag and restore movement speed
			if (Input.GetMouseButtonUp(0))
			{
				_isAttaking = false;
				speed = initialSpeed;
			}
		}
		else
		{
			// If object being handled is not set to attacking, unset attacking flag
			isAttaking = false;
		}
	}
	
	void OnInput()
	{
		// Set direction vector based on horizontal and vertical input axes
		_direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	}
	
	void OnMove()
	{
		// Move the rigid body in the direction determined by the input and current speed
		rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime);
	}
	
	void OnRun()
	{
		// If left shift is pressed, set speed to run speed and set running flag
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			speed = runSpeed;
			_isRunning = true;
		}
	
		// If left shift is released, restore speed to initial speed and unset running flag
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			speed = initialSpeed;
			_isRunning = false;
		}
	}
	
	void OnRolling()
	{
		// If the right mouse button is pressed down, set the speed to the rolling speed and activate the rolling animation
		if (Input.GetMouseButtonDown(1))
		{
			speed = rollSpeed;
			_isRolling = true;
		}
	
		// If the right mouse button is released, set the speed back to the initial speed and deactivate the rolling animation
		if (Input.GetMouseButtonUp(1))
		{
			speed = initialSpeed;
			_isRolling = false;
		}
	}
	
	#endregion
}