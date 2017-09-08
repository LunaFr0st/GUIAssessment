using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AssignmentGUI
{
    public class GUIManager : MonoBehaviour
    {
        public GUIStyle titleStyler = new GUIStyle();
        public GUIStyle styler = new GUIStyle();
        public Font font;

        private float sW;
        private float sH;

        void Start()
        {

        }

        void Update()
        {
            sW = Screen.width / 16;
            sH = Screen.height / 9;
        }

        void OnGUI()
        {
            titleStyler.fontSize = (Screen.width / 16) + (Screen.height / 9);
            titleStyler.normal.textColor = Color.white;
            titleStyler.font = font;

            styler.fontSize = ((Screen.width / 24) + (Screen.height / 11));
            styler.normal.textColor = Color.grey;
            styler.font = font;
            styler.hover.textColor = Color.white;
            styler.active.textColor = Color.gray;

            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            GUI.Label(new Rect(0.25f * sW, 3f * sH, 0, 0), "Roger That!", titleStyler);
            if (GUI.Button(new Rect(0.25f * sW, 5f * sH, 3 * sW, 0.5f * sH), "Play", styler))
            {

            }
        }
    }
}

