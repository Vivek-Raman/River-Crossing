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
  public static IEnumerator FetchMnCSolution(MnCSolveRequest request, Action<Dictionary<string, MnCStage>> onSuccess)
  {
    request.M_total = 3;
    request.C_total = 3;
    request.M_left = 3;
    request.C_left = 3;
    request.M_right = 0;
    request.C_right = 0;
    request.boat_position = "left";
    request.solver = "a_star";

    using (UnityWebRequest www = UnityWebRequest.Post("https://vivekraman.dev/missionary-cannibal",
             JsonUtility.ToJson(request), "application/json"))
    {
      yield return www.SendWebRequest();

      Dictionary<string, MnCStage> response =
        JsonConvert.DeserializeObject<Dictionary<string, MnCStage>>(www.downloadHandler.text);
      onSuccess?.Invoke(response);
    }
  }

  public static IEnumerator FetchJHSolution(JHSolveRequest request, Action<Dictionary<string, JHStage>> onSuccess)
  {
    request.num_of_couples = 3;
    request.solver = "a_star";

    using (UnityWebRequest www = UnityWebRequest.Post("https://vivekraman.dev/jealous-husband",
             JsonUtility.ToJson(request), "application/json"))
    {
      yield return www.SendWebRequest();

      Dictionary<string, JHStageRaw> rawResponse =
        JsonConvert.DeserializeObject<Dictionary<string, JHStageRaw>>(www.downloadHandler.text);
      Dictionary<string, JHStage> response = new ();
      foreach ((string key, JHStageRaw val) in rawResponse)
      {
        response[key] = JHStage.FromRaw(val);
      }
      onSuccess?.Invoke(response);
    }
  }
}
}
