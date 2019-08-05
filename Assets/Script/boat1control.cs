using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boat1control : MonoBehaviour {

    private Animator m_animator;
    private int Boatstate;
    public GameObject player;
    public GameObject fishbody;
    public GameObject FatherFish;
    public GameObject cam;
    private SteamVR_Fade fade;
    public float blackTime = 10f;
    public GameObject oilwater;
    private float boatRotaTime = 5f;
    private Vector3 fixposition;
    public GameObject garbage;
    private float transTime = 3f;
    public CommWithArduino commWithArduino;
    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        Boatstate = 1;
        fade = cam.GetComponent<SteamVR_Fade>();
    }
	
	// Update is called once per frame
	void Update () {

        //狀態1，靠近魚後船隻遠離我
        if (Boatstate == 1)
        {
            float dist = Vector3.Distance(player.transform.position, FatherFish.transform.position);
            if (dist < 100)
            {
                m_animator.SetBool("state1", true);
                Boatstate += 1;

            }

        }

        //狀態2，當玩家遠離到一定距離後到狀態3
        else if (Boatstate == 2)
        {
            float dist = Vector3.Distance(player.transform.position, FatherFish.transform.position);
            if (dist > 400)
            {
                Boatstate += 1;
            }

        }
        //狀態3，當玩家靠近到一定距離後到狀態4
        else if (Boatstate == 3)
        {
            float dist = Vector3.Distance(player.transform.position, FatherFish.transform.position);
            if (dist < 300)
            {
                Boatstate += 1;
            }
        }
        //狀態4，玩家畫面開始變黑
        else if (Boatstate == 4)
        {
            //花5秒時間由清晰轉黑
            commWithArduino.inflate = true;
            SteamVR_Fade.Start(Color.black, 5f);
            Boatstate += 1;
            

        }
        //狀態5，玩家畫面持續變黑一段時間
        else if (Boatstate == 5)
        {
            blackTime = blackTime - Time.deltaTime;

            if (blackTime < 0)
                Boatstate += 1;
        }
        //狀態6，玩家畫面變清晰，船轉動
        else if (Boatstate == 6)
        {
            commWithArduino.deflate = true;
            //油汙消失
            oilwater.SetActive(false);
            //開啟畫面
            SteamVR_Fade.Start(Color.clear, 1f);
            //讓船開始轉動方向
            m_animator.SetBool("state2", true);
            m_animator.SetBool("state3", true);
            fixposition = fishbody.transform.position;
            Boatstate += 1;
        }
        //玩家看著船轉動
        else if (Boatstate == 7)
        {
            //固定玩家位置
            fishbody.transform.position = fixposition;
            //倒數5秒
            boatRotaTime -= Time.deltaTime;
            if (boatRotaTime < 0f)
                Boatstate += 1;
        }
        //船開始移動至新地點
        else if (Boatstate == 8)
        {
            //顯示垃圾
            garbage.SetActive(true);
            Boatstate += 1;
        }
        //當靠近至一定距離時，被電暈
        else if (Boatstate == 9) {

            float dist = Vector3.Distance(player.transform.position, FatherFish.transform.position);
            if (dist < 80)
            {
                commWithArduino.electricShock = true;
                SteamVR_Fade.Start(Color.black, 3f);
                Boatstate += 1;
            }
        }
        //轉到新場景
        else if (Boatstate == 10) {
            transTime -= Time.deltaTime;
            if (transTime < 0) {
                Application.LoadLevel(1);
            }

        }

    }

    
}
