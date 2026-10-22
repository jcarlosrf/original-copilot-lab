# .NET Smoke Build com Apphost Bloqueado

Um apphost Debug em uso pode bloquear o build nessa configuração sem indicar erro de compilação no código.

## What Happened
No projeto `original-copilot-lab`, tarefa t5, `dotnet build` não conseguiu substituir `ECommAppBlazor.exe`, mantido aberto pelo PID 8112. O terminal também não herdou a ativação do SDK instalado no perfil de usuário. Ativar `DOTNET_ROOT` e compilar em Release produziu build completo limpo, sem encerrar processo alheio.

## Takeaway
Em projetos com `global.json` e SDK instalado por usuário, ative explicitamente `DOTNET_ROOT`/PATH em cada novo shell. Se o apphost Debug estiver bloqueado, identifique e preserve o processo; uma compilação Release separada pode verificar o código sem disputar o executável Debug. Registre claramente a tentativa Debug falha e o build alternativo aprovado.

## History
- 2026-10-22 (original-copilot-lab/t5): initial