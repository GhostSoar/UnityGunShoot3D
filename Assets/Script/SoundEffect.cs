using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour {
	
	public AudioSource gunSource;
	public AudioClip fire;
	
	void OnFire()
	{
		gunSource.PlayOneShot(fire);
	}
}
