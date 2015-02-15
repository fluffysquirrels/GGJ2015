using UnityEngine;
using System.Collections;

public class Ragdollify : MonoBehaviour 
{
	// holds reference to ragdoll prefab
	public Transform ragdoll;
	// reference to top of bone hierarchy
	public bool shouldRagdoll;

	// copies transform values from source to destination
	private void CopyTransforms ( Transform source, Transform destination ) 
	{
		foreach( Transform child in destination )
		{
			destination.position = source.position;
			destination.rotation = source.rotation;
			Transform currentSource = source.Find( child.name );
			if( currentSource )
			{
				CopyTransforms(currentSource, child);
			}
		}
	}

	// instantiates the ragdoll prefab and copies transform values over using utility function
	public void GotoRagdoll ()
	{
		ragdoll = Instantiate( ragdoll, transform.position, transform.rotation ) as Transform;
		Transform ragdollRoot = ragdoll.Search( "Player_Reference" );
		Transform playerRoot = transform.Search( "Player_Reference" );
		CopyTransforms( playerRoot, ragdollRoot );
		Destroy( gameObject );
	}

	void Update ()
	{
		if( shouldRagdoll )
		{
			shouldRagdoll = false;
			GotoRagdoll();
		}
	}
}
