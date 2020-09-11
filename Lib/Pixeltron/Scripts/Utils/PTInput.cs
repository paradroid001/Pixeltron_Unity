using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Input using the new movement system

namespace Pixeltron.Utils
{

    public class PTInputFrame
    {
        public Vector2 moveAxes;
        public Vector2 mousePos;   
        public float rotation;
        public float scrollDelta;  
        public Vector2 dragDeltaLeft;
        public Vector2 dragDeltaRight;
        public Vector2 mouseDelta;
        public bool mouseDownLeft = false;
        public bool mouseDownRight = false;
    }


    public class PTInput : MonoBehaviour
    {
        public bool lockCursorOnStart = true;
        public float dragDeadspot = 0.05f;
        PTInputFrame inputFrame;
        
        Vector2 prevMousePos;

        protected virtual void Awake()
        {
            inputFrame = new PTInputFrame();
        }

        protected virtual void Start()
        {
            if (lockCursorOnStart)
            {
                LockCursor(lockCursorOnStart);
            }
        }


        public PTInputFrame input
        {
            get {return inputFrame;}
        }

        public void Update()
        {
            GetInputThisFrame();
        }

        //You'd override this if you wanted to detect input differently.
        protected virtual void GetInputThisFrame()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            input.moveAxes = new Vector2(h, v);
            
            //
            //MOUSE
            //
            input.scrollDelta = Input.mouseScrollDelta.y;
            //mouse delta is current pos - previous pos
            Vector2 mousepos = new Vector2( Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) ;
            input.mouseDelta = mousepos - input.mousePos;
            input.mousePos = mousepos;
            
            if ( Input.GetMouseButton(0) )
            {
                if (input.mouseDownLeft)
                {
                    //if the mouse is still down, it's a drag.
                    input.dragDeltaLeft = input.mouseDelta;
                    //Debug.Log("Left drag: " + input.dragDeltaLeft);
                }
                else
                {
                    input.mouseDownLeft = true;
                }
            }
            else
            {
                input.mouseDownLeft = false;
                input.dragDeltaLeft = Vector2.zero;
            }
            
            if ( Input.GetMouseButton(1) )
            {
                if (input.mouseDownRight)
                {
                    //if the mouse is still down, it's a drag.
                    input.dragDeltaRight = input.mouseDelta;
                    //Debug.Log("Right drag: " + input.dragDeltaRight);
                }
                else
                {
                    input.mouseDownRight = true;
                }
            }
            else
            {
                input.mouseDownRight = false;
                input.dragDeltaRight = Vector2.zero;
            }


            //
            //ROTATION
            //
            input.rotation = 0;
            if (Input.GetKey(KeyCode.Q) || (input.dragDeltaRight.x < -dragDeadspot) )
            {
                input.rotation -=1;
            }
            if (Input.GetKey(KeyCode.E) || (input.dragDeltaRight.x > dragDeadspot) )
            {
                input.rotation +=1;
            }

            //
            // MISC
            //
            if (Input.GetKey(KeyCode.Escape))
            {
                LockCursor(false);
            }
        }

        /*

        void OnMove(InputValue value)
        {
            inputFrame.moveAxes = value.Get<Vector2>();
        }

        void OnLook(InputValue value)
        {
            inputFrame.mousePos = value.Get<Vector2>();
        }

        void OnCamScroll(InputValue value)
        {
            inputFrame.scrollDelta = value.Get<Vector2>().y;
            //Debug.Log(inputFrame.scrollDelta);
        }

        void OnRotate(InputValue value)
        {
            inputFrame.rotation = value.Get<float>();
        }
        */

        public void LockCursor(bool shouldLock = true)
        {
            if (shouldLock)
            {
                Cursor.lockState = CursorLockMode.Locked;
                //Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}