using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nihon
{
    public class Master_BlockRotation : MonoBehaviour
    {
        private Transform myTransform;
        private float puzzleRotation = 180f;
        private bool isRunning;
        private GameObject block;

        public float rotationTime;
        public float rayLength;
        public LayerMask layerMask;

        void Update()
        {
            // Checks if mouse clicks on an object
            // If true, check if raycast hits object of layer 9/block and rotate object

            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && !isRunning)
            {
                /*hit and Ray are used to select gameobject from camera perspective
                 If object is equal to a block layer it will then go into the next if statement.*/
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, rayLength, layerMask) && hit.collider.gameObject.layer == 9)
                {
                    /*This blockRay & BlockHit is a second ray/raycast that checks to see if the origional block is surrounded,
                     if the block contains blocks on the top, left and right of it, then the block will not be able to be used.
                     If block has an entry point {Orgional Block > Block on left > No Block on Right > No block on top > Block can be used.*/
                    Ray topBlockRay = new Ray(hit.collider.gameObject.transform.position, Vector3.up);
                    Ray leftBlockRay = new Ray(hit.collider.gameObject.transform.position, Vector3.left);
                    Ray rightBlockRay = new Ray(hit.collider.gameObject.transform.position, Vector3.right);
                    RaycastHit blockHit;

                    /*CHECKS IF NO BLOCK ON TOP, NO BLOCK ON LEFT BUT BLOCK ON RIGHT > IF TRUE PROCEED*/
                    if (!Physics.Raycast(topBlockRay, out blockHit, rayLength, layerMask) && !Physics.Raycast(leftBlockRay, out blockHit, rayLength, layerMask) && Physics.Raycast(rightBlockRay, out blockHit, rayLength, layerMask))
                    {
                        block = hit.collider.gameObject;
                        myTransform = block.GetComponent<Transform>();
                        StartCoroutine(CoroutineTest(this.rotationTime)); //You can start coroutines like this too, which is faster and safer
                    }
                    /*CHECKS IF NO BLOCK ON TOP, NO BLOCK ON RIGHT BUT BLOCK ON LEFT > IF TRUE PROCEED*/
                    else if (!Physics.Raycast(topBlockRay, out blockHit, rayLength, layerMask) && !Physics.Raycast(rightBlockRay, out blockHit, rayLength, layerMask) && Physics.Raycast(leftBlockRay, out blockHit, rayLength, layerMask))
                    {
                        block = hit.collider.gameObject;
                        myTransform = block.GetComponent<Transform>();
                        StartCoroutine(CoroutineTest(this.rotationTime)); //You can start coroutines like this too, which is faster and safer
                    }
                    /*CHECKS IF NO BLOCK ON TOP, NO BLOCK ON RIGHT & NO BLOCK ON LEFT > IF TRUE PROCEED*/
                    else if (!Physics.Raycast(topBlockRay, out blockHit, rayLength, layerMask) && !Physics.Raycast(leftBlockRay, out blockHit, rayLength, layerMask) && !Physics.Raycast(rightBlockRay, out blockHit, rayLength, layerMask))
                    {
                        block = hit.collider.gameObject;
                        myTransform = block.GetComponent<Transform>();
                        StartCoroutine(CoroutineTest(this.rotationTime)); //You can start coroutines like this too, which is faster and safer
                    }
                }
            }
        }

        IEnumerator CoroutineTest(float rotationTime)
        {
            isRunning = true;

            //Store these here. Now we can use Slerp as designed
            Quaternion initialRot = myTransform.rotation;
            Quaternion finalRot = Quaternion.Euler(0.0f, 0.0f, puzzleRotation);

            //i is our progress through the rotation, 0 = 0%, 1 = 100%
            //The increment ensures it will take 'rotationTime' seconds to reach 100%
            for (float i = 0; i < 1.0f; i += Time.deltaTime / rotationTime)
            {
                myTransform.rotation = Quaternion.Slerp(initialRot, finalRot, i);
                yield return null; //Wait one frame, then continue
            }

            //Make sure we're definitely at the final rotation.
            //The for loop may complete just before the final rotation
            myTransform.rotation = finalRot;
            isRunning = false;
        }
    }
}