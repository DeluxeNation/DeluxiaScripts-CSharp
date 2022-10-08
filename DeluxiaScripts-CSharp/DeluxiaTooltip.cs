using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Deluxia.Unity {
	[ExecuteInEditMode()]
	public class DeluxiaTooltip: MonoBehaviour {
        public TMP_Text header, content;
        public LayoutElement LE;
        public int hSizeLimit, cSizeLimit;
        private static DeluxiaTooltip main;
        private static RectTransform mainTransform;
        // Start is called before the first frame update
        void Awake() {
            if(Application.isPlaying) {
                main = this;
                mainTransform = GetComponent<RectTransform>();
                gameObject.SetActive(false);
            }
        }
        public static void Show(string headerText,string contentText) {
            Cursor.visible = false;
            main.gameObject.SetActive(true);
            main.header.text = headerText == null ? "" : headerText;
            main.content.text = contentText == null ? "" : contentText;
            int headerLength = main.header.text.Length, contentLength = main.content.text.Length;
            //main.LE.enabled = headerLength > main.hSizeLimit || contentLength > main.cSizeLimit;
            main.LE.enabled = Mathf.Max(main.header.preferredWidth,main.content.preferredWidth) >= main.LE.preferredWidth;

            Vector2 Mpos = Input.mousePosition;
            float pivotX = Mpos.x / Screen.width, pivotY = Mpos.y / Screen.height;
            mainTransform.pivot = new Vector2(pivotX * 1.5f,pivotY * 1.5f);
            mainTransform.position = Mpos;
        }
        public static void Hide() {
            Cursor.visible = true;
            if(main != null) {
                main.gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update() {
            if(!Application.isPlaying) {
                int headerLength = header.text.Length, contentLength = content.text.Length;
                LE.enabled = headerLength > hSizeLimit || contentLength > cSizeLimit;
            }
            else {

            }
        }
    }
}
