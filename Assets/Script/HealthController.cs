using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {
	
	public GameObject hitParticles;
	public float health;
	public float hitDamage = 3;
	public float maxHealth = 100;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnHit (RayAndHit rayAndHit) {
		health -= hitDamage;
		health = Mathf.Clamp(health, 0, maxHealth);
		
		if (hitParticles) {
			GameObject particles = Instantiate(
				hitParticles,
				rayAndHit.hit.point,
				Quaternion.LookRotation(-rayAndHit.ray.direction)
			) as GameObject;
			particles.transform.parent = transform;
		}
	}
}
