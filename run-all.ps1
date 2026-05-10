# Script para ejecutar tanto la API como el Frontend simultáneamente

# Iniciar la API en una nueva ventana
Start-Process dotnet "run --project PizzaPos.Api" -NoNewWindow:$false

# Esperar un momento para que la API inicie (Aumentado para asegurar DB ready)
Write-Host "Iniciando API... esperando 8 segundos..."
Start-Sleep -Seconds 8

# Iniciar el Frontend
Write-Host "Iniciando Frontend..."
dotnet run --project PizzaPos.WinForms
