using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRController : MonoBehaviour
{
    public float mSensitivity = 0.1f;
    public float mMaxSpeed = 1.0f;

    public float mJumpForce = 2.45f;
    public float mGravity = 20.0f;

    public SteamVR_Action_Vector2 mMoveValue = null;
    public SteamVR_Action_Boolean mJumpValue = null;

    private CharacterController mCharacterController = null;
    private Transform mCameraRig = null;
    private Transform mHead = null;

    private Vector3 moveDirection = Vector3.zero;

    public AudioSource mFoot;
    public AudioClip[] footstepSounds;

    private float nextFootstep;
    public float mFootstepDelay;

    private void Awake()
    {
        mCharacterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        mCameraRig = SteamVR_Render.Top().origin;
        mHead = SteamVR_Render.Top().head;
    }
    private void FixedUpdate()
    {
        //HandleHead();
        HandleHeight();
        CalculateMovement();
    }

    private void HandleHead()
    {
        // Store current
        Vector3 oldPosition = mCameraRig.position;
        Quaternion oldRotation = mCameraRig.rotation;

        // Rotation
        transform.eulerAngles = new Vector3(0.0f, mHead.rotation.eulerAngles.y, 0.0f);

        // Restore
        mCameraRig.position = oldPosition;
        mCameraRig.rotation = oldRotation;
    }

    private void CalculateMovement()
    {
        if (mCharacterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            Vector3 dir = Camera.main.transform.TransformDirection(new Vector3(mMoveValue.axis.x, 0, mMoveValue.axis.y));
            moveDirection = Vector3.ProjectOnPlane(dir, Vector3.up);
            moveDirection *= mMaxSpeed;

            if (dir != Vector3.zero)
            {
                nextFootstep -= Time.deltaTime;
                if (nextFootstep <= 0)
                {
                    mFoot.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Length)], 0.2f);
                    nextFootstep += mFootstepDelay;
                }
            }

            if (mJumpValue.stateDown)
            {
                moveDirection.y = mJumpForce;
                moveDirection *= 2.0f;
            }
        }
        float jump = 0f;
        if (mJumpValue.stateDown)
        {
            jump = mJumpForce;
            Debug.Log("Jump: " + jump);
        }

        moveDirection.y -= mGravity * Time.deltaTime;

        // Direction
        Vector3 direction = Camera.main.transform.TransformDirection(new Vector3(mMoveValue.axis.x, 0, mMoveValue.axis.y));

        // Movement
        Vector3 movement = (mMaxSpeed * Time.deltaTime) * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime;

        // Apply movement
        mCharacterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleHeight()
    {
        // Get the head in local space
        float headHeight = Mathf.Clamp(mHead.localPosition.y, 1, 2);
        mCharacterController.height = headHeight;

        // Trim height value in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = mCharacterController.height / 2;
        newCenter.y += mCharacterController.skinWidth;

        // Move capsule in local space
        newCenter.x = mHead.localPosition.x;
        newCenter.z = mHead.localPosition.z;

        // Rotate
        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        // Apply
        mCharacterController.center = newCenter;
    }
}
