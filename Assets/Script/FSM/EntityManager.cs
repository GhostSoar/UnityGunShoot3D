using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class EntityType<entity_type>
{
	public entity_type target;
	public string name;
}


public class EntityManager
{
	private static EntityManager instance;
	private Dictionary<int, BaseGameEntity> m_EntityMap = new Dictionary<int, BaseGameEntity> ();
	
	public BaseGameEntity GetEntityFromID(int ID)
	{
		foreach(KeyValuePair<int ,BaseGameEntity> val in m_EntityMap)
		{
			if(val.Key == ID)
				return val.Value;
		}
		return null;
	}


	public void RemoveEntity (BaseGameEntity pEntity)
	{
		foreach(KeyValuePair<int ,BaseGameEntity> val in m_EntityMap)
		{
			if(val.Value == pEntity)
				m_EntityMap.Remove(val.Key);
		}
	}

	public void RegisterEntity (BaseGameEntity NewEntity)
	{
		m_EntityMap.Add (NewEntity.ID(),NewEntity);
	}
	
	public static EntityManager Instance ()
	{
		if (instance == null)
			instance = new EntityManager ();
		return instance;
	}

}
