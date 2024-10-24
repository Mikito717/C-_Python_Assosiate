using System;
using System.Diagnostics;
using UnityEngine;
using System.Threading.Tasks;

public class ActivatePython : MonoBehaviour
{    
    private static Process flaskProcess;

    public void StartFlaskServer()
    {
        Task.Run(() => {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = "/c cd ./Assets; run_flask.bat",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            flaskProcess = new Process
            {
                StartInfo = startInfo
            };
            UnityEngine.Debug.Log(flaskProcess);
            UnityEngine.Debug.Log("Flask server starting...");

            flaskProcess.OutputDataReceived += (sender, data) => {
                if (data.Data != null)
                    UnityEngine.Debug.Log(data.Data);
            };
            flaskProcess.ErrorDataReceived += (sender, data) => {
                if (data.Data != null)
                    UnityEngine.Debug.Log($"Error: {data.Data}");
            };

            flaskProcess.Start();
            UnityEngine.Debug.Log($"Flask Started Process ID: {flaskProcess.Id}");

            flaskProcess.BeginOutputReadLine();
            flaskProcess.BeginErrorReadLine();

            // Wait for the process to exit, but in the background
            flaskProcess.WaitForExit();
            UnityEngine.Debug.Log("Flask server stopped");
        });
    }

        void OnApplicationQuit()
    {
        StopFlaskServer();
    }

    void OnDisable()
    {
        StopFlaskServer();
    }

    public void StopFlaskServer()
    {
            // バッチファイルのパスを指定
    string batFilePath = @"Assets\stop_flask.bat";

    // バッチファイルを実行
    var processInfo = new System.Diagnostics.ProcessStartInfo("powershell.exe", "/c " + batFilePath)
    {
        CreateNoWindow = true,
        UseShellExecute = false
    };

    using (var process = System.Diagnostics.Process.Start(processInfo))
    {
        process.WaitForExit();
        UnityEngine.Debug.Log("Flaskサーバーが停止しました。");
    }
    }
}
