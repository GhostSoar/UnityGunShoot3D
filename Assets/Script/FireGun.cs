using UnityEngine;
using System.Collections;

public class RayAndHit {
	public Ray ray;
	public RaycastHit hit;
	public RayAndHit(Ray ray, RaycastHit hit) {
		this.ray = ray;
		this.hit = hit;
	}
}

public class FireGun : MonoBehaviour {
	
	public Renderer muzzleFlash;
	public Transform turret;
	public Transform fireTrail;
	public float  FireDistance;
	
	private LayerMask mask;
    // Use this for initialization
	void Start () {
		// Add orb's own layer to mask
		mask = 1 << gameObject.layer;
		// Add Igbore Raycast layer to mask
		mask |= 1 << LayerMask.NameToLayer("Ignore Raycast");
		// Invert mask
		mask = ~mask;
		
		muzzleFlash.gameObject.SetActiveRecursively(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			Fire();
		}
	}
	
	void Fire()
	{
		StartCoroutine(ShowMuzzleFlash());
		shoot();

	}
	
	void shoot()
	{
		//Ray ray = new Ray(turret.position,new Vector3(100,100,100));
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2,0));
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, FireDistance, mask)) {
			hit.transform.root.SendMessage("OnHit", new RayAndHit(ray, hit), SendMessageOptions.DontRequireReceiver);
			muzzleFlash.transform.localRotation *= Quaternion.Euler(0, 0, Random.Range(-360, 360));
			//fireTrail.localScale = new Vector3(0.2f, 0.2f, hit.distance*2.4f);
			SendMessage("OnFire");
		}
	}
	
	
	// Show muzzle flash for a brief moment
	IEnumerator ShowMuzzleFlash () {
		// Show muzzle flash when firing

		muzzleFlash.gameObject.SetActiveRecursively(true);
		yield return new WaitForSeconds(0.05f);//wait 5 s
		muzzleFlash.gameObject.SetActiveRecursively(false);
	}
	
	void OnHit (RayAndHit rayAndHit) {
		// Add a big force impact from the bullet hit
		rigidbody.AddForce(rayAndHit.ray.direction * 200, ForceMode.Impulse);
		
		// Sometimes, also AddForceAtPosition - this adds some rotation as well.
		// We don't want to allow this too often, otherwise the orb, when being hit constantly,
		// will aim so bad that it's too easy to win.
		if (Random.value < 0.2f)
			rigidbody.AddForceAtPosition(rayAndHit.ray.direction * 15, rayAndHit.hit.point, ForceMode.Impulse);
	}
}
