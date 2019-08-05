using UnityEngine;
using System.Collections;

public class InputModel : MonoBehaviour {
    public bool isValid;
	private SteamVR_TrackedObject steamObject;
	private SteamVR_Controller.Device steamController;
    // [Double Click parameters]
    private float prevPressTriggerTime = 0f;
    private float doubleClickTH = 0.5f;

	void Awake(){
		steamObject = this.GetComponent<SteamVR_TrackedObject> ();
	}


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		// Track controller
		isValid = steamObject.isValid;
		steamController = SteamVR_Controller.Input ((int)steamObject.index);

	}

	public bool TriggerPressDown(){
		if (isValid) {
			if (steamController.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
				return true;
			}
			return false;
		}
		return false;
	}

	public bool TriggerPressUp(){
		if (isValid) {
			if (steamController.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) {
				return true;
			}
			return false;
		}
		return false;
	}

	public bool TriggerPressing(){
		if (isValid) {
			if (steamController.GetPress (SteamVR_Controller.ButtonMask.Trigger)) {
				return true;
			}
			return false;
		}
		return false;
	}

	public bool DoubleClickTrigger(){
		if (isValid) {
			if (steamController.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
				float nowTime = Time.time;
				if (Mathf.Abs (nowTime - prevPressTriggerTime) < doubleClickTH) {
					prevPressTriggerTime = 0.0f;
					return true;
				} else {
					prevPressTriggerTime = nowTime;
					return false;
				}
			}
			return false;
		}
		return false;
	}

	public bool PadPressDown(){
		if (isValid) {
			if (steamController.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
				return true;
			}
			return false;
		}
		return false;
	}

	public bool PadTouchDown(){
		if (isValid) {

			if (steamController.GetTouchDown (SteamVR_Controller.ButtonMask.Touchpad)) {
				return true;
			}
		}
		return false;
	}

	public bool PadTouchUp(){
		if (isValid) {

			if (steamController.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad)) {
				return true;
			}
		}
		return false;
	}

	public bool PadPressDownUpward(){
		if (isValid) {
			
			if (steamController.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
				Vector2 padAxis = steamController.GetAxis ();

				if (padAxis.y > 0.0f) {
					return true;
				}
			}
		}
		return false;
	}

	public bool PadPressDownDownward(){
		if (isValid) {

			if (steamController.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
				Vector2 padAxis = steamController.GetAxis ();
				if (padAxis.y < 0) {
					return true;
				}
			}
		}
		return false;
	}

	public bool PadPressDownRight(){
		if (isValid) {

			if (steamController.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
				Vector2 padAxis = steamController.GetAxis ();

				if (padAxis.x > 0.0f) {
					return true;
				}
			}
		}
		return false;
	}

	public bool PadPressDownLeft(){
		if (isValid) {

			if (steamController.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
				Vector2 padAxis = steamController.GetAxis ();

				if (padAxis.x < 0.0f) {
					return true;
				}
			}
		}
		return false;
	}

	public bool GripPressDown(){
		if (isValid) {
			if (steamController.GetPressDown (SteamVR_Controller.ButtonMask.Grip)) {
				return true;
			}
			return false;
		}
		return false;
	}

	public bool ApplicationPressDown(){
		if (isValid) {
			if (steamController.GetPressDown (SteamVR_Controller.ButtonMask.ApplicationMenu)) {
				return true;
			}
			return false;
		}
		return false;
	}
	public int PadClickX(){
		if (isValid) {
			if (!steamController.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
				return 0;
			}
			if (steamController.GetAxis ().x > 0.8f) {
				return 1;
			} else if (steamController.GetAxis ().x < -0.8f){
				return -1;
			}
		}
		return 0;
	}
}
