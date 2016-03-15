using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {

	public float viewRadius;
	[Range(0,360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	public float meshResolution;

	void DrawFieldOfView()
	{
		int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
		float stepAngleSize = viewAngle / stepCount;

		for (int i = 0; i <= stepCount; i++) 
		{
			float angle = transform.eulerAngles.y - viewAngle/2 + stepAngleSize * i;
			Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius, Color.red);
		}
	}

	void Start()
	{

		StartCoroutine ("FindTargetsWithDelay", .2f);

	}


	IEnumerator FindTargetsWithDelay(float delay)
	{
		while (true) 
		{
			yield return new WaitForSeconds (delay);
			FindVisibleTargets();
		}
	}

	void Update()
	{
		DrawFieldOfView ();
	}

	void FindVisibleTargets()
	{

		visibleTargets.Clear ();
		//Array of all targets within radius
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		//Loop through all of theese
		for (int i=0; i < targetsInViewRadius.Length; i++) 
		{
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float distToTarget = Vector3.Distance(transform.position, target.position);

				if(!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
				{
					visibleTargets.Add(target);
				}
			}

		}
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal) 
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
