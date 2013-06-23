using UnityEngine;
using System.Collections;

public class MessageManager : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		MessageDispatcher.Instance().DispatchDelayedMessages();
	}
}
