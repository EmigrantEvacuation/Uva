using System;
using UnityEngine;

namespace UvaSimulator.Extension{
	
	public static class UvaExtends {

		public static Vector3 localforward(this Transform transform){
			Vector3 globalforward = transform.forward;
			globalforward.Normalize ();
			return transform.InverseTransformDirection (globalforward);
		}
	}
}
