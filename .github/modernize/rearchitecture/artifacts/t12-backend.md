# t12 — Modelos de domínio e serviços em memória

## Resultado

- Adicionados `Product`, `Review`, `CartItem` e `Order` em `eCommApp-blazor/Models`.
- Criados `ICatalogService`/`CatalogService` e `ICartService`/`CartService`, registrados como scoped no Blazor Server, com estado temporário por circuito.
- `CatalogService` carrega os quatro arquivos JSON existentes em `wwwroot/products`, preserva a ordem do catálogo e mantém avaliações em memória. Review sem data recebe timestamp UTC ISO-8601.
- `CartService.TryAddToCart` retorna `false` para produto indisponível ou sem ID, agrega produtos pelo ID e incrementa quantidade. `Checkout()` retorna `null` para carrinho vazio; caso contrário, cria snapshot da ordem, calcula total e limpa o carrinho.
- O catálogo e o carrinho seguem em memória; nenhuma API, banco ou persistência foi adicionada. As páginas não foram conectadas aos serviços, conforme escopo de t13.

## Contrato para t13

- Namespace dos tipos: `ECommAppBlazor.Models`; serviços: `ECommAppBlazor.Services`.
- Injetar `ICatalogService` e `ICartService` nas páginas/componentes consumidores.
- Produto: propriedades `Id`, `Name`, `Price`, `Description`, `Image`, `Category`, `Reviews`, `InStock`.
- Item do carrinho é composto: `CartItem.Product` e `CartItem.Quantity`. Não assume propriedades planas como `CartItem.Name` ou `CartItem.Price`.
- Carrinho: ler `Items` e `Total`; adicionar via `TryAddToCart(product)` e tratar `false` como indisponível; finalizar via `Checkout()` e tratar retorno nulo como vazio.
- Catálogo: `GetProductsAsync()`, `GetProductByIdAsync(id)` e `AddReviewAsync(id, review)`. `AddReviewAsync` retorna `false` se o produto não existe.
- O estado está scoped e é volátil: fica disponível no circuito atual, sem sobreviver a reinicialização ou entre usuários.

## Upstream Artifacts Consumed
- `.github/modernize/rearchitecture/artifacts/t4-frontend.md` — preservação das cinco rotas, lista/ordem do catálogo, estado visual indisponível de Orange e limite de não conectar fluxos da UI nessa etapa.
- `.github/modernize/rearchitecture/clarification.md` — escopo de treinamento/demo, dados locais/JSON, estado em memória e ausência de backend, banco ou persistência.
- `.github/modernize/rearchitecture/artifacts/t3-frontend.md` — estrutura do projeto Blazor e configuração do .NET 8.
- `eCommApp/src/types/index.ts`, `eCommApp/src/context/CartContext.tsx`, `eCommApp/src/components/ProductsPage.tsx` e `eCommApp/src/components/CartPage.tsx` — estrutura dos dados e regras existentes de produto, disponibilidade, quantidade, avaliação e checkout.

## Evidence Mapping
- `clarification.md#Generic` → implementação local em JSON/memória, sem serviço real ou persistência.
- `t4-frontend.md#Resumo` → serviços preservam catálogo e Orange indisponível; interação de UI segue para t13.
- `CartContext.tsx#addToCart` → `CartService.TryAddToCart` agrega por ID e soma quantidade.
- `CartPage.tsx#handleConfirmCheckout` → `CartService.Checkout` captura itens/total e limpa o carrinho.
- `ProductsPage.tsx#handleReviewSubmit` → `CatalogService.AddReviewAsync` adiciona avaliação ao produto em memória.

## Test Results
- Command: `$env:DOTNET_ROOT = Join-Path $env:USERPROFILE '.dotnet'; $env:PATH = "$env:DOTNET_ROOT;$env:DOTNET_ROOT\tools;$env:PATH"; dotnet test ..\eCommApp-blazor.Tests\ECommAppBlazor.Tests.csproj --configuration Release -p:BaseOutputPath=bin\T12\` (executado a partir de `eCommApp-blazor`).
- Passed: 4 testes (catálogo/JSON e avaliação; inclusão/incremento; recusa de produto indisponível; criação de ordem/limpeza).
- Failed: 0.
- Skipped: 0.
- Command: `dotnet build .\ECommAppBlazor.csproj --configuration Release --no-restore -p:BaseOutputPath=bin\T12\` (SDK 8.0.426 ativado conforme t2).
- Build: sucesso, 0 warnings e 0 errors.
- Nota ambiental: a primeira tentativa sem ativar `DOTNET_ROOT` não encontrou o SDK pinado; a tentativa padrão seguinte encontrou o executável e a DLL Release bloqueados pela instância Blazor ativa (PID 23296). A validação final passou usando diretório de saída isolado; o processo existente foi preservado.

## Issues
- Nenhum bloqueio de implementação. Integração dos serviços nos componentes e validação dos fluxos visuais pertencem a t13/t14.
