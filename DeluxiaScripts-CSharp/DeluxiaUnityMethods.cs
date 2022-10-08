using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using System;

namespace Deluxia.Unity{
    public static class DeluxiaUnityMethods{
        public static MonoBehaviour mainClass;
        public static int SetTextBox(InputField input,int min,int max){
            int checkNum = 0;
            if(int.TryParse(input.text,out int num)){
                checkNum = num;
            }
            else{
                return int.Parse(input.placeholder.GetComponent<Text>().text);
            }
            if(checkNum > max){
                input.text = max+"";
                return max;
            }
            else if(checkNum < min){
                input.text = min+"";
                return min;
            }
            else{
                return checkNum;
            }
        }
        public static byte SetTextBox(InputField input,byte min,byte max){
            byte checkNum = 0;
            if(byte.TryParse(input.text,out byte num)){
                checkNum = num;
            }
            else{
                return byte.Parse(input.placeholder.GetComponent<Text>().text);
            }
            if(checkNum > max){
                input.text = max+"";
                return max;
            }
            else if(checkNum < min){
                input.text = min+"";
                return min;
            }
            else{
                return checkNum;
            }
        }
        public static IEnumerator Fade2Can(CanvasGroup CA,CanvasGroup CB,float speed){
            float opacityT = 0f;
            float inverseT = 255f;
            CA.GetComponent<Canvas>().enabled = true;
            while (opacityT < 255){
                //Debug.Log(opacityT);
                CA.alpha = opacityT/255f;
                CB.alpha = inverseT/255f;
                opacityT+=speed;
                inverseT-=speed;
                yield return new WaitForSeconds(0.01f);
            }
            CB.GetComponent<Canvas>().enabled = false;
            CA.alpha = 1;
            CB.alpha = 0;
        }
        public static IEnumerator FadeCan(CanvasGroup C,float speed,bool fadeIn){
            float opacity = fadeIn?0f:255f;
            C.GetComponent<Canvas>().enabled = true;
            do{
                C.alpha = opacity/255f;
                opacity+=speed*(fadeIn?1:-1);
                yield return new WaitForSeconds(0.01f);
            }while (opacity < 255 && opacity > 0);
            C.alpha = fadeIn?1:0;
            C.GetComponent<Canvas>().enabled = fadeIn;
        }
		public static IEnumerator FadeCan(CanvasGroup C,float speed,bool fadeIn,System.Action method) {
			float opacity = fadeIn ? 0f : 255f;
			C.GetComponent<Canvas>().enabled = true;
			do {
				C.alpha = opacity / 255f;
				opacity += speed * (fadeIn ? 1 : -1);
				yield return new WaitForSeconds(0.01f);
			} while(opacity < 255 && opacity > 0);
			C.alpha = fadeIn ? 1 : 0;
			C.GetComponent<Canvas>().enabled = fadeIn;
            method();
		}
		public static IEnumerator FadeCan(CanvasGroup C,float speed,bool fadeIn,System.Action<string> method,string param) {
			float opacity = fadeIn ? 0f : 255f;
			C.GetComponent<Canvas>().enabled = true;
			do {
				C.alpha = opacity / 255f;
				opacity += speed * (fadeIn ? 1 : -1);
				yield return new WaitForSeconds(0.01f);
			} while(opacity < 255 && opacity > 0);
			C.alpha = fadeIn ? 1 : 0;
			C.GetComponent<Canvas>().enabled = fadeIn;
			method(param);
		}
		public static IEnumerator FadeCanInAndOut(CanvasGroup C,bool blockRaycasts,float speed,float time){
            float opacity = 0f;
            C.blocksRaycasts = blockRaycasts;
            C.GetComponent<Canvas>().enabled = true;
            do{
                C.alpha = opacity/255f;
                opacity+=speed;
                yield return new WaitForSeconds(0.01f);
            }while (opacity < 255 && opacity > 0);
            C.alpha = 1;
            yield return new WaitForSeconds(time);
            do{
                C.alpha = opacity/255f;
                opacity+=speed*-1;
                yield return new WaitForSeconds(0.01f);
            }while (opacity < 255 && opacity > 0);
            C.alpha = 0;
            C.blocksRaycasts = false;
            C.GetComponent<Canvas>().enabled = false;
        }
        public static IEnumerator FadeTMP(TMP_Text T,float speed,bool fadeIn){
            float opacity = fadeIn?0f:255f;
            do{
                T.alpha = opacity/255f;
                opacity+=speed*(fadeIn?1:-1);
                yield return new WaitForSeconds(0.01f);
            }while (opacity < 255 && opacity > 0);
            T.alpha = fadeIn?1:0;

        }
        public static IEnumerator FadeImage(UnityEngine.UI.Image T,float speed,bool fadeIn){
            float opacity = fadeIn?0f:255f;
            do{
                T.color = new Color(T.color.r,T.color.g,T.color.b,opacity/255f);
                opacity+=speed*(fadeIn?1:-1);
                yield return new WaitForSeconds(0.01f);
            }while (opacity < 255 && opacity > 0);
            T.color = new Color(T.color.r,T.color.g,T.color.b,fadeIn?1:0);

        }
        public static Image ConvertSpriteToImage(this SpriteRenderer sprite,Transform parent,bool copyChildren) {
            if (sprite == null || parent == null) return null;
            GameObject toImage = Object.Instantiate(sprite,parent).gameObject;
            Object.Destroy(toImage.GetComponent<SpriteRenderer>());
            if(!copyChildren) {
                foreach(Transform t in toImage.transform.GetComponentInChildren<Transform>()) {
                    if(t != null && t.gameObject != null) {
                        Object.Destroy(t.gameObject);
                    }
                }
            }
            toImage.AddComponent<Image>();
            toImage.GetComponent<Image>().sprite = sprite.sprite;
			toImage.GetComponent<Image>().color = sprite.color;
			toImage.GetComponent<Image>().enabled = sprite.enabled;
            toImage.GetComponent<Image>().SetNativeSize();
            toImage.GetComponent<RectTransform>().localScale = sprite.transform.lossyScale;
			//toImage.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
			//toImage.AddComponent<RectTransform>();
			toImage.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(sprite.transform.position);
            return toImage.GetComponent<Image>();

		}
        public static RectTransform[,] CreateUIGrid(RectTransform original,CloneNameType nameT,int rowLength,int totalInGrid,float moveByX,float moveByY,bool destroyOriginal,bool moveToOriginal){
            if(totalInGrid == 0 || rowLength == 0){
                return null;
            }
            // Debug.Log(totalInGrid);
            // Debug.Log(rowLength);
            if(rowLength > totalInGrid){
                rowLength = totalInGrid;
            }
            RectTransform[,] toSend = new RectTransform[(totalInGrid/(rowLength+1))+1,rowLength];
            int total = 0;
            for (int i = 0; total < totalInGrid; i++){
                for (int j = 0;j < rowLength; j++){
                    total++;
                    if(total > totalInGrid){
                        toSend[i,j] = null;
                        continue;
                    }
                    //Debug.Log(total);
                    toSend[i,j] = GameObject.Instantiate(original.gameObject,original.parent).GetComponent<RectTransform>();
                    toSend[i,j].anchoredPosition = new Vector3(original.localPosition.x+(moveByX*(j+(!moveToOriginal ||(!destroyOriginal&& i==0)?1:0))),original.localPosition.y+(moveByY*i));
                    switch(nameT){
                        case CloneNameType.total:
                        toSend[i,j].gameObject.name = ""+(total-1);
                        break;
                        case CloneNameType.grid:
                        toSend[i,j].gameObject.name = "["+i+","+j+"]";
                        break;
                        case CloneNameType.originalName:
                        toSend[i,j].gameObject.name = original.gameObject.name;
                        break;
                    }
                }
            }
            if(destroyOriginal){
                GameObject.Destroy(original.gameObject);
            }
            return toSend;
        }
        public static List<RectTransform> CreateUIGridList(RectTransform original,CloneNameType nameT,int rowLength,int totalInGrid,float moveByX,float moveByY,bool destroyOriginal){
            if(totalInGrid == 0 || rowLength == 0){
                return null;
            }
            Debug.Log(totalInGrid);
            Debug.Log(rowLength);
            if(rowLength > totalInGrid){
                rowLength = totalInGrid;
            }
            List<RectTransform> toSend = new List<RectTransform>();
            int total = 0;
            for (int i = 0; total < totalInGrid; i++){
                for (int j = 0;j < rowLength; j++){
                    total++;
                    if(total > totalInGrid){
                        continue;
                    }
                    RectTransform next = GameObject.Instantiate(original.gameObject,original.parent).GetComponent<RectTransform>();
                    toSend.Add(next);
                    next.anchoredPosition = new Vector3(original.localPosition.x+(moveByX*(j+(!destroyOriginal && i==0?1:0))),original.localPosition.y+(moveByY*i));
                    switch(nameT){
                        case CloneNameType.total:
                        next.gameObject.name = ""+(total-1);
                        break;
                        case CloneNameType.grid:
                        next.gameObject.name = "["+i+","+j+"]";
                        break;
                        case CloneNameType.originalName:
                        next.gameObject.name = original.gameObject.name;
                        break;
                    }
                }
            }
            if(destroyOriginal){
                GameObject.Destroy(original.gameObject);
            }
            return toSend;
        }
        private static void FindMainClass(){
          if(mainClass == null){
            mainClass = GameObject.Find("GameScriptsManager").GetComponent<MonoBehaviour>();
          }
        }
        public static AudioSource ChangeSong(this AudioSource audio,MonoBehaviour main, AudioClip clip,float volume){
            //MonoBehaviour main = new MonoBehaviour();
            FindMainClass();
            //Debug.Log((float)(Settings.GetSettingsChoices().settings["MusicVol"]/100f));
            mainClass.StartCoroutine(DeluxiaUnityMethods.FadeOut(audio,0,volume,true));
            AudioSource audio2 = audio.gameObject.AddComponent<AudioSource>();
            audio2.volume = 0;
            //audio.CloneObject();
            mainClass.StartCoroutine(DeluxiaUnityMethods.FadeIn(audio2,clip,0,volume));
            return audio2;
            
        }

        private static IEnumerator FadeOut(AudioSource audio,float delay,float maxVol,bool destroyWhenDone){
            yield return new WaitForSeconds(delay);
            
            float timeElapsed = 0;
            while (audio.volume > 0) {
                audio.volume = Mathf.Lerp(maxVol, 0, timeElapsed / 5);
                timeElapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            if(destroyWhenDone){
                Component.Destroy(audio);
            }
        }

        public static IEnumerator FadeIn(AudioSource audio,AudioClip clip,float delay,float maxVol){
            yield return new WaitForSeconds(delay);
            Debug.Log(maxVol);
            float timeElapsed = 0;
            audio.clip = clip;
            audio.Play();
            while (audio.volume < maxVol) {
                audio.volume = Mathf.Lerp(0, maxVol, timeElapsed / 5);
                timeElapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
