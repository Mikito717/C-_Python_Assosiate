@echo off

REM 環境名を変数に設定
set ENV_NAME=C_Sharp_python_assosiate

REM 環境が存在するか確認
conda env list | findstr /C:"%ENV_NAME%" >nul
if %errorlevel% neq 0 (
    echo 環境 %ENV_NAME% が見つかりません。新しい環境を作成します。
    call conda create --name %ENV_NAME% python -y
    call pip install -r requirements.txt
) else (
    echo 環境 %ENV_NAME% が既に存在します。
)

REM 環境をアクティベート
call conda activate %ENV_NAME%
python ./app.py > flask_log.txt 2>&1
