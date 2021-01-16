using MLAgents;
using KartGame.KartSystems;
using UnityEngine;

    [System.Serializable]
    //Sensor raycast & HitThreshold for potential crash
    public struct Sensor
    {
        public Transform transform;
        public float HitThreshold;
    }

    //Agent Mode Training (Learning) & Inferencing (Doing ` action based on the agent have been learn)
    public enum AgentMode
    {
        Training,
        Inferencing
    }

    public class KartingAgent : Agent, IInput
    {
        #region training
        public AgentMode mode = AgentMode.Training;
        #endregion

        #region sensor
        //raycast distance
        public float raycastDistance;
        //layer mask, what layer can be detected by sensor raycast
        public LayerMask layerMask;
        //Sensor used to detect environment/provide information
        public Sensor[] sensor;
        //Bunch of collider for checkpoint, so NPC can determine where to go next
        public Collider[] checkpointColliders;
        //Layer mask for checkpoint
        public LayerMask checkpointMask;
        #endregion

        #region reward configuration
        //penalty given if NPC crash
        public float hitPenalty = -1f;
        //reward given if NPC pass the checkpoint
        public float passCheckpointReward;
        //reward given if NPC move in the right direction
        public float movingInRightDirectionReward;
        //reward for NPC that move fast
        public float speedReward;
        #endregion

        #region show raycast
        public bool showRaycasts;
        #endregion

        const int localActionSize = 2;
        float[] localActions;

        Vector3 startingPosition;

        Quaternion startingRotation;
        ArcadeKart kart;
        float acceleration;
        float steering;
        int checkpointIndex;

        void Awake()
        {
            kart = GetComponent<ArcadeKart>();
            localActions = new float[localActionSize];
            startingPosition = this.transform.position;
            startingRotation = this.transform.rotation;
        }

        void Start()
        {
            AgentReset();
        }

        void OnTriggerEnter(Collider other)
        {
            //gameobject.layer need to be left shift (<<) into bit so they can be compared to the real layer mask.
            //to compare one of them are match use &
            int maskedValue = 1 << other.gameObject.layer;

            int triggered = maskedValue & checkpointMask;

            FindCheckpointIndex(other, out int index);

            // condition to check NPC pass the checkpoint and the next index is greater than the last one NPC passed 
            if (triggered > 0 && index > checkpointIndex || index == 0 && checkpointIndex == checkpointColliders.Length - 1)
            {
                AddReward(passCheckpointReward);
                checkpointIndex = index;
            }
        }

        void FindCheckpointIndex(Collider checkPoint, out int index)
        {
            for (int i = 0; i < checkpointColliders.Length; i++)
            {
                if (checkpointColliders[i].GetInstanceID() ==
                checkPoint.GetInstanceID())
                {
                    index = i;
                    return;
                }
            }
            index = -1;
        }

        //generate input 
        public Vector2 GenerateInput()
        {
            return new Vector2(steering, acceleration);
        }

        float FindNextCheckpointDir()
        {
            if (checkpointColliders.Length != 0)
            {
                var nextCheckpoint = (checkpointIndex + 1) % checkpointColliders.Length;
                var nextCheckpointColliders = checkpointColliders[nextCheckpoint];
                var dir = (nextCheckpointColliders.transform.position - kart.transform.position).normalized;
                var resultDir = Vector3.Dot(kart.Rigidbody.velocity.normalized, dir);
                return resultDir;
            }
            else { return 0; }
        }

        public override void CollectObservations()
        {
            //Add speed of kart into vector observations
            AddVectorObs(kart.LocalSpeed());
            AddVectorObs(FindNextCheckpointDir());

            //iterate for all the raycast sensor and add the hitdistance into vector observations
            for (int i = 0; i < sensor.Length; i++)
            {
                var current = sensor[i];
                var currentTransform = current.transform;
                var hit = Physics.Raycast(transform.position, currentTransform.forward, out var hitInfo,
                raycastDistance, layerMask, QueryTriggerInteraction.Ignore);

                var hitDistance = (hit ? hitInfo.distance : raycastDistance) / raycastDistance;
                AddVectorObs(hitDistance);

                if (hitDistance < current.HitThreshold)
                {
                    AddReward(hitPenalty);
                    Done();
                    AgentReset();
                }

                if (showRaycasts)
                {
                    Debug.DrawRay(transform.position, currentTransform.forward * raycastDistance, Color.Lerp(Color.red, Color.green, hitDistance));
                    Debug.DrawRay(transform.position, currentTransform.forward * raycastDistance * current.HitThreshold, Color.red);
                }
            }
        }

        public override void AgentAction(float[] vectorAction)
        {
            steering = vectorAction[0];
            acceleration = vectorAction[1];

            AddReward(kart.LocalSpeed() * speedReward);
            float rightDir = FindNextCheckpointDir();
            AddReward(rightDir * movingInRightDirectionReward);
        }

        public override void AgentReset()
        {
            switch (mode)
            {
                case AgentMode.Training:
                    checkpointIndex = 0;
                    transform.localRotation = startingRotation;
                    transform.position = startingPosition;
                    kart.Rigidbody.velocity = default;
                    acceleration = 0f;
                    steering = 0f;
                    break;
                default:
                    break;
            }
        }

        //dont forget to change behavior type into heuristic if you want to control manually
        public override float[] Heuristic()
        {
            localActions[0] = Input.GetAxis("Horizontal");
            localActions[1] = Input.GetAxis("Vertical");

            return localActions;
        }
    }