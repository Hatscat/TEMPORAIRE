using UnityEngine;
using System.Collections;



public class MoveFromInput: MonoBehaviour, IMove {
    /*
	private int mouseAxis;
	private Transform _transform;

	public float lerpCoef=10f;

	void Awake()
	{
		_transform = transform;
	}

	#region IRacketMove Methods
	public void Init (params object[] parameters)
	{
		lerpCoef = (float)parameters[0];
		this.mouseAxis = 0;
		for (int i = 1; i < parameters.Length; i++)
			this.mouseAxis|=(int)parameters[i];
		
	}

	public void Move()
	{
		if(mouseAxis==0) return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane plane = new Plane(Vector3.forward,transform.position);
		float dist;
		
		plane.Raycast(ray,out dist);
		
		Vector3 pt = ray.GetPoint(dist);

		Vector3 pos = _transform.position;

		if((this.mouseAxis & (int)AXIS.x) != 0)
			pos.x = pt.x;
		
		if((this.mouseAxis & (int)AXIS.y) != 0)
			pos.y =pt.y;

		_transform.position  = Vector3.Lerp(_transform.position,pos,CustomTimer.manager.DeltaTime*lerpCoef);
	}
	#endregion
     */
}
