# t13 — Fluxos demo conectados à UI Blazor

## Resultado

- Habilitada interatividade `InteractiveServer` no roteador para os eventos e bindings das páginas.
- `Products` consome `ICatalogService` e `ICartService`: adiciona produtos disponíveis, preserva Orange indisponível e abre/envia avaliações via modal.
- `Cart` mostra itens, quantidades e total; oferece confirmação/cancelamento do checkout e apresenta o snapshot da ordem criada pelo serviço.
- `Login` preserva a autenticação demonstrativa `admin/admin`, exibe credenciais inválidas e navega para `/admin` no sucesso; não protege a rota nem implementa autenticação real.
- `Admin` valida e apresenta promoção local, encerra a promoção e mantém o status visual esperado; como na referência React, não altera os preços do catálogo.
- Os dados continuam em JSON/memória e o estado do carrinho segue o escopo de circuito já definido em t12.

## Upstream Artifacts Consumed
- `.github/modernize/rearchitecture/artifacts/t4-frontend.md` — rotas, labels, classes CSS, layout e limites visuais da fase inicial.
- `.github/modernize/rearchitecture/artifacts/t12-backend.md` — modelos, métodos dos serviços, recusas de estoque, semântica de review e checkout.
- `.github/modernize/rearchitecture/clarification.md` — inglês na UI, Blazor .NET 8, estado local, demo sem persistência/autenticação real e preservação do React.

## Evidence Mapping
- `t12-backend.md#Contrato para t13` → `Products.razor` usa `TryAddToCart`, `GetProductsAsync` e `AddReviewAsync`; `Cart.razor` lê `CartItem.Product`/`Quantity` e usa `Checkout()`/`Order`.
- `t4-frontend.md#Resumo` → mantidas as rotas e os textos/styles existentes; Orange continua desabilitada e o catálogo preserva ordem e preços.
- `clarification.md#Generic` → login/promoção/checkout permanecem demonstrações locais, sem backend ou persistência.
- Fluxo de browser em `http://127.0.0.1:5202` → login `admin/admin` navegou a `/admin`; promoção de 25% alterou o status; review apareceu no produto; Apple foi adicionada; Orange permaneceu desabilitada; cancelamento preservou o carrinho e confirmação exibiu a ordem e total.

## Test Results
- Command: `$env:DOTNET_ROOT = Join-Path $env:USERPROFILE '.dotnet'; $env:PATH = "$env:DOTNET_ROOT;$env:DOTNET_ROOT\tools;$env:PATH"; dotnet test .\eCommApp-blazor.Tests\ECommAppBlazor.Tests.csproj --configuration Release -p:BaseOutputPath=bin\T13b\`
- Passed: 4.
- Failed: 0.
- Skipped: 0.
- A compilação das páginas Razor ocorreu como parte do `dotnet test`; o build terminou sem erros.
- Smoke de browser: fluxos de login, promoção, review, adição ao carrinho, indisponibilidade e checkout confirm/cancel responderam.
- Ressalva do smoke: executar diretamente a DLL dentro de `bin/T13` não serviu `ECommAppBlazor.styles.css` nem `favicon.png` (404); o banner de erro padrão do template ficou visível por falta do bundle scoped. Os handlers interativos continuaram funcionando. A validação via `dotnet run`/publicação normal não foi repetida.

## Issues
- Não há bloqueios funcionais encontrados nos fluxos exercitados.
- Aviso não bloqueante: verificar o bundle `ECommAppBlazor.styles.css` em uma execução normal do projeto, pois a DLL isolada de saída dos testes não o disponibilizou.