using System;
using UnityEngine;
using UnityEngine.UI;
using UvaSimulator.Uva.ControlCalculator;

namespace UvaSimulator.UI
{
    public class UvaUIController : MonoBehaviour {
            
        public UvaUserControllCalculator Uva;
        public Text SpeedData;
        public Text EnginPowerData;
        public Text FuelData;
        public Text AltitudeData;
            
        private float forwardSpeed { get; set; }
        private float fuel { get; set; }
        private float enginPower { get; set; }
        private float altitude { get; set; }
            
	    // Use this for initialization
	    void Start () {
            forwardSpeed = 0.0f; // speed init
            fuel = Uva.Fuel;     // fuel 
            enginPower = 0.0f;   // enginPower init
            altitude = 0.0f;     // altitude
	    }
	    
	    // Update is called once per frame
	    void Update () {
            forwardSpeed = Uva.ForwardSpeed;
            fuel = Uva.Fuel;
            enginPower = Uva.EnginPower;
            altitude = Uva.Altitude;
            SpeedData.text = ((int)forwardSpeed).ToString() + " KM/H";
            EnginPowerData.text = ((int)enginPower).ToString() + " BHP";
            FuelData.text = ((int)fuel).ToString() + " L";
            AltitudeData.text = ((int)altitude).ToString() + " M";
	    }
    }
}
