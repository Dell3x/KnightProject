using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Knight : Creature
{
	private bool onGround = true;
	[SerializeField]private float jumpForce = 80f;
	[SerializeField]private Transform groundCheck;
	[SerializeField]private float stairSpeed;
	public bool m_onStair;
	[SerializeField]private Transform attackPoint;
	[SerializeField]private float attackRange;
	[SerializeField] private float HitDelay;

    void Start()
    {
     GameController.Instance.OnUpdateHeroParameters += HandleOnUpdateHeroParameters;
     GameController.Instance.Knight = this;
     
     if (Input.GetButtonDown("Fire1"))
     { 
                   animator.SetTrigger("Attack");
     }
    }

    // Update is called once per frame
    void Update() 
	{
	    onGround = CheckGround();
         animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
		 animator.SetBool("Jump", !onGround);

		Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = Input.GetAxis("Horizontal") * speed;
		GetComponent<Rigidbody2D>().velocity = velocity;

		if (Input.GetButtonDown("Fire1"))
    	{ 
            animator.SetTrigger("Attack");
         	//Attack();
         	Invoke("Attack", HitDelay);
            GameController.Instance.AudioManager.PlaySound("DM-CGS-46");
        }

		 if (transform.localScale.x < 0)
        {
        	       if (Input.GetAxis("Horizontal") > 0) {
            	       transform.localScale = Vector3.one;
        	       }
        }
        else
        {
          if (Input.GetAxis("Horizontal") < 0)  
          {
           transform.localScale = new Vector3(-1, 1, 1);
          }
        }

		if (Input.GetButtonDown("Jump") && onGround)
    	{
         GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
         GameController.Instance.AudioManager.PlaySound("DM-CGS-47");
        }

		if (OnStair) 
		{
        	velocity = GetComponent<Rigidbody2D>().velocity;
     	    velocity.y = Input.GetAxis("Vertical") * stairSpeed;
            GetComponent<Rigidbody2D>().velocity = velocity;
        } 
    }

    private void OnDestroy()
    {
     GameController.Instance.OnUpdateHeroParameters -=
     HandleOnUpdateHeroParameters;
    }

    private void HandleOnUpdateHeroParameters(HeroParameters parameters)
	{
     Health = parameters.MaxHealth;
     Damage = parameters.Damage;
     Speed = parameters.Speed;
	}
	
	public bool OnStair
	{
	   get 
	   {
	    return  m_onStair;
	   }

	   set 
	   {

	   if (value == true) 
	   {
       	 GetComponent<Rigidbody2D>().gravityScale = 0;
       }

	   else 
       {
         	  GetComponent<Rigidbody2D>().gravityScale = 2;
       }
       m_onStair = value;
    }


	
    }

      public void Attack()
      {
        DoHit(attackPoint.position, attackRange, Damage);
      }
	  private bool CheckGround()
      {
       RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, groundCheck.position);

       for (int i = 0; i < hits.Length; i++)
       {
        if (!GameObject.Equals(hits[i].collider.gameObject, gameObject))
        {
         return true;
        } 
       }
       return false;
      }

    // Start is called before the first frame update
    
}

