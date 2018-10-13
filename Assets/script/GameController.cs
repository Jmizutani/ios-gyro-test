using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject ball;
	public GameObject stage;
    public Button start;
    public Text signal;
    public Text timetex;
    private bool startflag = false;
    private float duration = 3f;
    private float time;
    private bool timeflag=false;
    private bool check1 = false;
    private bool check2 = false;
    private bool goalflag = false;
    public GameObject ballpos;
    private float dur = 3f;
    private Vector3 restart;
    private bool restartflag = false;
    public Button restartbtn;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if(!restartflag && ballpos.transform.localPosition.y<0f){
            if(Mathf.Abs(ballpos.transform.localPosition.z)<5f&&ballpos.transform.localPosition.x>7f){
                restart = new Vector3(10f, 0.6f, ballpos.transform.localPosition.z);
            }else if(Mathf.Abs(ballpos.transform.localPosition.z) < 5f && ballpos.transform.localPosition.x < -7f){
                restart = new Vector3(-10f, 0.6f, ballpos.transform.localPosition.z);
            }else if(ballpos.transform.localPosition.z>5f){
                var theta = Mathf.Atan2(ballpos.transform.localPosition.z - 5f, ballpos.transform.localPosition.x);
                restart = new Vector3(Mathf.Cos(theta) * 10f, 0.6f, 5f + Mathf.Sin(theta) * 10f);
            }else if(ballpos.transform.localPosition.z<-5f){
                var theta = Mathf.Atan2(ballpos.transform.localPosition.z + 5f, ballpos.transform.localPosition.x);
                restart = new Vector3(Mathf.Cos(theta) * 10f, 0.6f, -5f + Mathf.Sin(theta) * 10f);
            }
            restartflag = true;
            dur = 3f;
        }
        if(restartflag){
            dur -= Time.deltaTime;
            if (dur < 0f){
                ball.GetComponent<Rigidbody>().isKinematic = true;
                ball.transform.position = restart;
                ball.GetComponent<Rigidbody>().isKinematic = false;
                restartflag = false;
            }
        }

        if(ballpos.transform.localPosition.z>8.5f){
            check1 = true;
        }
        if(ballpos.transform.localPosition.z<-8.5f&&check1){
            check2 = true;
        }
        if(Mathf.Abs(ballpos.transform.localPosition.z)<0.2f&&ballpos.transform.localPosition.x>7f&&!restartflag&&check1&&check2){
            goalflag = true;
        }

        if (!(Input.gyro.enabled))
        {
            ball.transform.position = new Vector3(10f, 0.51f, 0f);
        }
        if(startflag){
            duration -= Time.deltaTime;
            if(duration<-1f){
                signal.gameObject.SetActive(false);
                startflag = false;
            }else if(duration<0f){
                signal.text = "Start";
                ball.GetComponent<Rigidbody>().isKinematic = false;
                timetex.gameObject.SetActive(true);
                timeflag = true;
            }else if(duration<1f){
                signal.text = "1";
            }else if(duration<2f){
                signal.text = "2";
            }
        }
        if(timeflag){
            time += Time.deltaTime;
            timetex.text = ((int)time/60).ToString("00")+":"+(time%60).ToString("00.00");
        }
        if(goalflag){
            signal.text = "Goal!";
            signal.gameObject.SetActive(true);
            timeflag = false;
            restartbtn.gameObject.SetActive(true);
        }
    }

    public void StartClick(){
        start.gameObject.SetActive(false);
        Input.gyro.enabled = true;
        signal.gameObject.SetActive(true);
        startflag = true;
    }
    public void RestartClick(){
        SceneManager.LoadScene("course1");
    }
}
