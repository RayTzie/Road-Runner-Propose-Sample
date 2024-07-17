using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour {

	private float m_horizontalInput;
	private float m_verticalInput;
	public float m_spaceKeyDown; 
	private float m_steeringAngle;

	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public float maxSteerAngle = 45;
	public float motorForce = 50;
	public float brakeForce = 200;
	public bool isBraking = false;


	public void GetSteer()
	{
		m_horizontalInput = Input.GetAxis("Horizontal");
		m_verticalInput = Input.GetAxis("Vertical");
	  
	
	}

	public void GetBrake(){
		      // Check if space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBraking = true;
        }

        // Check if space bar is released (optional)
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBraking = false;
        }

        // Apply braking logic (modify based on your movement implementation)
        if (isBraking)
        {
            // Reduce speed gradually
           		
			frontDriverW.brakeTorque = brakeForce;
			frontPassengerW.brakeTorque = brakeForce;
        }
        else {
        	frontDriverW.brakeTorque = 0;
			frontPassengerW.brakeTorque = 0;
        }
	}

	private void Steer()
	{
		m_steeringAngle = maxSteerAngle * m_horizontalInput;
		frontDriverW.steerAngle = m_steeringAngle;
		frontPassengerW.steerAngle = m_steeringAngle;
	}

	private void Accelerate()
	{
		frontDriverW.motorTorque = m_verticalInput * motorForce;
		frontPassengerW.motorTorque = m_verticalInput * motorForce;
	}

	

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontDriverW, frontDriverT);
		UpdateWheelPose(frontPassengerW, frontPassengerT);
		UpdateWheelPose(rearDriverW, rearDriverT);
		UpdateWheelPose(rearPassengerW, rearPassengerT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}

	private void FixedUpdate()
	{
		GetSteer();
		Steer();
		Accelerate();
		UpdateWheelPoses();
		GetBrake();
		
	}

}
