using System;
using UnityEngine;
using UvaSimulator.Extension;
/*
 * how can a plane take off ?
 * it needs lift strength to get off the ground, engin power for pushing, brakes to slow down, and air drag everywhere,
 * and don't forget gravity
 * 
 *                           g| 
 *                             \
 *         engin power->  \_____\____      <- air drag and brakes            it is just a common condition of flight
 *                              /    \
 *                             / 
 *                             ^
 *                       lift strength
 *                       
 *to calculate the lift 
 * Y = (1/2.0) * listEffect * airDensity * airSpeed^2 * wingArea
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
		[SerializeField] private float liftEffect = 0.002f;                 // lift effect caused by speed from wings
        [SerializeField] private float dragIncreaseFactor = 0.001f;       // how much linear drag should increase
        [SerializeField] private float angularDragIncreaseFactor = 0.02f; // how much angular drag should increase 
        [SerializeField] private float airDensity = 0.5f;                 // air Density

        // Aeroplane Configuration
		[SerializeField] private float maxEnginPower = 40.0f;      // max engin power 
		[SerializeField] private float zeroLiftSpeed = 300.0f;     // speed that the lift strength no longer applied
        [SerializeField] private float airBrakesEffect = 0.2f;     // brakes
        [SerializeField] private float fuelAmount = 200.0f;        // total fuel
        [SerializeField] private float pitchEffect = 100f;         // pitch Effect
        [SerializeField] private float rollEffect = 70f;          // roll Effect
        [SerializeField] private float yawEffect = 20f;           // yaw Effect
        [SerializeField] private float throttleChangeSpeed = 0.3f; // the speed of throttle changing
        [SerializeField] private float wingArea = 1.0f;            // the area of wings 

        // Aeroplane Condition
		public float Alititude { get; private set; }
        public float ForwardSpeed { get; private set; }
        public float Throttle { get; private set; }
		public float ThrottleInput { get; private set; }
        public float PitchAngle { get; private set; }
        public float PitchInput { get; private set; }
		public float RollAngle { get; private set; }
		public float RollInput { get; private set; }
		public float YawAngle { get; private set; }
		public float YawInput { get; private set; }
		public float SpeedFoward { get; private set; }
		public bool AirBrakes { get; private set; }
		public float EnginPower { get; private set; }

		// RigidBody
		private Rigidbody planeRigid;

		// Use this for initialization
		void Start () {
			
			// get all the rigids
			planeRigid = GetComponent<Rigidbody>();
		
		}
		
		public void Move(float pitchinput = 0, float rollinput = 0, float yawinput = 0, float throttleinput = 0, bool airbrakes = false){
			ThrottleInput = throttleinput;
			PitchInput = pitchinput;
			RollInput = rollinput;
			YawInput = yawinput;
		    AirBrakes = airbrakes;

			_ClampInputs ();
           
            _CalculateRollYawPitchAngle();
           
            _CalculateForwardSpeed();
            
            _CalculateLiftEffect();
           
            _CalculateThrottleAndFuel();
          
            _CalculateAirLift();

            _CalculateTorque();

            _CalculateEnginForce();
           
            _CalculateDrag();
           
            _CalculateAlititude();
            
		}

		private void _ClampInputs()
		{
            //print(PitchInput);
			ThrottleInput = Mathf.Clamp (ThrottleInput, -1, 1);
			PitchInput = Mathf.Clamp (PitchInput, -1, 1);
            //print(PitchInput);
			RollInput = Mathf.Clamp (RollInput, -1, 1);
			YawInput = Mathf.Clamp (YawInput, -1, 1);
		}

		private void _CalculateRollYawPitchAngle()
		{
            PitchAngle = transform.GetPitchAngle();
            RollAngle = transform.GetRollAngle();
            YawAngle = transform.GetYawAngle();
		}

        private void _CalculateForwardSpeed()
        {
            var localvelocity = transform.InverseTransformDirection(planeRigid.velocity);
            ForwardSpeed = Mathf.Max(0.0f, localvelocity.z); //when it hit brakes and slow down, it cannot reverse
        }

        private void _CalculateLiftEffect()
        {
            liftEffect = Mathf.InverseLerp(zeroLiftSpeed, 0.0f, ForwardSpeed);
        }

        private void _CalculateThrottleAndFuel()
        {
            Throttle = Mathf.Clamp01(Throttle + ThrottleInput * Time.deltaTime * throttleChangeSpeed);
            EnginPower = Throttle * maxEnginPower;
            fuelAmount -= Throttle * Time.deltaTime;
        }

        private void _CalculateAirLift()
        {
            // THINKING: I'm not sure if it does need further consideration
            var liftDirection = transform.up;
            float lift = 0.5f * liftEffect * airDensity * SpeedFoward * SpeedFoward * wingArea;
            planeRigid.AddForce(lift * liftDirection);
        }

        private void _CalculateEnginForce()
        {
            var enginforce = EnginPower * transform.forward;
            planeRigid.AddForce(enginforce);
        }

        private void _CalculateDrag()
        {
            // the drag includes linear drag and angular drag
            // the angular is needed for that it will become more difficult to turn in high speed

            // TODO: the whole drag system need further consideration

            // first, drag caused by speed
            var linearairdrag = planeRigid.velocity.magnitude * dragIncreaseFactor;

            // second, drag changes by airbrake
            linearairdrag += AirBrakes ? (planeRigid.velocity.magnitude * airBrakesEffect) : 0.0f;

            // third, angular drag caused by speed
            var angulardrag = planeRigid.velocity.magnitude * angularDragIncreaseFactor;

            planeRigid.drag = linearairdrag;
            planeRigid.angularDrag = angulardrag;
        }

        private void _CalculateTorque()
        {
            // calculate the torque based on pitch & roll & yaw input

            //print(1);
            var torque = Vector3.zero;
            torque += PitchInput * pitchEffect * transform.right;
            //print(PitchInput);
            torque += RollInput * rollEffect * transform.forward;
            torque += YawInput * yawEffect * transform.up;
            planeRigid.AddTorque(torque);
        }

        private void _CalculateAlititude()
        {
            var raydown = new Ray(transform.position, -Vector3.up);
            RaycastHit hit;
            Alititude = Physics.Raycast(raydown, out hit) ? hit.distance : transform.position.y;
        }
	}
}