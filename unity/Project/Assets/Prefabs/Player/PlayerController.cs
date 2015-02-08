using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Animator _anim;
	private CharacterController _characterController;
	private CapsuleCollider _capsuleCollider;

	// Use this for initialization
	void Awake () 
	{
		_anim = GetComponent<Animator>();
		_characterController = GetComponent<CharacterController>();
		_capsuleCollider = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( IsDucking() )
		{
			// adjust controller center
			float x = _characterController.center.x;
			float y = _anim.GetFloat( "ColliderCenterY" );
			float z = _characterController.center.z;
			_characterController.center = new Vector3( x, y, z );
			// adjust controller height
			_characterController.height = y * 2;
			// adjust collider center
			_capsuleCollider.center = new Vector3( x, y, z );
			// adjust collider height
			_capsuleCollider.height = y * 2;			
		}
	}
	
	private bool IsDucking ()
	{
		return _anim.GetCurrentAnimatorStateInfo(0).IsTag( "Ducking" );
	}
}
