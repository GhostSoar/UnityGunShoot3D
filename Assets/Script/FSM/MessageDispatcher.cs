using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Telegram
{

	public int Sender;

	public int Receiver;

	public int Msg;
	
	
	public float DispatchTime;
	
	public MonoBehaviour _Behaviour;
	
	

	public Telegram (float atime, int asender, int areceiver, int amsg,MonoBehaviour _Be)
	{
		DispatchTime = atime;
		Sender = asender;
		Receiver = areceiver;
		Msg = amsg;
		_Behaviour = _Be;
	}
	
}


public class MessageDispatcher
{


	public float SEND_MSG_IMMEDIATELY = 0.0f;
	public int NO_ADDITIONAL_INFO = 0;
	public int SENDER_ID_IRRELEVANT = -1;
	private static MessageDispatcher instance;
	private IList<Telegram> PriorityQ = new List<Telegram> ();

	private void Discharge (BaseGameEntity pReceiver, Telegram telegram)
	{
		if (!pReceiver.HandleMessage (telegram)) {
			Debug.LogError ("bu neng jie xi de xiao xi ");
		}
	}
	
	public void DispatchMessage (float delay, 	/*fa song zhe */int sender, 	/*jie shou zhe */int receiver, int msg,MonoBehaviour _be)
	{
		
		
		//jie shou zhe de zhi zhen 
		BaseGameEntity pReceiver = EntityManager.Instance ().GetEntityFromID (receiver);
		
		
		//chuang jian yi ge xiao xi 
		Telegram telegram = new Telegram (0, sender, receiver, msg,_be);
		
		if (delay <= 0.0f) {
			
			//li ji fa song xiao xi  
			Discharge (pReceiver, telegram);
		} else {
			
			//yan chi fa song xiao xi 
			float CurrentTime = Time.realtimeSinceStartup;
			
			telegram.DispatchTime = CurrentTime + delay;

			foreach(Telegram val in PriorityQ)
			{
				if(val.Sender == sender && val.Receiver == receiver && val.Msg ==msg)
				{
					return ;
				}
			}

			PriorityQ.Add (telegram);
			
		}
		
	}


	public void DispatchDelayedMessages ()
	{
		
		
		//zui chu de shi jian 
		float CurrentTime = Time.realtimeSinceStartup;
		
		for(int i = 0 ;i < PriorityQ.Count ; i++)
		{
			Telegram val  = PriorityQ[i];
			if (val.DispatchTime < CurrentTime && val.DispatchTime > 0f) {
				//chao guo shi jian de shan chu diao 
				BaseGameEntity pReceiver = EntityManager.Instance ().GetEntityFromID (val.Receiver);
				Discharge (pReceiver, val);
				PriorityQ.RemoveAt(i);
			}
		}
	}
	
	public static MessageDispatcher Instance ()
	{
		if (instance == null) {
			instance = new MessageDispatcher ();
		}
		return instance;
	}
}
