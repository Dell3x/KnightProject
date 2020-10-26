﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HeroParameters
{
    #region Private_Variables
 
	[SerializeField] private float maxHealth = 100;
 
	[SerializeField] private float damage = 20;
 
	[SerializeField] private float speed = 5;
 
	[SerializeField] private int experience = 0;
	private int nextExperienceLevel = 100;
	private int previousExperienceLevel = 0;
	private int level = 1;
 
    #endregion
	public int Experience
	{
get
{
return experience;
    		}
set
{
experience = value;

CheckExperienceLevel();
    		}
	}


	public float MaxHealth
	{
	 get
	 {
		  return maxHealth;
	 }

	 set
	 {
	  maxHealth = value;
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

	private void CheckExperienceLevel()
	{
     if (experience > nextExperienceLevel)
     {
      level++;
 			//рассчет следующего уровня опыта
      int addition = previousExperienceLevel;
      previousExperienceLevel = nextExperienceLevel;
      nextExperienceLevel += addition;
      //улучшение одного из параметров на единицу.  
      switch (Random.Range(0, 3))  //случайное число от 0 до 2 включительно
      {
       case 0: maxHealth++;	break;

       case 1: damage++;		break;

       case 2: speed++;		break;
      }
        //Вызов метода в контроллере для обновления
       //HUD и параметров рыцаря:
 	   GameController.Instance.LevelUp();
     }
    }

}
