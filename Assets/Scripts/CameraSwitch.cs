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
        //removing this and assigning in inspector of the Core Game Objects prefab to lessen the amount of Find we're doing
        //pieceCam = GameObject.Find("Piece Cam").GetComponent<CinemachineCamera>();
        
        camSwitch = InputSystem.actions.FindAction("Camera Switch");
    }

    // Update is called once per frame
    void Update()
    {
        if (camSwitch.WasPerformedThisFrame() && PlayerController.doneIntro)
        {
            //update this code to make pieceInPlay actually track the CURRENT launched piece
            //rn it's using the "Player Piece" tag which is an issue since multiple player pieces will be laying on-field
            
            //how it was done before:
            //Transform pieceInPlay = GameObject.FindGameObjectWithTag("Player Piece").GetComponent<Transform>();
            
            //testing now with array of gameobjects with Player Piece tag, and selecting the last one (which is array length -1)
            GameObject[] piecesInPlay = GameObject.FindGameObjectsWithTag("Player Piece");
            Transform pieceInPlay = piecesInPlay[piecesInPlay.Length - 1].transform;
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
