using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixeltron.Utils
{
    [RequireComponent(typeof(Rigidbody))]
    public class PTControl3D : MonoBehaviour
    {
        public float movementSpeed = 2.0f;
        public float rotationSpeed = 2.0f;
        public bool constrainPhysics = true;
        protected Rigidbody rb;
        protected bool controlsEnabled = true;
        private Vector3 velocity = Vector3.zero;
        private Vector3 rotation = Vector3.zero;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            ConstrainPhysics(constrainPhysics);
        }
        
        public virtual void ProcessInput(PTInputFrame input)
        {
            if (controlsEnabled)
            {
                PTInputFrame inputFrame = input;
                Vector2 move = inputFrame.moveAxes * movementSpeed;
                Move(transform.forward * move.y + transform.right * move.x);
                Rotate(new Vector3(0, inputFrame.rotation, 0) * rotationSpeed);

                PerformRotation();
                PerformMovement();
            }
        }

        //Allows for external scripts to move us
        public void Move(Vector3 vel)
        {
            velocity = vel;
        }

        //Allows for external scripts to rotate us
        public void Rotate(Vector3 rot)
        {
            rotation = rot;
        }

        protected virtual void PerformRotation()
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation) );
        }

        protected virtual void PerformMovement()
        {
            if (velocity != Vector3.zero)
            {
                //Debug.Log(velocity);
                rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
                //rb.velocity += velocity * Time.fixedDeltaTime;
                
            }   
        }

        protected virtual void ConstrainPhysics(bool constrain = true)
        {
            if (constrain)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotationZ |
                                RigidbodyConstraints.FreezeRotationY |
                                RigidbodyConstraints.FreezeRotationX;
            }
            else
            {
                rb.constraints = RigidbodyConstraints.None;
            }
        }
    }
}