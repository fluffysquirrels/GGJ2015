using UnityEngine;
using System.Collections;

public class BreathSpawner : MonoBehaviour 
{

	// holds our breath particle system prefab reference
	public GameObject breathEmitter;
	
	private GameObject _emitter;
	private Animator _anim;

	// Use this for initialization
	void Awake () 
	{
		_anim = transform.root.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( IsPanting() )
		{	
			if ( !_emitter )
			{
				// spawn emitter
				_emitter = (GameObject)Instantiate( breathEmitter, transform.position, transform.rotation );
				_emitter.transform.parent = transform;
			}
		} else
		{
			// destroy emitter
			Destroy( _emitter );
			_emitter = null;
		}
	}
	
	public bool IsPanting()
	{
		return _anim.GetCurrentAnimatorStateInfo(0).IsName( "IdlePant_Cycle" ) || _anim.GetCurrentAnimatorStateInfo(0).IsName( "IdlePant_Into" );
	}
}
