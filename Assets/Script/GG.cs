using UnityEngine;
using System.Collections;

public class GG : MonoBehaviour {
	
	public AnimationClip FireGun;
	public AnimationClip WalkRun;
	
	public Texture texture;
    // Use this for initialization
	void Start () {		
		animation.CrossFade(WalkRun.name, 0.1f);
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.D))
		{
			animation.CrossFade(WalkRun.name, 0.1f);
		}
		
		if(Input.GetMouseButton(0))
		{
			animation.CrossFade(FireGun.name, 0.1f);
		}
	}
	
	void OnGUI()
	{
		/*
		Rect rect=new Rect(
			Input.mousePosition.x-(texture.width>>1),
			Screen.height-Input.mousePosition.y-(texture.height>>1),
			texture.width,
			texture.height);
		GUI.DrawTexture(rect,texture);
		*/
		
		GUI.DrawTexture(new Rect(Screen.width/2-(texture.width*0.5f), Screen.height/2-(texture.height*0.5f), texture.width, texture.height), texture);
	}
}
