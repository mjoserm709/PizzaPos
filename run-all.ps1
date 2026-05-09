# Script para ejecutar tanto la API como el Frontend simultáneamente

# Iniciar la API en una nueva ventana
Start-Process dotnet "run --project PizzaPos.Api" -NoNewWindow:$false

# Esperar un momento para que la API inicie
Write-Host "Iniciando API... esperando 3 segundos..."
Start-Sleep -Seconds 3

# Iniciar el Frontend
Write-Host "Iniciando Frontend..."
dotnet run --project PizzaPos.WinForms
