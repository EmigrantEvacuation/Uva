using System;
using UnityEngine;

namespace UvaSimulator.Extension{
	
	public static class UvaExtends {

        // transform
		public static float GetPitchAngle(this Transform transform){
			var globalforward = transform.forward; //world xyz
            globalforward.y = 0;
            globalforward.Normalize(); // set the vector's maganitude to 1

            var localforward = transform.InverseTransformDirection(globalforward);
            if(localforward.z == 0.0f)
            {
                return localforward.y >= 0.0f ? 90.0f : -90.0f;
            }
            float pitchangle = Mathf.Atan2(localforward.y, localforward.z);
            return pitchangle;
        }

        public static float GetYawAngle(this Transform transform)
        {
            var globalforward = transform.forward;
            globalforward.x = 0;
            globalforward.Normalize();

            var localforward = transform.InverseTransformDirection(globalforward);
            if(localforward.z == 0.0f)
            {
                return localforward.x >= 0.0f ? 90.0f : -90.0f;
            }
            float yawangle = Mathf.Atan2(localforward.x, localforward.z);
            return yawangle;
        }

        public static float GetRollAngle(this Transform transform){
			var globalforward = transform.forward; //world xyz
            globalforward.y = 0;
            globalforward.Normalize(); // set the vector's maganitude to 1

            var localforward = transform.InverseTransformDirection(globalforward);
            var localright = Vector3.Cross(globalforward, Vector3.up);
            if(localforward.x == 0.0f)
            {
                return localforward.y >= 0.0f ? 90.0f : -90.0f;
            }
	        float rollangle = Mathf.Atan2 (localright.y, localright.x);
            return rollangle;
        }
	}
}
