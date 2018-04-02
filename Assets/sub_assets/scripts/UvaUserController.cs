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
namespace UvaSimulator.Uva
{
	[RequireComponent(typeof (Rigidbody))]
	public class UvaUserController : MonoBehaviour {

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
        public float alititude { get; private set; }
        public float throttle { get; private set; }
        public float pitchAngle { get; private set; } 
        public float rollAngle { get; private set; }
        public float yawAngle { get; private set; }
        public float speedFoward { get; private set; }
        public bool airBrakes { get; private set; }
        public float enginPower { get; private set; }

		// Use this for initialization
		void Start () {
			
            // get all the 
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}