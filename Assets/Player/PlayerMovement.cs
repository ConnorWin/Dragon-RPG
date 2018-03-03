using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]
[RequireComponent (typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	ThirdPersonCharacter thirdPersonCharacter;
	// A reference to the ThirdPersonCharacter on the object
	CameraRaycaster cameraRaycaster;
	Vector3 currentDestination, clickPoint;

	[SerializeField] float walkMoveStopRadius = 0.2f;
    [SerializeField] float attackMoveStopRadius = 5f;

    [SerializeField] const int walkableLayerNumber = 8;
    [SerializeField] const int enemyLayerNumber = 9;

    AICharacterControl aiCharacterControl = null;
    GameObject walkTarget = null;
    bool isInDirectMode = false;

	void Start ()
	{
		cameraRaycaster = Camera.main.GetComponent<CameraRaycaster> ();
		thirdPersonCharacter = GetComponent<ThirdPersonCharacter> ();
		currentDestination = transform.position;
        aiCharacterControl = GetComponent<AICharacterControl>();
        walkTarget = new GameObject("walkTarget");

        cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
    }

    void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
        switch (layerHit)
        {
            case enemyLayerNumber:
                //walk to enemy
                GameObject enemy = raycastHit.collider.gameObject;
                aiCharacterControl.SetTarget(enemy.transform);
                break;
            case walkableLayerNumber:
                //navigate to point on ground
                walkTarget.transform.position = raycastHit.point;
                aiCharacterControl.SetTarget(walkTarget.transform);
                break;
            default:
                Debug.LogWarning("don't know how to handle this mouse click for movement");
                return;
        }
    }

    void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }

    void WalkToDestination()
    {
        //var playerToClickPoint = currentDestination - transform.position;
        //if (playerToClickPoint.magnitude >= .11)
        //    thirdPersonCharacter.Move(currentDestination - transform.position, false, false);
        //else
        //    thirdPersonCharacter.Move(Vector3.zero, false, false);
    }
}

