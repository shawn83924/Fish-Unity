using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson {

    public class SetUnderwater : MonoBehaviour {

        public float WaterLevel;
        public float speed=100;
        private bool isUnderWater;
        private Color normalcolor;
        private Color underwatercolor;
        //private RigidbodyFirstPersonController chMotor;
        public GameObject player;
        private ConstantForce constforce;
        private Vector3 Localforward;
        private InputModel control;
        public GameObject controller;
        public GameObject particle;
        public float swimTime;
        private float count;

        // Use this for initialization
        void Start () {
            normalcolor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            underwatercolor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
            //chMotor = GetComponent<RigidbodyFirstPersonController>();
            constforce = player.GetComponent<ConstantForce>();
            control = controller.GetComponent<InputModel>();
            particle.SetActive(false);
            count = 0;
        }
	
	    // Update is called once per frame
	    void Update () {
            
            if ( (transform.position.y < WaterLevel ) != isUnderWater ) {
                isUnderWater = transform.position.y < WaterLevel;
                if (isUnderWater) Setunderwater();
                if (!isUnderWater) Setnormal();
                
            }
            //如果在水底下，按按鍵來游泳
            if (isUnderWater && control.TriggerPressing())
            {
                if (count < swimTime)
                {//設定游泳時間
                    count += Time.deltaTime;
                    Localforward = transform.forward;
                    constforce.relativeForce = new Vector3(Localforward.x * speed,
                                                           Localforward.y * speed,
                                                           Localforward.z * speed);
                    //開啟速度線
                    particle.SetActive(true);
                }
                else {
                    constforce.relativeForce = new Vector3(0, 0, 0);
                    //關閉速度線
                    particle.SetActive(false);
                }
                
            }
            if(!control.TriggerPressing()) //如果按鍵放開停止游泳
            {
                constforce.relativeForce = new Vector3(0, 0, 0);
                //關閉速度線
                particle.SetActive(false);
                count = 0;
            }

        }
        //出水後設定
        void Setnormal() {
            //視野恢復正常
            RenderSettings.fog = false;
            RenderSettings.fogColor = normalcolor;
            RenderSettings.fogDensity = 0.001f;
            //把場景重力恢復
            Physics.gravity = new Vector3(0f, -9.81f, 0);
        }
        //入水後設定
        void Setunderwater() {
            //視野改成水中
            RenderSettings.fog = true;
            RenderSettings.fogColor = underwatercolor;
            RenderSettings.fogDensity = 0.001f;
            //把場景重力減輕(約1/3)
            Physics.gravity = new Vector3(0f, -9.81f / 10, 0);
        }
    }

}
