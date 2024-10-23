using System;
using System.Diagnostics;
using UnityEngine;
using System.Threading;

public class activate_python : MonoBehaviour
{    
    private int pid;
    public void StartFlaskServer()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "powershell.exe";
        startInfo.Arguments = "/c cd ./Assets; dir; run_flask.bat";
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;

        using (Process process = new Process())
        {
            process.StartInfo = startInfo;
            
            
            process.OutputDataReceived += (sender, data) => {
                if (data.Data != null)
                    UnityEngine.Debug.Log(data.Data);
                    //Console.WriteLine($"Output: {data.Data}");
            };
            process.ErrorDataReceived += (sender, data) => {
                if (data.Data != null)
                    UnityEngine.Debug.Log($"Error: {data.Data}");
                    //Console.WriteLine($"Error: {data.Data}");
            };
            
            process.Start();
            
            pid = process.Id;
            UnityEngine.Debug.Log($"Flask Started Process ID: {pid}");
            
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }
        UnityEngine.Debug.Log("Flask server started");
    }   
    public void StopFlaskServer()
    {
    try
        {
            Process.GetProcessById(pid).Kill();
            UnityEngine.Debug.Log("Flaskサーバーが停止しました。");
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"プロセスの停止に失敗しました: {ex.Message}");
        }
    }
}
