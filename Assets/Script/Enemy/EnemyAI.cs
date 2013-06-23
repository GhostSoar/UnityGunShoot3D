using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	public GameObject player;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(player.transform.position,transform.position)<3)
		{
			transform.animation.Play("FireGun");
		}
		else
		{
			transform.animation.Play("WalkRun");
		}
	}
}
