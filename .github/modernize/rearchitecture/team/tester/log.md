## [t7] Validar cinco rotas e regressão React
- Todas as cinco rotas Blazor aceitam acesso direto; confirmar status HTTP sozinho não basta, então verificar também o heading esperado no conteúdo principal.
- A aplicação React define `npm run test:run` como comando canônico; a suíte atual contém 1 teste de componente e passou sem falhas.
- Docker CLI não está disponível e não é necessário: a fase UI-only não depende de banco ou serviço externo. Node.js v26.12.0 está disponível.
- Não foi necessário alterar código ou adicionar testes; o t5 já fornece evidência de build/startup, e esta tarefa fechou a lacuna de rotas registrada lá.
- Learnings consumed: [ux/blazor-phase-one-scope]
- Timing: 2026-10-22T19:35:30Z→2026-10-22T19:40:29Z (~299s)

## [t8.2] Contrato HTTP de integração do Blazor
- O contrato aprovado precisa executar contra Kestrel; HTML prerenderizado permite verificar rotas, navegação e assets sem criar harness .NET permanente.
- O terminal padrão não tinha a instalação de usuário no PATH; `DOTNET_ROOT=%USERPROFILE%\.dotnet` e PATH explícito ativaram o SDK pinado 8.0.426 sem alterar `global.json`.
- O perfil `http` existente configura Development; executar a DLL em Production localmente não serviu o bundle scoped CSS, enquanto o perfil de desenvolvimento serviu todos os três estilos.
- O atributo de escopo CSS do Blazor aparece no `<nav>`; seletores de teste devem tolerar atributos adicionais no elemento.
- Learnings consumed: (none)
- Timing: 2026-10-22T19:53:50Z→2026-10-22T20:00:48Z (~418s)
