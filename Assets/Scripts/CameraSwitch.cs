using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CameraSwitch : MonoBehaviour
{
    //[SerializeField] private CinemachineCamera launcherCam;
    [SerializeField] private CinemachineCamera pieceCam;
    InputAction camSwitch;
    [SerializeField] private bool isSwitched = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //should change how this finds the cameras if we decide to have objects persist per scene
        //launcherCam = GameObject.Find("Launcher Cam").GetComponent<CinemachineCamera>();
        pieceCam = GameObject.Find("Piece Cam").GetComponent<CinemachineCamera>();
        camSwitch = InputSystem.actions.FindAction("Camera Switch");
    }

    // Update is called once per frame
    void Update()
    {
        if (camSwitch.WasPerformedThisFrame())
        {
            //update this code to make pieceInPlay actually track the CURRENT launched piece
            //rn it's using the "Player Piece" tag which is an issue since multiple player pieces will be laying on-field
            Transform pieceInPlay = GameObject.FindGameObjectWithTag("Player Piece").GetComponent<Transform>();
            if (pieceInPlay != null && !isSwitched)
            {
                pieceCam.Priority = 20;
                pieceCam.Target.TrackingTarget = pieceInPlay;
                isSwitched = true;
            }
            else
            {
                pieceCam.Priority = 0;
                pieceCam.Target.TrackingTarget = null;
                isSwitched = false;
            }
        }
    }
}
