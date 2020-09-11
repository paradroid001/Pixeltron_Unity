using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixeltron.Utils
{
    public class PTCharacter3D : MonoBehaviour
    {
        PTInput input;
        PTCameraTP cameraTP;
        PTControl3D control3D;

        void Awake()
        {
            input = GetComponent<PTInput>();
            cameraTP = GetComponent<PTCameraTP>();
            control3D = GetComponent<PTControl3D>();
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        protected virtual void FixedUpdate()
        {
            PTInputFrame inputFrame = input.input;
            float rdy = inputFrame.dragDeltaRight.y;
            float ldx = inputFrame.dragDeltaLeft.x;
            float ldy = inputFrame.dragDeltaLeft.y;

            //hide cursor if dragging
            if (inputFrame.mouseDownLeft|| inputFrame.mouseDownRight)
            {
                input.LockCursor(true);
            }
            else
            {
                input.LockCursor(false);
            }
 
            cameraTP.MoveCam(new Vector2(ldx, Mathf.Max(rdy, ldy)));
            cameraTP.ChangeZoom(inputFrame.scrollDelta);
            control3D.ProcessInput(inputFrame);
        }
    }
}