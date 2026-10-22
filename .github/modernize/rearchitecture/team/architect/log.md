## [t5] Build e startup smoke test .NET 8
- O SDK 8.0.426 está instalado em `%USERPROFILE%\.dotnet`; terminais novos precisam de `DOTNET_ROOT` e PATH explícitos para respeitar o pin em `global.json`.
- O build Debug falhou ao substituir o apphost bloqueado por ECommAppBlazor PID 8112; preservar o processo e executar o build Release em saída separada permitiu validar a compilação completa sem afetar a instância do usuário.
- Kestrel respondeu HTTP 200 em 5198; a medição isolada em 5200 levou 2,82 s até aceitar conexão.
- Learnings consumed: (none)

## [review-commit] Commit da fase Review do run 30cb0377-521a-4005-9477-4db1d3327535
- A Review ficou registrada em `t5-architect.md`, board e `project-profile.yaml`; as saídas Release e alterações geradas de `obj` são evidência do build e foram preservadas para o commit.
- Nenhum arquivo de comportamento da aplicação estava alterado; o listener existente foi mantido em execução.
- Learnings consumed: [architect/dotnet-smoke-build-process-lock]