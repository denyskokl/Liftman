using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class ShareImageCanvas : MonoBehaviour
{
  private bool isProcessing = false;
  public Canvas canvasLogo;
  public string message;


  public void ButtonShare()
  {
    canvasLogo.enabled = true;

    if (!isProcessing)
    {
      StartCoroutine(ShareScreenshot());
    }
  }


  public IEnumerator ShareScreenshot()
  {
    isProcessing = true;
    // wait for graphics to render
    yield return new WaitForEndOfFrame();
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
    // create the texture
    Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
    // put buffer into texture
    screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
    // apply
    screenTexture.Apply();
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
    byte[] dataToSave = screenTexture.EncodeToPNG();
    string destination = Application.persistentDataPath + "/MyImage.png";
    File.WriteAllBytes(destination, dataToSave);
    if (!Application.isEditor)
    {
//#if UNITY_ANDROID
      // block to open the file and share it ------------START
      AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
      AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
      intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
      AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
      AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
      intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

      intentObject.Call<AndroidJavaObject>("setType", "text/plain");
      intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "" + message);
      intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");

      intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
      AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
      AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

      currentActivity.Call("startActivity", intentObject);
//    #elif UNITY_IOS
//    		CallSocialShareAdvanced(message, imagePath);
//    #else
//      Debug.Log("No sharing set up for this platform.");
//#endif
//    }
//    isProcessing = false;
//    canvasLogo.enabled = false;
//  }
//
//#if UNITY_IOS
//  	public struct ConfigStruct
//  	{
//  		public string title;
//  		public string message;
//  	}
//
//  	[DllImport ("__Internal")] private static extern void showAlertMessage(ref ConfigStruct conf);
//
//  	public struct SocialSharingStruct
//  	{
//  		public string text;
//  		public string image;
//
//  	}
//
//  	[DllImport ("__Internal")] private static extern void showSocialSharing(ref SocialSharingStruct conf);
//
//  	public static void CallSocialShare(string title, string message)
//  	{
//  		ConfigStruct conf = new ConfigStruct();
//  		conf.title  = title;
//  		conf.message = message;
//  		showAlertMessage(ref conf);
//  	}
//
//  	public static void CallSocialShareAdvanced(string defaultTxt, string img)
//  	{
//  		SocialSharingStruct conf = new SocialSharingStruct();
//  		conf.text = defaultTxt;
//  		conf.image = img;
//
//  		showSocialSharing(ref conf);
//  	}
//  #endif
    }
  }
}