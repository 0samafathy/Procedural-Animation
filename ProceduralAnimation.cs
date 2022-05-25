using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralAnimation : MonoBehaviour
{

    #region Variables

    Animator animator;

    public Transform Player;

    public LayerMask Ground;
    public float feetToGround;
    public float kneeToGround;
    public float legToGround;
    public float hipsToGround;
    public float ikWeight;

    //public Transform leftIKTarget;
    //public Transform rightIKTarget;

    [Header("Left Foot")]
    public Vector3 leftFootPosition;
    [Range(-30, 30)] public Quaternion leftFootRotation;
    public Transform leftFoot;
    public float leftFootWeight;

    [Header("Right Foot")]
    public Vector3 rightFootPosition;
    [Range(-30, 30)] public Quaternion rightFootRotation;
    public Transform rightFoot;
    public float rightFootWeight;

    [Header("Left Knee")]
    public Vector3 leftKneePosition;
    [Range(0, 90)] public Quaternion leftKneeRotation;
    [Range(0, 90)] public float leftKneeRotationY;
    public Transform leftKnee;
    public float leftKneeWeight;

    [Header("Right Knee")]
    public Vector3 rightKneePosition;
    [Range(0,90)] public Quaternion rightKneeRotation;
    [Range(0,90)] public float rightKneeRotationY;
    public float rightKneeweight;
    public Transform rightKnee;

    [Header("Left Leg")]
    public Vector3 leftLegPosition;
    [Range(-90, 60)] public Quaternion leftLegRotation;
    [Range(-90, 45)] public float leftLegRotationY;
    [Range(0, 60)] public float leftLegRotaionZ;
    public float leftLegWeight;
    public Transform leftLeg;

    [Header("Right Leg")]
    public Vector3 rightLegPosition;
    [Range(-90, 45)] public Quaternion rightLegRotation;
    public float rightLegWeight;
    public Transform rightLeg;

    [Header("Hips")]
    public Vector3 hipsPosition;
    [Range(-30, 30)] public Quaternion hipsRotation;
    [Range(-30, 30)] public float hipsRotationX;
    [Range(-1, 1)] public float hipsRotationZ;
    public float hipsWeight;
    public Transform hips;

    public float offsetY;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        //left Foot
        leftFootPosition = leftFoot.position;
        leftFootRotation = leftFoot.rotation;
        leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
   
        //right Foot
        rightFootPosition = rightFoot.position;       
        rightFootRotation = rightFoot.rotation;
        rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);

        //left knee
        leftKneePosition = leftKnee.position;
        leftKneeRotation = leftKnee.rotation;
        leftKnee = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);

        //right knee
        rightKneePosition = rightKnee.position;
        rightKneeRotation = rightKnee.rotation;
        rightKnee = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);

        leftLegPosition = leftLeg.position;
        leftLegRotation = leftLeg.rotation;
        leftLeg = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);

        rightLegPosition = rightLeg.position;
        rightLegRotation = rightLeg.rotation;
        rightLeg = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);

        hipsPosition = hips.position;
        hipsRotation = hips.rotation;
        hips = animator.GetBoneTransform(HumanBodyBones.Hips);
    }

    // Update is called once per frame
    void Update()
    {
        LeftFoot();
        RightFoot();
        LeftKnee();
        RightKnee();
        LeftLeg();
        RightLeg();
        //Hips();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootWeight);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootWeight);

        animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPosition + new Vector3(0, offsetY, 0));
        animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPosition + new Vector3(0, offsetY, 0));
    }

    //left method
    void LeftFoot()
    {
        RaycastHit leftHit;
        //setting the position and rotation of left foot to the ground
        Vector3 lPos = leftFoot.position;
        if (Physics.Raycast(lPos, transform.TransformDirection(-Vector3.up), out leftHit, Ground))
        {
            leftFootPosition.y = leftHit.point.y;
            lPos.y = leftFootPosition.y;
            leftFootRotation = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
            leftFoot.rotation = leftFootRotation;
            Debug.DrawLine(leftFoot.position, leftHit.point, Color.red);
        }
    }

    
    void RightFoot()
    {
        RaycastHit rightHit;

        //setting the position and rotation of right foot to the ground
        Vector3 rPos = rightFoot.position;
        if (Physics.Raycast(rPos, transform.TransformDirection(-Vector3.up), out rightHit, Ground))
        {
            rightFootPosition = rightHit.point;
            rPos.y = rightFootPosition.y;
            rightFootRotation = Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation;
            rightFoot.rotation = rightFootRotation;
            Debug.DrawLine(rightFoot.position, rightHit.point, Color.red);
        }
    }

    void LeftKnee()
    {
        RaycastHit LefttKneeHit;

        Vector3 lKPos = leftKnee.position;

        kneeToGround = feetToGround + 0.5f;

        if (Physics.Raycast(lKPos, transform.TransformDirection(-Vector3.up),out LefttKneeHit, Ground))
        {
            //lKPos.y = LefttKneeHit.point.y + kneeToGround;
            //leftFootPosition.y = lKPos.y;
            leftFootRotation = Quaternion.FromToRotation(transform.up, new Vector3(0, LefttKneeHit.point.y, 0)) * transform.rotation;
            leftKnee.rotation = leftFootRotation;
        }
    }

    void RightKnee()
    {
        RaycastHit RighttKneeHit;

        
        Vector3 rKPos = rightKnee.position;
        kneeToGround = feetToGround + 0.5f;
        if (Physics.Raycast(rKPos, transform.TransformDirection(-Vector3.up), out RighttKneeHit, Ground))
        {
            //rKPos.y = RighttKneeHit.point.y + kneeToGround;
            //rightKneePosition.y = rKPos.y;
            rightKneeRotation = Quaternion.FromToRotation(transform.up, new Vector3(0, RighttKneeHit.point.y, 0)) * transform.rotation;
            rightKnee.rotation = rightKneeRotation;
        }
    }

    void LeftLeg()
    {
        RaycastHit leftLegHit;

        Vector3 lLegPos = leftLeg.position;
        legToGround = feetToGround + 0.97f;
        if (Physics.Raycast(lLegPos, transform.TransformDirection(-Vector3.up), out leftLegHit, Ground))
        {
            //leftLegPosition.y = leftLegHit.point.y + feetToGround;
            //lLegPos.y += leftLegPosition.y;
            leftLegRotation = Quaternion.FromToRotation(transform.up, new Vector3(0, leftLegHit.normal.x, leftLegHit.normal.x)) * transform.rotation;
            leftLeg.rotation = leftLegRotation;
            Debug.DrawLine(leftLeg.position, leftLegHit.point, Color.cyan);
        }
    }
    void RightLeg()
    {
        RaycastHit rightLegHit;

        Vector3 rLegPos = rightLeg.position;
        legToGround = feetToGround + 0.97f;
        if (Physics.Raycast(rLegPos, transform.TransformDirection(-Vector3.up), out rightLegHit, Ground))
        {
            //rLegPos.y = rightLegHit.point.y + legToGround;
            //leftLegPosition.y += rLegPos.y;
            rightLegRotation = Quaternion.FromToRotation(transform.up, new Vector3(0, rightLegHit.normal.x, rightLegHit.normal.x)) * transform.rotation;
            Debug.Log("right leg rotation: " + rightLegRotation);
            rightLeg.rotation = rightLegRotation;
        }
    }

    void Hips()
    {
        RaycastHit hipsHit;
        Vector3 hipsPos = leftLeg.position;
        hipsToGround = feetToGround + 1.036f;
        if (Physics.Raycast(hipsPos, transform.TransformDirection(-Vector3.up), out hipsHit, Ground))
        {
            //hipsPos = hipsHit.point;
            //hipsPosition.y = hipsPos.y;
            hipsRotationX = hipsHit.point.x;
            hipsRotationZ = hipsHit.point.z;

            hipsRotation = Quaternion.FromToRotation(transform.up, new Vector3(hipsRotationZ, 0, 0)) * transform.rotation;
            hips.rotation = hipsRotation;
        }
    }
}
