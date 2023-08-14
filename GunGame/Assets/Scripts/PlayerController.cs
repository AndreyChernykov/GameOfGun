using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    string mouseX = "Mouse X";
    string mouseY = "Mouse Y";
    

    void Start()
    {
        
    }

    void Update()
    {
        if(Time.timeScale > 0)
        {
            Rotate();
            Shoot();
        }

    }

    private void Rotate()
    {
        playerManager.Rotation(Input.GetAxis(mouseY), Input.GetAxis(mouseX));

    }

    private void Shoot()
    {
        if(Input.GetMouseButtonDown(0)) playerManager.Shoot();
    }
}
