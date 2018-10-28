using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nihon
{
    public class MouseHover_ChangeColor : MonoBehaviour
    {
        /*THIS SCRIPT IS RESPONSIBLE FOR CHANGING THE COLOR OF ATTACHED OBJECT WHEN MOUSE IS HOVERED AND BACK TO DEFAULT ONCE MOUSE IS NO LONGER HOVERED*/

        public Color hoverOverColor;
        public Color defaultColor;
        private Color blockSelectedColor = new Color32(0x3D, 0xA8, 0x82, 0xff); //Color of block selected in Master_BlockRotation (should make this pass-through color variable : To fix later

        private void OnMouseEnter()
        {
            if (GetComponent<Renderer>().material.color != blockSelectedColor)
            {
                GetComponent<Renderer>().material.SetColor("_Color", hoverOverColor);
            }
        }

        private void OnMouseExit()
        {
            if (GetComponent<Renderer>().material.color != blockSelectedColor)
            {
                GetComponent<Renderer>().material.SetColor("_Color", defaultColor);
            } 
        }
    }
}
