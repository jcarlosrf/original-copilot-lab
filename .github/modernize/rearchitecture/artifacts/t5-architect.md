# t5 — Build e startup smoke test .NET 8

## Veredito

**PASS com ressalva ambiental.** O build completo em Release concluiu sem avisos ou erros, e a aplicação respondeu com HTTP 200. Nenhum código-fonte foi alterado.

- SDK: `8.0.426`, resolvido de `C:\Users\jsra\.dotnet` conforme `global.json`.
- Build final: `dotnet build --configuration Release` — exit code `0`, 0 warnings e 0 errors.
- Startup principal: `dotnet run --no-build --configuration Release --urls http://127.0.0.1:5198` — HTTP 200, HTML de 1.972 bytes e `<title>` presente.
- Startup independente temporário: DLL Release em `http://127.0.0.1:5200` — prontidão em `2,82 s`, HTTP 200. Somente o processo temporário foi encerrado.
- Verificação final: listener em `5198` (PID `23296`) e HTTP 200 às `2026-10-22T19:28:53Z`; a instância principal foi deixada ativa.

## Test Results

- Comando: `C:\Users\jsra\.dotnet\dotnet.exe build --configuration Release` em `eCommApp-blazor`.
- Passed: 1 build completo; 2 sondas de startup HTTP (portas `5198` e `5200`).
- Failed: 1 tentativa do build padrão Debug terminou com exit code `1` porque `ECommAppBlazor.exe` estava bloqueado pelo processo PID `8112` (MSB3027/MSB3021). Uma tentativa seguinte não iniciou o build porque o terminal não tinha `DOTNET_ROOT`/PATH da instalação de usuário e não encontrou o SDK `8.0.426`; após ativar a instalação documentada em t2, o build Release passou.
- Skipped: testes automatizados e verificação das cinco rotas; pertencem à validação t7.
- Falhas de resposta após startup: 0. A primeira sonda em `5198`, antes de iniciar a aplicação nesta sessão, não encontrou listener.
- Aviso não bloqueante: o middleware de redirecionamento HTTPS não encontrou uma porta HTTPS configurada no perfil HTTP local.

## Observações

- A configuração Debug continua sujeita ao bloqueio do apphost enquanto o processo PID `8112` mantiver o arquivo aberto. O processo foi preservado; o build completo em Release usou uma saída separada e passou.
- A medição de `2,82 s` refere-se ao startup temporário em `5200`; o tempo exato da instância em `5198` não foi instrumentado.
- O teste confirma build e inicialização, não paridade das cinco páginas ou fluxos de negócio.

## Upstream Artifacts Consumed

- `.github/modernize/rearchitecture/clarification.md` — alvo .NET 8, rotas nativas Blazor, escopo UI-only e limites das fases seguintes.
- `.github/modernize/rearchitecture/artifacts/t2-devops.md` — caminho de instalação do SDK e ativação necessária em novos terminais.
- `.github/modernize/rearchitecture/artifacts/t3-frontend.md` — scaffold, `global.json` e resultado de build anterior.
- `.github/modernize/rearchitecture/artifacts/t4-frontend.md` — cinco rotas implementadas e responsabilidades de validação downstream.
- `.github/modernize/rearchitecture/artifacts/t9-devops.md` — contexto de dependências/build para o projeto .NET.
- `.github/modernize/rearchitecture/board.md` — status e dependências da fase Review; consultado sem alterações.

## Evidence Mapping

- `t2-devops.md#Target Environment Preparation` → SDK `8.0.426` ativado explicitamente para o build final.
- `t3-frontend.md#Test Results` → projeto Blazor com pin de SDK; build independente repetido nesta tarefa.
- `t4-frontend.md#Test Results` → escopo de cinco páginas; esta tarefa confirmou apenas o startup HTTP, deixando a verificação das rotas para t7.
- `clarification.md#Downstream Usage Notes` → nenhuma lógica de carrinho, avaliação, promoção, login ou checkout foi implementada nem testada aqui.
- `t9-devops.md#Resumo` → varredura anterior sem referências NuGet; o build completo foi executado no projeto .NET identificado.