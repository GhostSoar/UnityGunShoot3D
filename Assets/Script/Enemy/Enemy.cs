using UnityEngine;
using System.Collections;

public class Enemy:BaseGameEntity {
	StateMachine<Enemy> m_pStateMachine;
	
	public Renderer MuzzleFlash;
	public Transform Turret;
	public Transform FireTrail;
	public float  FireDistance;
	
	public GameObject Player;
	public AnimationClip FireGun;
	public AnimationClip WalkRun;
	
	public float EnemySpeed=1.0f;
	public Vector3 EnemyVelocity;
	
    // Use this for initialization
	void Start () {
		SetID((int)EntityID.m_Enemy);
		
		MuzzleFlash.gameObject.SetActiveRecursively(false);
		
		m_pStateMachine=new StateMachine<Enemy>(this);
		m_pStateMachine.SetCurrentState(Enemy_WanderState.Instance());
		m_pStateMachine.SetGlobalStateState(Enemy_GloballState.Instance());
		EntityManager.Instance().RegisterEntity(this);
	}
	
	// Update is called once per frame
	void Update () {
		m_pStateMachine.SMUpdate();
	}
	
	public void ShowMuzzle()
	{
		StartCoroutine(ShowMuzzleFlash());
	}
	
	// Show muzzle flash for a brief moment
	IEnumerator ShowMuzzleFlash () {
		// Show muzzle flash when firing

		MuzzleFlash.gameObject.SetActiveRecursively(true);
		yield return new WaitForSeconds(0.05f);//wait 5 s
		MuzzleFlash.gameObject.SetActiveRecursively(false);
	}
	
	public StateMachine<Enemy> GetFSM()
	{
		return m_pStateMachine;
	}
	
	public override bool HandleMessage (Telegram telegram)
	{
		return base.HandleMessage (telegram);
	}
}
