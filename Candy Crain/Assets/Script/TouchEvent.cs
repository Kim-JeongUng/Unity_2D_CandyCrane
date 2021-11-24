﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class TouchEvent : MonoBehaviour
{

    public AudioSource ClickSound;
    public Canvas canvas;
    GraphicRaycaster gr;
    PointerEventData ped;
    RawImage img;

    Game GameManager;

    void Start()
    {
        gr = canvas.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);
        GameManager = this.GetComponent<Game>();
    }

    void Update()
    {
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);


        if (results.Count > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!results[0].gameObject.CompareTag("Bomb"))
                {
                    ClickSound.Play();
                }
                img = (RawImage)results[0].gameObject.GetComponent<RawImage>();

                // 캔디 삭제
                if (results[0].gameObject.CompareTag("Candies") && Game.SlotChild < Game.MaxSlot)
                {
                    // 클릭된 칼럼의 상단에 있는 캔디 제거
                    GameManager.SlotSpawn(results[0].gameObject.transform.parent.GetChild(results[0].gameObject.transform.parent.childCount - 1).gameObject);
                }
                if (results[0].gameObject.CompareTag("Bomb"))
                {
                    GameManager.Bomb();
                }
                if (results[0].gameObject.name == "Reload")
                {
                    SceneManager.LoadScene("GameScene");
                }
                if (results[0].gameObject.name == "GoMenu")
                {
                    SceneManager.LoadScene("LobyScene");
                }

            }
        }

    }
}
