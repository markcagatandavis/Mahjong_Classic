using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nihon
{
    public class Master_BlockManager : MonoBehaviour
    {
        //Used for rotating blocks when selected
        private Transform myTransform;
        private float puzzleRotation = 180f;
        private bool isRunning;
        public float rotationTime;
        
        //Used for detecting and storing blocks on selection
        public float rayLength;
        public LayerMask layerMask;
        private RaycastHit hit;
        private GameObject block;

        //Used for temporary storage of block data and conditional checking
        private GameObject tempFirstSelectedBlock;
        private GameObject tempSecondSelectedBlock;
        private bool firstBlockSelected = false;
        private bool secondBlockSelected = false;
        private Color blockSelectedColor = new Color32(0x3D,0xA8,0x82, 0xff);

        void Update()
        {
            // Checks if mouse clicks on an object; If true, check if raycast hits object of layer(9)block
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !isRunning)
            {
                /*Ray is used to select gameobject from camera perspective; If object is equal to a block layer it proceed to CheckSurroundingBlocks*/
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, rayLength, layerMask) && hit.collider.gameObject.layer == 9)
                {
                    CheckSurroundingBlocks(hit);
                }
            }
        }

        void CheckSurroundingBlocks(RaycastHit hit)
        {
            /*This blockRay & BlockHit is a second ray/raycasthit that checks to see if the original block is surrounded,
                if the block contains blocks on the top, left and right of it, then the block will not be able to be used.
                If block has an entry point {Orgional Block > Block on left > No Block on Right > No block on top > Block can be used.*/
            Ray topBlockRay = new Ray(hit.collider.gameObject.transform.position, Vector3.up);
            Ray leftBlockRay = new Ray(hit.collider.gameObject.transform.position, Vector3.left);
            Ray rightBlockRay = new Ray(hit.collider.gameObject.transform.position, Vector3.right);
            RaycastHit blockHit;

            /*CHECKS IF NO BLOCK ON TOP, NO BLOCK ON LEFT BUT BLOCK ON RIGHT > IF TRUE PROCEED*/
            if (!Physics.Raycast(topBlockRay, out blockHit, rayLength, layerMask) && !Physics.Raycast(leftBlockRay, out blockHit, rayLength, layerMask) && Physics.Raycast(rightBlockRay, out blockHit, rayLength, layerMask))
            {
                if (!firstBlockSelected)
                {
                    SelectFirstBlock();
                }
                else if (firstBlockSelected && !secondBlockSelected)
                {
                    SelectSecondBlock();
                }
            }
            /*CHECKS IF NO BLOCK ON TOP, NO BLOCK ON RIGHT BUT BLOCK ON LEFT > IF TRUE PROCEED*/
            else if (!Physics.Raycast(topBlockRay, out blockHit, rayLength, layerMask) && !Physics.Raycast(rightBlockRay, out blockHit, rayLength, layerMask) && Physics.Raycast(leftBlockRay, out blockHit, rayLength, layerMask))
            {
                if (!firstBlockSelected)
                {
                    SelectFirstBlock();
                }
                else if (firstBlockSelected && !secondBlockSelected)
                {
                    SelectSecondBlock();
                }
            }
            /*CHECKS IF NO BLOCK ON TOP, NO BLOCK ON RIGHT & NO BLOCK ON LEFT > IF TRUE PROCEED*/
            else if (!Physics.Raycast(topBlockRay, out blockHit, rayLength, layerMask) && !Physics.Raycast(leftBlockRay, out blockHit, rayLength, layerMask) && !Physics.Raycast(rightBlockRay, out blockHit, rayLength, layerMask))
            {
                if (!firstBlockSelected)
                {
                    SelectFirstBlock();
                }
                else if (firstBlockSelected && !secondBlockSelected)
                {
                    SelectSecondBlock();
                }
            }
        }

        void SelectFirstBlock()
        {
            //myTransform = block.GetComponent<Transform>();
            //StartCoroutine(CoroutineTest(this.rotationTime)); //You can start coroutines like this too, which is faster and safer
            block = hit.collider.gameObject;
            tempFirstSelectedBlock = hit.collider.gameObject;
            block.GetComponent<Renderer>().material.SetColor("_Color", blockSelectedColor);
            firstBlockSelected = true;
            Debug.Log(tempFirstSelectedBlock);
        }

        void SelectSecondBlock()
        {
            //myTransform = block.GetComponent<Transform>();
            //StartCoroutine(BlockRotationCoroutine(this.rotationTime)); //You can start coroutines like this too, which is faster and safer
            block = hit.collider.gameObject;
            tempSecondSelectedBlock = hit.collider.gameObject;
            block.GetComponent<Renderer>().material.SetColor("_Color", blockSelectedColor);
            secondBlockSelected = true;
            Debug.Log(tempSecondSelectedBlock);
            CheckIfBlocksMatch();
        }

        void CheckIfBlocksMatch()
        {
            Debug.Log("First Block Selected: " + tempFirstSelectedBlock + " and Second Block Selected: " + tempSecondSelectedBlock + "Now in checkIfBlocksMatch function.");
        }

        IEnumerator BlockRotationCoroutine(float rotationTime)
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