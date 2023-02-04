using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public GameObject seed;
    
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
    public float wannaJumpMemory;
    public float maxWannaJumpMemory = 0.2f;
    public AudioClip jumpSound;
    
    Vector2 _movedir;
    Vector2 _lookdir;
    
    [HideInInspector]
    public bool _canPlant;
    private bool _invincible;
    private AudioSource soundEffects;
    public Vector3 velocity;
    public float gravity = 25;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<PlayerInput>();
        soundEffects = GetComponent<AudioSource>();
    }
    
    public void PlayEffect(AudioClip clip)
    {
        soundEffects.PlayOneShot(clip, Random.Range(0.9f, 1.2f));
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

        velocity.x = _movedir.x * speed;
        velocity.z = _movedir.y * speed;

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
            
        }
        else
        {
            if (_input.actions["a"].triggered)
            {
                //add force to player
                wannaJumpMemory = maxWannaJumpMemory;
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
        
        
        wannaJumpMemory -= Time.deltaTime;
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
            seed.SetActive(false);
        }
        else
        {
            
        }
       
    }
    

    private void FixedUpdate()
    {
       // _rb.velocity = new Vector3(_movedir.x * speed, _rb.velocity.y, _movedir.y * speed);
        
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
        
        // gravity


        // shoot ray down from center to extend of player to check if grounded
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.1f))
        {
            if (hit.transform.CompareTag("Ground"))
            {
                if (wannaJumpMemory > 0)
                {
                    Jump();
                }
            }
        } else
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        _rb.velocity = velocity;
    }

    public void Jump()
    {
        var hSpeed = HeightToVelocity( jumpHeight);
        velocity.y = hSpeed;
        Audio.Play(jumpSound);
        wannaJumpMemory = 0;
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
    
    float HeightToVelocity(float height)
    {
        return Mathf.Sqrt(2 * height * gravity);
    }
}
