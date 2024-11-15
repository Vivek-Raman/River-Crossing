using System.Collections;
using dev.vivekraman.RiverCrossing.API.Request;
using UnityEngine;
using UnityEngine.Networking;

namespace dev.vivekraman.RiverCrossing.API
{
public static class APIClient
{
  public static IEnumerator FetchSolutions(string gameState)
  {
    TestRequest body = new TestRequest();
    body.StrContent = "asdte";

    string text = "";

    using (UnityWebRequest webRequest = UnityWebRequest.Post("https://vivekraman.dev/backend/notion-assistant/echo",
             JsonUtility.ToJson(body), "application/json"))
    {
      yield return webRequest.SendWebRequest();

      Debug.Log(webRequest.result);
      text = webRequest.downloadHandler.text;
    }

    Debug.Log(text);

    // List<string> solutions = new List<string>();
    // // TODO: implement and call API
    // solutions.Add(gameState); // TODO: remove this
    // return solutions;
  }
}
}
