@echo off
call conda activate C_Sharp_python_assosiate
python ./app.py > flask_log.txt 2>&1
