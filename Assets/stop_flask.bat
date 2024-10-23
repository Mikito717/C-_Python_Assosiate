@echo off

rem 実行中のPythonプロセスを強制終了
taskkill /F /IM python.exe

echo Flaskサーバーを停止しました
