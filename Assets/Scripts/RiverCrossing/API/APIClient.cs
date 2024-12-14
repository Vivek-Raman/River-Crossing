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
             JsonConvert.SerializeObject(request), "application/json"))
    {
      yield return www.SendWebRequest();

      MnCSolveResponse response = new MnCSolveResponse();
      try
      {
        response = JsonConvert.DeserializeObject<MnCSolveResponse>(www.downloadHandler.text);
      }
      catch (Exception)
      {
        Debug.LogError("Could not parse response: " + www.downloadHandler.text);
        throw;
      }
      onSuccess?.Invoke(response);
    }
  }

  public static IEnumerator FetchJHSolution(JHSolveRequest request, Action<JHSolveResponse> onSuccess)
  {
    using (UnityWebRequest www = UnityWebRequest.Post("https://vivekraman.dev/jealous-husband",
             JsonConvert.SerializeObject(request), "application/json"))
    {
      yield return www.SendWebRequest();

      JHSolveResponse response = new JHSolveResponse();
      try
      {
        response = JsonConvert.DeserializeObject<JHSolveResponse>(www.downloadHandler.text);
        response.parsedOutput = new Dictionary<string, JHStage>();
        if (response.output != null)
        {
          foreach ((string key, JHStageRaw val) in response.output)
          {
            response.parsedOutput[key] = JHStage.FromRaw(val);
          }
        }
      }
      catch (Exception)
      {
        Debug.LogError("Could not parse response: " + www.downloadHandler.text);
        throw;
      }

      onSuccess?.Invoke(response);
    }
  }
}
}
