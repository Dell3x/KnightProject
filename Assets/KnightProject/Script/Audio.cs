using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Audio
{

  #region Private_Variables

  //Ссылка на источник звука для воспроизведения звуков
     private AudioSource sourceSFX;
 
//Ссылка на источник звука для воспроизведения музыки
     private AudioSource sourceMusic;
 
//Ссылка на источник звука для воспроизведения звуков	
//со случайной частотой
     private AudioSource sourceRandomPitchSFX;
 

//громкость музыки
     private float musicVolume = 1f;

//громкость звуков
	private float sfxVolume = 1f;
 
//Массив звуков
     [SerializeField] private AudioClip[] sounds;

//Звук по умолчанию, на случай, если в массиве отсутствует требуемый
    [SerializeField] private AudioClip defaultClip;

//Музыка для главного меню
	[SerializeField] private AudioClip menuMusic;  

//Музыка для игры на уровнях
	[SerializeField] private AudioClip gameMusic;
#endregion

  public AudioSource SourceSFX
  {
   get
   {
     return sourceSFX;
   }

   set
   {
     sourceSFX = value;
   }
  }

  public AudioSource SourceMusic
  {
   get
   {
     return sourceMusic;
   }

   set
   {
     sourceMusic = value;
   }
  }

  public AudioSource SourceRandomPitchSFX
  {
   get
   {
     return sourceRandomPitchSFX;
   }

   set
   {
     sourceRandomPitchSFX = value;
   }
  }

  public float MusicVolume
  {
   get
   {
    return musicVolume;
   }
   set
   {
    musicVolume = value;
    SourceMusic.volume = musicVolume;
   }
  }
  public float SfxVolume
  {
   get
   {
    return sfxVolume;
   }
   set
   {
    sfxVolume = value;
    SourceSFX.volume = sfxVolume;
    SourceRandomPitchSFX.volume = sfxVolume;
   }
  }


  private AudioClip GetSound(string clipName)
  {
   for (int i = 0; i < sounds.Length; i++)
   {
    if (sounds[i].name == clipName)
    {
     return sounds[i];
    }
   }
   Debug.LogError("Can not find clip " + clipName);
   return defaultClip;
  }

  public void PlaySound(string clipName)
  {
   SourceSFX.PlayOneShot(GetSound(clipName), SfxVolume);
  }

  public void PlaySoundRandomPitch(string clipName)
  {
   SourceRandomPitchSFX.pitch = Random.Range(0.7f, 1.3f);
   SourceRandomPitchSFX.PlayOneShot(GetSound(clipName), SfxVolume);
  }

  public void PlayMusic(bool menu)
  {
   if (menu)
   {
    SourceMusic.clip = menuMusic;
   }
   else
   {
    SourceMusic.clip = gameMusic;
   }
   SourceMusic.volume = MusicVolume;
   SourceMusic.loop = true;
   SourceMusic.Play();
  }
}
