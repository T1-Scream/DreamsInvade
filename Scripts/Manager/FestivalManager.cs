using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FestivalManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private MissionLantern[] lantern;
    [SerializeField] private Animator missionDoorAnimator;
    [SerializeField] private GameObject tipsMessage;
    [SerializeField] private GameObject tipsImageParent;

    private PlayerMovement movement;
    private float countDownTimer;
    private bool doorIsOpened = false;
    private bool showTips;
    private bool released;
    private bool activeTrue;
    private bool activeFalse;
    private GameManager gameManager;

    private void Awake()
    {
        movement = player.GetComponent<PlayerMovement>();
        movement.Speed = 2f;
        movement.canJump = true;
        movement.canCrouch = true;
        movement.jumpForce = 3.5f;
        showTips = false;
        released = false;
        gameManager = FindObjectOfType<GameManager>();
    }

    private bool JumpToLanternComplete()
    {
        for (int i = 0; i < lantern.Length; i++) {
            if (lantern[i].clear == false)
                return false;
        }

        return true;
    }

    private void Update()
    {
        if (JumpToLanternComplete() && !doorIsOpened) {
            FindObjectOfType<AudioManager>().Play("AutoDoor", 0f);
            missionDoorAnimator.Play("DoorOpen", 0, 0.0f);
            doorIsOpened = true;
        }

        if (released) return;

        countDownTimer = gameManager.countDownTime;

        if (countDownTimer <= 360f && !activeTrue) {
            tipsMessage.SetActive(true);
            activeTrue = true;
        }

        if (countDownTimer <= 350f && !activeFalse) {
            tipsMessage.SetActive(false);
            activeFalse = true;
        }

        if (countDownTimer <= 300f && !showTips) {
            tipsImageParent.SetActive(true);
            showTips = true;
            released = true;
        }
    }
}
