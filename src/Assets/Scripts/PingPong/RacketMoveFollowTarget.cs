using UnityEngine;
using System.Collections;

public class MoveFollowTarget : MonoBehaviour, IMove {

	private Transform _transform;
	private int followAxis=0;
	private Transform target;

	void Awake()
	{
		_transform = transform;
	}

	#region IRacketMove Methods
	public void Init (params object[] parameters)
	{
		this.target = parameters[0] as Transform;

		this.followAxis = 0;
		for (int i = 1; i < parameters.Length; i++)
			this.followAxis|=(int)parameters[i];
	}

	public void Move()
	{
		if(target==null || followAxis==0) return;

		Vector3 pos = _transform.position;

		if((this.followAxis & (int)AXIS.x) != 0)
			pos.x = target.position.x;

		if((this.followAxis & (int)AXIS.y) != 0)
			pos.y = target.position.y;

		if((this.followAxis & (int)AXIS.z) != 0)
			pos.z = target.position.z;

		_transform.position = pos;
	}
	#endregion
}
