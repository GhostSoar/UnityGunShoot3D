using UnityEngine;
using System.Collections;

public class EnemySoundEffect : MonoBehaviour {
	public AudioSource gunSource;
	public AudioClip fire;
	
	void OnFire()
	{
		gunSource.PlayOneShot(fire);
	}
}
