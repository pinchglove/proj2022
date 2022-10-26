using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Note : MonoBehaviour
{
    Sync sync;
    Transform destoryPos;
    Vector3 desPos;

    UIManager_BeatPinch uiManager;

    public GameObject Box;
    GameObject explosion;
    ParticleSystem particle;
    AudioSource effectsound;
    bool effectsoundPlayedYet;

    float speed;
    public int score;

    void Start()
    {
        destoryPos = GameObject.Find("DestroyNote").GetComponent<Transform>();
        desPos = destoryPos.transform.position;
        effectsoundPlayedYet = true;
        
        uiManager = GameObject.Find("UIManager_BeatPinch").GetComponent<UIManager_BeatPinch>();
        sync = GameObject.Find("Sync").GetComponent<Sync>();
        speed = sync.scrollSpeed * sync.userSpeedRate;
        Box = transform.Find("Box").gameObject;
        Box.SetActive(false);
        explosion = transform.GetChild(1).gameObject;
        particle = explosion.transform.GetChild(0).GetComponent<ParticleSystem>();
        explosion.SetActive(false);
        effectsound = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        StartCoroutine(MoveNote());
    }

    IEnumerator MoveNote()
    {   
        if (transform.position.z > desPos.z)
        {
            transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
        yield return null;
    }

    public void ChangeNoteSpeed(int key)
    {
        if(key.Equals(0))
        {
            transform.position = new Vector3(transform.position.x , transform.position.y * 1.1f);
            speed *= 1.1f;
            Mathf.Floor(speed);
        }
        else if(key.Equals(1))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y / 1.1f);
            speed /= 1.1f;
            Mathf.Floor(speed);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "StartPos")
        {
            Box.SetActive(true);
        }
        if (other.tag == "Sensor")
        {
            Box.SetActive(false);
            StartCoroutine(HitNote());
        }
        if (other.tag == "Grabber" && SelectFinger.GetInputData() > 20)
        {
            Box.SetActive(false);
            StartCoroutine(HitNote());
        }
    }

    IEnumerator HitNote()
    {
        //이펙트 터지게
        explosion.SetActive(true);
        particle.Play();
        if (effectsoundPlayedYet)
        {
            Data.instance.noteCount += 1;
            uiManager.HitCount();
            effectsound.Play();
            effectsoundPlayedYet = false;
        }
        speed = 0;
        yield return new WaitForSeconds(0.7f);
        this.gameObject.SetActive(false);
        yield return null;
    }
    // 싱크 확인용 메소드 -> Sync.cs
    public void RotateNote()
    { 
         transform.Rotate(Vector3.back * 45f);
    }
}
