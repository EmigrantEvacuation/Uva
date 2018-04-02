using System;
using UnityEngine;

namespace UvaSimulator.Uva
{
	[RequireComponent(typeof (Rigidbody))]
	public class UvaUserController : MonoBehaviour {


		[SerializeField] private float MaxEnginPower = 40.0f;    // max engin power 
		[SerializeField] private float Lift = 0.2f;              // lift strength caused by speed from wings
		[SerializeField] private float ZeroLiftSpeed = 300.0f;   // speed that the lift strength no longer applied
		[SerializeField] private float PitchEffect = 0.2f;       // pitch Effect
		[SerializeField] private float RollEffect = 0.2f;        // roll Effect
		[SerializeField] private float YawEffect = 0.1f;         // yaw Effect
		[SerializeField] private float AerodynamicEffect = 1.0f; // how air effect the uva

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}