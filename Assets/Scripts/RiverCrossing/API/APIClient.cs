using System;
using System.Collections;
using dev.vivekraman.RiverCrossing.API.Response;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace dev.vivekraman.RiverCrossing.API
{
public static class APIClient
{
  public static IEnumerator FetchMnCSolution(MnCStage currentStage, Action<MnCSolutionResponse> onSuccess)
  {
    using (UnityWebRequest webRequest = UnityWebRequest.Post("https://vivekraman.dev/backend/river-crossing/solve",
             JsonUtility.ToJson(currentStage), "application/json"))
    {
      // TODO: call API
      // yield return webRequest.SendWebRequest();
      yield return new WaitForSeconds(2f);

      MnCSolutionResponse response = JsonConvert.DeserializeObject<MnCSolutionResponse>("{ \"output\": {" +
          "\"0\":  {\"M_left\": 3, \"C_left\": 3, \"M_right\": 0, \"C_right\": 0, \"boat_position\": \"left\"}," +
          "\"1\":  {\"M_left\": 3, \"C_left\": 1, \"M_right\": 0, \"C_right\": 2, \"boat_position\": \"right\"}," +
          "\"2\":  {\"M_left\": 3, \"C_left\": 2, \"M_right\": 0, \"C_right\": 1, \"boat_position\": \"left\"}," +
          "\"3\":  {\"M_left\": 3, \"C_left\": 0, \"M_right\": 0, \"C_right\": 3, \"boat_position\": \"right\"}," +
          "\"4\":  {\"M_left\": 3, \"C_left\": 1, \"M_right\": 0, \"C_right\": 2, \"boat_position\": \"left\"}," +
          "\"5\":  {\"M_left\": 1, \"C_left\": 1, \"M_right\": 2, \"C_right\": 2, \"boat_position\": \"right\"}," +
          "\"6\":  {\"M_left\": 2, \"C_left\": 2, \"M_right\": 1, \"C_right\": 1, \"boat_position\": \"left\"}," +
          "\"7\":  {\"M_left\": 0, \"C_left\": 2, \"M_right\": 3, \"C_right\": 1, \"boat_position\": \"right\"}," +
          "\"8\":  {\"M_left\": 0, \"C_left\": 3, \"M_right\": 3, \"C_right\": 0, \"boat_position\": \"left\"}," +
          "\"9\":  {\"M_left\": 0, \"C_left\": 1, \"M_right\": 3, \"C_right\": 2, \"boat_position\": \"right\"}," +
          "\"10\": {\"M_left\": 1, \"C_left\": 1, \"M_right\": 2, \"C_right\": 2, \"boat_position\": \"left\"}," +
          "\"11\": {\"M_left\": 0, \"C_left\": 0, \"M_right\": 3, \"C_right\": 3, \"boat_position\": \"right\"}" +
      "}}");

      onSuccess?.Invoke(response);
    }
  }
}
}
