using UnityEngine;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class call_flask_responce : MonoBehaviour
{
    public void Click()
    {
        _ = ClickAsync();
    }

    private async Task ClickAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = "http://localhost:5000/api/hello";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Debug.Log($"Response: {responseBody}");
                }
                else
                {
                    Debug.LogError($"Error: {response.StatusCode}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                Debug.LogError($"HttpRequestException: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Exception: {ex.Message}");
            }
        }
    }
}