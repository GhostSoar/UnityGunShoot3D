using UnityEngine;
using System.Collections;

public class Enemy_GloballState:State<Enemy>{
	
	private static Enemy_GloballState instance;
	public static Enemy_GloballState Instance()
	{
		if(instance==null)
			instance=new Enemy_GloballState();
		return instance;
	}
	
	public override void Enter (Enemy entity)
	{
		//base.Enter (entityType);
	}
	
	public override void Execute (Enemy entity)
	{
		//base.Execute (entityType);
	}
	
	public override void Exit (Enemy entity)
	{
		//base.Exit (entityType);
	}
	
	public override bool OnMessage (Enemy entity, Telegram telegram)
	{
		//return base.OnMessage (entityType, telegram);
		return false;
	}
}

public class Enemy_RunningState:State<Enemy>{
	private static Enemy_RunningState instance;
	public static Enemy_RunningState Instance()
	{
		if(instance==null)
			return new Enemy_RunningState();
		return instance;
	}
	
	public override void Enter (Enemy entity)
	{
		//base.Enter (entityType);
		Debug.Log("Enemy_RunningState");
	}
	
	public override void Execute (Enemy entity)
	{
		//base.Execute (entityType);
		entity.animation.CrossFade(entity.WalkRun.name,0.1f);
		
		if(Vector3.Distance(entity.Player.transform.position,entity.transform.position)<3)
		{
			entity.GetFSM().ChangeState(Enemy_FireState.Instance());
		}
	}
	
	public override void Exit (Enemy entity)
	{
		//base.Exit (entityType);
		Debug.Log("Enemy_RunningState Exit!!!");
	}
	
	public override bool OnMessage (Enemy entityType, Telegram telegram)
	{
		//return base.OnMessage (entityType, telegram);
		return false;
	}
}

public class Enemy_WanderState:State<Enemy>{
	private static Enemy_WanderState instance;
	public static Enemy_WanderState Instance()
	{
		if(instance==null)
			return new Enemy_WanderState();
		return instance;
	}
	
	/* Wander steering behavior
	private float m_dWanderRadius=3.0f;
	private float m_dWanderDistance=2.0f;
	private float m_dWanderJitter=1.0f;
	private Vector3 m_vWanderTarget;
	private Vector3 m_vTargetLocal;
	*/
				
	private float m_fTime;
	private const float m_fWanderRadius=20.0f;
	private Vector2 m_vWayPoint;
	private Vector3 m_vStartPos;
	
	public override void Enter (Enemy entity)
	{
		//base.Enter (entityType);
		
		/* Wander steering behavior
		m_vWanderTarget=new Vector3(0,0,0);
		m_vTargetLocal=new Vector3(0,0,0);
		*/
		
		Debug.Log("Enemy_WanderState");
		m_vWayPoint= Random.insideUnitCircle*m_fWanderRadius;
		m_vStartPos=entity.transform.position;
		m_fTime=0;
	}
	
	public override void Execute (Enemy entity)
	{
		//base.Execute (entityType);
		if(Vector3.Distance(entity.Player.transform.position,entity.transform.position)<8)
			entity.GetFSM().ChangeState(Enemy_SeekState.Instance());
		
		entity.animation.CrossFade(entity.WalkRun.name,0.1f);
		
		/* Wander steering behavior
		time+=Time.deltaTime;
		if(time>=1.0)
		{
		time=0;
		m_vWanderTarget+=new Vector3(Random.Range(-1,1)*m_dWanderJitter,0,Random.Range(-1,1)*m_dWanderJitter);
		m_vWanderTarget.Normalize();
		m_vWanderTarget*=m_dWanderRadius;
		
		m_vTargetLocal=m_vWanderTarget+new Vector3(m_dWanderDistance,entity.transform.position.y,0.0f);
		//Vector3 targetWorld=entity.transform.position+targetLocal;
		//targetWorld.y=entity.transform.position.y;
		
		Debug.Log(m_vWanderTarget+","+m_vTargetLocal);
		}
		
		entity.transform.Translate(m_vTargetLocal*Time.deltaTime);
		*/
		
		m_fTime+=Time.deltaTime;
		if(m_fTime>=1.0)
		{
			m_fTime=0;
			m_vWayPoint= Random.insideUnitCircle*m_fWanderRadius;
			entity.transform.LookAt(m_vStartPos+new Vector3(m_vWayPoint.x,0,m_vWayPoint.y));
		}
		entity.transform.Translate(new Vector3(m_vWayPoint.x,entity.transform.position.y,m_vWayPoint.y)*0.1f*Time.deltaTime);
	}
	
	public override void Exit (Enemy entity)
	{
		//base.Exit (entityType);
	}
	
	public override bool OnMessage (Enemy entity, Telegram telegram)
	{
		//return base.OnMessage (entityType, telegram);
		return false;
	}
}

public class Enemy_SeekState:State<Enemy>
{
	private static Enemy_SeekState instance;
	public static Enemy_SeekState Instance()
	{
		if(instance==null)
			return new Enemy_SeekState();
		return instance;
	}
	
	public override void Enter (Enemy entity)
	{
		//base.Enter (entityType);
		Debug.Log("Enemy_SeekState");
	}
	
	public override void Execute (Enemy entity)
	{
	    //base.Execute (entityType);
				
		if(Vector3.Distance(entity.Player.transform.position,entity.transform.position)<5)
			entity.GetFSM().ChangeState(Enemy_FireState.Instance());
		else if(Vector3.Distance(entity.Player.transform.position,entity.transform.position)>8)
			entity.GetFSM().ChangeState(Enemy_WanderState.Instance());
		
		entity.animation.CrossFade(entity.WalkRun.name,0.1f);
		entity.EnemyVelocity=Seek(entity.Player.transform.position,entity.transform.position)*entity.EnemySpeed;
		entity.transform.Translate(entity.EnemyVelocity);
		entity.transform.forward=entity.EnemyVelocity;
	}
	
	public override void Exit (Enemy entity)
	{
		//base.Exit (entityType);
	}
	
	public override bool OnMessage (Enemy entity, Telegram telegram)
	{
		//return base.OnMessage (entityType, telegram);
		return false;
	}
	
	private Vector3 Seek(Vector3 targetPos,Vector3 entityPos)
	{
		Vector3 DesiredVelocity=Vector3.Normalize(targetPos-entityPos)*Time.deltaTime;
		return new Vector3(DesiredVelocity.x,0,DesiredVelocity.z);
	}
}

public class Enemy_FireState:State<Enemy>{
	private static Enemy_FireState instance;
	public static Enemy_FireState Instance()
	{
		if(instance==null)
			return new Enemy_FireState();
		return instance;
	}
	
	private float time=0;
	
	public override void Enter (Enemy entity)
	{
		//base.Enter (entityType);
		Debug.Log("Enemy_FireState");
	}
	
	public override void Execute (Enemy entity)
	{
		//base.Execute (entityType);
		if(Vector3.Distance(entity.Player.transform.position,entity.transform.position)>5)
			entity.GetFSM().ChangeState(Enemy_SeekState.Instance());

		
		time+=Time.deltaTime;
		if(time>1.0f)
		{
			time=0;		
			entity.animation.CrossFade(entity.FireGun.name,0.1f);
			entity.transform.forward=Heading(entity.Player.transform.position,entity.transform.position);
			entity.ShowMuzzle();
			Shoot(entity);
		}
	}
	
	public override void Exit (Enemy entity)
	{
		//base.Exit (entityType);
		Debug.Log("Enemy_FireState Exit!!!");
	}
	
	public override bool OnMessage (Enemy entity, Telegram telegram)
	{
		//return base.OnMessage (entityType, telegram);
		return false;
	}
	
	public Vector3 Heading(Vector3 targetPos,Vector3 entityPos)
	{
		Vector3 Direction=Vector3.Normalize(targetPos-entityPos);
		return new Vector3(Direction.x,0,Direction.z);
	}
	
	public void Shoot(Enemy entity)
	{
		Ray ray = new Ray(entity.Turret.transform.position,entity.transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit,1000,LayerMask.NameToLayer("player")))
		{
			hit.transform.SendMessage("OnHit",new RayAndHit(ray,hit),SendMessageOptions.DontRequireReceiver);
			//entity.MuzzleFlash.transform.localRotation*=Quaternion.Euler(0,0,Random.Range(-360,360));
			entity.SendMessage("OnFire");
		}
	}
}

public class Enemy_EvadeState:State<Enemy>{
	private static Enemy_EvadeState instance;
	public static Enemy_EvadeState Instance()
	{
		if(instance==null)
			return new Enemy_EvadeState();
		return instance;
	}
	
	public override void Enter (Enemy entityType)
	{
		//base.Enter (entityType);
	}
	
	public override void Execute (Enemy entityType)
	{
		//base.Execute (entityType);
	}
	
	public override void Exit (Enemy entityType)
	{
		//base.Exit (entityType);
	}
	
	public override bool OnMessage (Enemy entityType, Telegram telegram)
	{
		//return base.OnMessage (entityType, telegram);
		return false;
	}
}