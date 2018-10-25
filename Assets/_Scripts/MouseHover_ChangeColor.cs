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

        private void OnMouseEnter()
        {
            GetComponent<Renderer>().material.SetColor("_Color", hoverOverColor);
        }

        private void OnMouseExit()
        {
            GetComponent<Renderer>().material.SetColor("_Color", defaultColor);
        }
    }
}
