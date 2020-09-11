using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixeltron.Utils
{
    public class PTCameraTP : MonoBehaviour
    {
        public Camera tpCam;
        public Transform camFocus;
        public Transform camPosRoot;
        public Transform camPosition;
        [Range(0.0f, 1.0f)]
        public float camMoveSpeed;
        public float camMinDist = 1.0f;
        public float camMaxDist = 30.0f;
        public float camZoomSpeed = 0.5f;
        public float camRotSpeed; //???
        public LayerMask camObstructionLayers;

        private float occludedDist;
        private float zoomDist;

        // Start is called before the first frame update
        void Start()
        {
            zoomDist = -2.0f; //(camMaxDist - camMinDist) / 2.0f;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            CheckForObstruction();
            Zoom();
            MoveCamTowardsTarget();
            tpCam.transform.LookAt(transform.position);
            camPosition.transform.LookAt(transform.position);
        }


        void MoveCamTowardsTarget()
        {
            float mvspd = camMoveSpeed;
            if (occludedDist > zoomDist)
                mvspd = 1.0f;
            if (camPosition != null && tpCam != null && camFocus != null)
            {
                tpCam.transform.position = Vector3.Lerp(tpCam.transform.position, camPosition.position, mvspd);
                
            }
        }

        public void ChangeZoom(float delta)
        {
            zoomDist += delta * camZoomSpeed;
            zoomDist = Mathf.Clamp(zoomDist, -camMaxDist, -camMinDist);
            //Zoom(zoomDist);
            //Debug.Log(zoomDist);
        }

        public void MoveCam(Vector2 moveAxis)
        {
            //Vector3 moveVector = Vector3.up * moveAxis.y + Vector3.right * moveAxis.x;
            //camPosition.transform.RotateAround(transform.position, Vector3.up, moveAxis.x * Time.deltaTime);
            //camPosition.transform.RotateAround(transform.position, transform.right, moveAxis.y * Time.deltaTime);
            camPosRoot.Rotate(moveAxis.y, moveAxis.x, 0.0f, Space.Self);
            //if (camPosRoot.localRotation.x > )
            
        }

        //Note, distances are measured from behind the character, so they are negative.
        public void Zoom()
        { 
            //Debug.Log(val);
            //The clamp here is dealing with -ve numbers, 
            //so it's actually between (-Max, -Min) 
            //instead of (min, max) 
            float newdist = Mathf.Max(occludedDist, zoomDist); // Mathf.Clamp(val, -camMaxDist, -camMinDist);
            Vector3 cp = camPosition.localPosition;
            camPosition.localPosition = new Vector3(cp.x, cp.y, newdist);
        }

        void CheckForObstruction()
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, camPosition.position-transform.position, 
                                out hitInfo, camMaxDist, camObstructionLayers) )
            {
                //Vector3 point = hitInfo.
                //Vector3 cp = camPosition.position;
                //camPosition.position = new Vector3(cp.x, cp.y, hitInfo.distance);
                occludedDist = -hitInfo.distance;
            }
            else
            {
                occludedDist = -camMaxDist;
            }
        }
    }
}