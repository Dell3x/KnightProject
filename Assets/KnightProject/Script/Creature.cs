using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour, IDestructable
{
   protected Animator animator; 
   protected Rigidbody2D rigidbody;
   [SerializeField] protected float speed;
   [SerializeField] protected float damage;
   [SerializeField] protected float health = 100;

   public float Health
   {
     	get
    	{
         return health;
        }
      	set
    	{
         health = value;
    	}
   }

   public float Speed
   {
     	get
    	{
         return speed;
        }
      	set
    	{
         speed = value;
    	}
   }

   public float Damage
   {
     	get
    	{
         return damage;
        }
      	set
    	{
         damage = value;
    	}
   }
   void Awake()
   {
    	animator = gameObject.GetComponentInChildren<Animator>();
     	rigidbody = gameObject.GetComponent<Rigidbody2D>();
   }
 
	
   public virtual void Die() 
   {
    GameController.Instance.Killed(this);
         	Destroy(gameObject);
   }
 
   public void RecieveHit(float damage)
   {
     Health -= damage;
     GameController.Instance.Hit(this);
     if (Health <= 0)
     {
      Die();
     }
   }

   protected void DoHit(Vector3 hitPosition, float hitRadius, float hitDamage)
   {
    	Collider2D[] hits = Physics2D.OverlapCircleAll(hitPosition, hitRadius);

     	for (int i = 0; i < hits.Length; i++)
    	{
         if (!GameObject.Equals(hits[i].gameObject, gameObject))
         {
           IDestructable destructable =
           hits[i].gameObject.GetComponent<IDestructable>();

           if (destructable != null)
           {
            destructable.RecieveHit(hitDamage);
           }
         }
    	}
}


}


