using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int maxResourcesInHand = 10;
    public int resourcesInHand;
    public BaseTower towerInHand;
    

    public float speed = 10.0f;
    public float turnSpeed = 25.0f;
    public float jumpHeight = 2.0f;
    public Animator anim;
    public float invincibilityTime = 2.0f;
    
    [HideInInspector]
    public bool choiceMode = false;
    
    private Rigidbody _rb;
    PlayerInput _input;
    private bool _isGrounded;
    
    Vector2 _movedir;
    Vector2 _lookdir;
    private bool _canPlant;
    private bool _invincible;
    
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
    }

    public void TakeDamage(float damage)
    {
        if (!_invincible)
        {
            print("Player took " + damage + " damage!");
            anim.SetTrigger("impact");
            _invincible = true;
            Invoke(nameof(MakeVunerable), invincibilityTime);
        }
        
    }

    public bool TakeResource()
    {
        if (resourcesInHand < maxResourcesInHand)
        {
            resourcesInHand++;
            
            //TODO: update UI
            return true;
        }

        return false;
    }

    void MakeVunerable()
    {
        _invincible = false;
    }
    
    private void Update()
    {
        _movedir = _input.actions["move"].ReadValue<Vector2>();
        
        if (choiceMode)
        {
            if (resourcesInHand > 0)
            {
                Flowey.inst.AddResources(resourcesInHand);
                //TODO: play sound
                resourcesInHand = 0;
            }
            
            if (_input.actions["x"].triggered)
            {
                Flowey.inst.BuySlot1();
            }
            if (_input.actions["y"].triggered)
            {
                Flowey.inst.BuySlot2();
            }
            if (_input.actions["b"].triggered)
            {
                Flowey.inst.BuySlot3();
            }
            return;
        }
        
        
        
        //if player is on the ground
        if (_isGrounded)
        {
            //if player presses jump
            if (_input.actions["a"].triggered)
            {
                //add force to player
                _rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            }
        }

        if (_input.actions["b"].triggered && _isGrounded)
        {
            Plant();
        }

        if (anim != null)
        {
            anim.SetBool("grounded", _isGrounded);
            anim.SetFloat("speed", _movedir.magnitude);
        }
        
    }

    void Plant()
    {
        if (_canPlant && towerInHand != null)
        {
            print("Planting");

            if (anim != null)
            {
                anim.SetTrigger("action");
            }
            Instantiate(towerInHand, transform.position + transform.forward, Quaternion.identity);

            towerInHand = null;
        }
        else
        {
            
        }
       
    }
    

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(_movedir.x * speed, _rb.velocity.y, _movedir.y * speed);
        
        //rotate smoothly toward look dir
        if (_movedir != Vector2.zero)
        {
            Vector3 lookDir = new Vector3(_movedir.x, 0, _movedir.y);
            Quaternion targetRotation = Quaternion.LookRotation(lookDir);

            if (targetRotation != _rb.rotation)
            {
                Quaternion newRotation = Quaternion.Lerp(_rb.rotation, targetRotation, turnSpeed * Time.deltaTime);
                _rb.MoveRotation(newRotation);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("Ground"))
            _isGrounded = true;
    }
    
    private void OnCollisionExit(Collision other)
    {
        if(other.transform.CompareTag("Ground"))
            _isGrounded = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Tower"))
            _canPlant = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Tower"))
            _canPlant = false;
    }
}
