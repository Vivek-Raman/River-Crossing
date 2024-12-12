using System;
using System.Collections;
using System.Collections.Generic;
using dev.vivekraman.RiverCrossing.API.Request;
using dev.vivekraman.RiverCrossing.API.Response;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace dev.vivekraman.RiverCrossing.API
{
public static class APIClient
{
  public static IEnumerator FetchMnCSolution(MnCSolveRequest request, Action<MnCSolveResponse> onSuccess)
  {
    using (UnityWebRequest www = UnityWebRequest.Post("https://vivekraman.dev/missionary-cannibal",
             JsonUtility.ToJson(request), "application/json"))
    {
      yield return www.SendWebRequest();

      MnCSolveResponse response = JsonConvert.DeserializeObject<MnCSolveResponse>(www.downloadHandler.text);
      onSuccess?.Invoke(response);
    }
  }

  public static IEnumerator FetchJHSolution(JHSolveRequest request, Action<JHSolveResponse> onSuccess)
  {
    using (UnityWebRequest www = UnityWebRequest.Post("https://vivekraman.dev/jealous-husband",
             JsonUtility.ToJson(request), "application/json"))
    {
      yield return www.SendWebRequest();

      JHSolveResponse response =
        JsonConvert.DeserializeObject<JHSolveResponse>(www.downloadHandler.text);
      response.parsedOutput = new Dictionary<string, JHStage>();
      foreach ((string key, JHStageRaw val) in response.output)
      {
        response.parsedOutput[key] = JHStage.FromRaw(val);
      }
      onSuccess?.Invoke(response);
    }
  }
}
}
