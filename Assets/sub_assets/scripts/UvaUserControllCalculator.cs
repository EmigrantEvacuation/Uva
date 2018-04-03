using System;
using UnityEngine;
/*
 * how can a plane take off ?
 * it needs lift strength to get off the ground, engin power for pushing, brakes to slow down, and air drag everywhere,
 * and don't forget gravity
 * 
 *                                              g| 
 *                                              \
 *                          engin power->  \_____\____      <- air drag and brakes
 *                                               /    \
 *                                              / 
 *                                              ^
 *                                        lift strength
 *                                        
 * and we should know something important when flying a aeroplane
 * first  alititude, the height of the plane
 * second thorttle, it control the power of the engin, and it has something to do with the time it can fly——the fuel tank
 * then it comes to three angles: 
 * Pitch, head looking up or down 
 * Roll, head moving towards right shoulder or the left shoulder
 * Yaw, head looking right or left
 *                                
 * and we also should know some phenomenon of the aeroplane
 * 1. stall, the plane does not have enough speed to provide lift aganist the G force
 * 2. sonic boom, the plane is so fast that its speed is far beyond the speed of sound
 */
namespace UvaSimulator.Uva.ControlCalculator
{
	[RequireComponent(typeof (Rigidbody))]
	public class UvaUserControllCalculator : MonoBehaviour {

        // Aerodynamic 
		[SerializeField] private float liftEffect = 0.2f;        // lift effect caused by speed from wings
		[SerializeField] private float aerodynamicEffect = 1.0f; // how air effect the uva    

        // Aeroplane Configuration
		[SerializeField] private float maxEnginPower = 40.0f;    // max engin power 
		[SerializeField] private float zeroLiftSpeed = 300.0f;   // speed that the lift strength no longer applied
        [SerializeField] private float brakesEffect = 0.2f;      // brakes
        [SerializeField] private float fuelAmount = 200.0f;      // total fuel
        [SerializeField] private float pitchEffect = 0.2f;       // pitch Effect
        [SerializeField] private float rollEffect = 0.2f;        // roll Effect
        [SerializeField] private float yawEffect = 0.1f;         // yaw Effect

        // Aeroplane Condition
		private float alititude { get; set; }
        private float throttle { get; set; }
		private float throttleInput { get; set; }
		private float pitchAngle { get; set; }
		private float pitchInput { get; set; }
		private float rollAngle { get; set; }
		private float rollInput { get; set; }
		private float yawAngle { get; set; }
		private float yawInput { get; set; }
		private float speedFoward { get; set; }
		private bool airBrakes { get; set; }
		private float enginPower { get; set; }

		// RigidBody
		private Rigidbody planeRigid;
		private WheelCollider[] wheelRigids;

		// Use this for initialization
		void Start () {
			
			// get all the rigids
			planeRigid = GetComponent<Rigidbody>();
			var colliders = transform.Find ("Colliders");
			wheelRigids = colliders.GetComponentsInChildren<WheelCollider>();
		
		}
		
		public void Move(float pitchinput = 0, float rollinput = 0, float yawinput = 0, float throttleinput = 0, bool airbrakes = false){
			throttleInput = throttleinput;
			pitchInput = pitchinput;
			rollInput = rollinput;
			yawInput = yawinput;
		    airBrakes = airbrakes;

			_ClampInputs ();

		}

		private void _ClampInputs()
		{
			throttleInput = Mathf.Clamp (throttleInput, -1, 1);
			pitchInput = Mathf.Clamp (pitchInput, -1, 1);
			rollInput = Mathf.Clamp (rollInput, -1, 1);
			yawInput = Mathf.Clamp (yawInput, -1, 1);
		}

		private void _CalculateRollYawPitchAngle()
		{
			var globalforward = transform.forward;
			globalforward.y = 0;
			if (globalforward.sqrMagnitude > 0) {
				globalforward.Normalize (); // set the vector's 
				var localforward = transform.InverseTransformDirection (globalforward);
				pitchAngle = Mathf.Atan2 (localforward.y, localforward.z);

				rollAngle = Mathf.Atan2 (localforward.y, localforward.x);
				yawAngle = Mathf.Atan2 (localforward.x, localforward.z);
			}
		}
	}
}