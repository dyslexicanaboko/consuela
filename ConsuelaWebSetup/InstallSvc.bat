sc create Consuela binPath="%~dp0Consuela.Service.exe"
sc failure Consuela actions= restart/60000/restart/60000/""/60000 reset= 86400
sc start Consuela
sc config Consuela start=auto
