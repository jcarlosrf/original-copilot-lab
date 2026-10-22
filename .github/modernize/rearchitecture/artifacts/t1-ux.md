# t1 — Especificação de UX da fase 1

## Objetivo

Reproduzir em uma aplicação Blazor Web App (.NET 8), localizada em `eCommApp-blazor`, a estrutura visual, a navegação e as cinco páginas existentes de The Daily Harvest. A aplicação React original permanece intacta. Esta entrega é de apresentação: não implementa regras de negócio nem efeitos de carrinho, avaliações, promoções, autenticação ou checkout.

## Decisões de experiência

- Manter a interface em inglês e preservar a identidade e a composição atuais, adaptando `App.css` e `index.css` ao host Blazor em vez de introduzir outra biblioteca ou redesign.
- Reutilizar os quatro JSONs do catálogo e as quatro imagens (`apple.png`, `grapes.png`, `orange.png`, `pear.png`), preservando nomes e relações entre `image` e arquivo.
- Usar navegação nativa do Blazor para as cinco rotas abaixo. Cada rota deve renderizar também quando aberta diretamente pelo URL.
- Preservar o comportamento responsivo que já existe, inclusive a quebra da navegação e o breakpoint de 768 px para a grade de produtos.
- A única interação funcional nesta fase é navegação entre páginas. Controles de negócio ficam visualmente presentes como parte da paridade, mas não alteram dados, navegam para resultados de negócio nem exibem confirmações falsas. A regra original de indisponibilidade permanece: Orange mostra “Out of Stock” e não permite adicionar.
- Não adicionar autenticação real, proteção da rota `/admin`, persistência, chamadas de API, estado de carrinho funcional, modais de avaliações/checkout ou tratamento funcional de promoção. Esses comportamentos pertencem às fases seguintes.

## Estrutura compartilhada

- Aplicar em todas as páginas o shell de altura mínima de viewport, em coluna: cabeçalho no topo, conteúdo principal expansível e rodapé no fim.
- Preservar o fundo em gradiente roxo/azul, o cabeçalho e o rodapé translúcidos, a tipografia e as cores da folha existente, sem reinterpretar a marca.
- O cabeçalho exibe “The Daily Harvest” e navegação central com `Home` → `/`, `Products` → `/products`, `Cart` → `/cart` e `Admin Login` → `/login`. Não existe link de navegação separado para `/admin` no original.
- O rodapé mantém “© 2025 The Daily Harvest. All rights reserved.”
- Em telas até 768 px, permitir que a navegação quebre linha e reduzir o espaçamento interno conforme as regras existentes. Manter conteúdo utilizável a partir de 320 px, sem rolagem horizontal causada pela página.
- Conservar landmarks semânticos (`header`, `nav`, `main`, `footer`), ordem de títulos, foco de teclado nativo e texto alternativo com o nome do produto. Associar labels aos campos; isso é um requisito básico para não regredir a interface, não uma auditoria formal WCAG nesta fase.

## Rotas e conteúdo

| Rota | Página | Conteúdo e apresentação |
|---|---|---|
| `/` | Home | Mensagem “Welcome to the The Daily Harvest!” e texto “Check out our products page for some great deals.” em área principal centralizada entre cabeçalho e rodapé. |
| `/products` | Products | Título “Our Products”; grade com Apple, Grapes, Orange e Pear, nesta ordem. Cada item mostra imagem, nome, preço em USD com duas casas decimais, descrição e controle “Add to Cart” ou “Out of Stock”. Preservar os dados e as imagens do JSON; Orange é indisponível. |
| `/cart` | Cart | Título “Your Cart” e estado vazio “Your cart is empty.”, sem cards nem botão de checkout enquanto o carrinho não for funcional nesta fase. |
| `/login` | Admin Login | Título “Admin Login”, campos de username e password e botão “Login”. O botão não autentica nem redireciona; não produzir erro ou sucesso fictício. |
| `/admin` | Admin portal | Título “Welcome to the admin portal.”; label “Set Sale Percent (% off for all items):”, campo inicialmente `0`, botões “Submit” e “End Sale”, estado “No sale active.” e link “Back to Storefront” para `/`. A rota é aberta diretamente; não depende de login nesta fase. Os controles não alteram o estado de promoção. |

## Contrato visual dos produtos

- Catálogo: `apple.json`, `grapes.json`, `orange.json` e `pear.json`; conservar valores, descrições e disponibilidade existentes.
- Mostrar preços no padrão atual (`$0.50`, `$2.50`, `$0.75`, `$0.60`), sem descontos calculados nesta fase.
- Desktop: grade responsiva com cards de largura mínima aproximada de 280 px e espaçamento de 2 rem, dentro de conteúdo limitado a 1200 px.
- Até 768 px: uma coluna e padding reduzido. Imagens mantêm proporção recortada conforme `object-fit: cover`, altura visual de 200 px, e `alt` com o nome do produto.
- Cards preservam fundo branco, borda, sombra e hierarquia existente de nome, preço e descrição. Botões de ação conservam o estilo original; clicar em um controle de negócio não deve modificar a página.

## Critérios de aceite

1. As cinco rotas renderizam a página correta por navegação e por acesso direto, usando o mesmo cabeçalho e rodapé.
2. A página Products exibe exatamente os quatro itens do catálogo local, na ordem indicada, com valores e disponibilidade coerentes com os JSONs e sem dependência de API.
3. A página Cart inicia vazia; não existe confirmação de checkout ou estado de pedido nesta fase.
4. Login e Admin mantêm os controles e textos descritos, mas nenhuma credencial, promoção ou alteração de estado é processada.
5. A aparência usa CSS e imagens existentes, preserva a identidade visual e responde ao breakpoint de 768 px sem overflow em viewport de 320 px.
6. A aplicação Blazor fica isolada em `eCommApp-blazor`; não altera nem substitui arquivos da aplicação React.
7. Nenhum fluxo de negócio listado para a fase 3 é apresentado como concluído ou funcional.

## Fora de escopo nesta entrega

Adicionar ao carrinho, bloqueio/interação de indisponibilidade além da apresentação do botão, avaliação/modal e envio de review, aplicação/fim de promoção, validação de credenciais, estado autenticado, checkout/modal e confirmação de pedido. Também ficam fora API, banco de dados e persistência.

## Upstream Artifacts Consumed
- `.github/modernize/rearchitecture/clarification.md` — decisões confirmadas de framework, idioma, CSS/assets, rotas, estado, escopo, saída e responsividade.
- `.github/modernize/rearchitecture/artifacts/project-profile.yaml` — inventário do projeto React como SPA com cinco rotas, catálogo JSON e imagens.
- `.github/modernize/rearchitecture/artifacts/t2-devops.md` — alvo .NET 8 e preparação do ambiente para o scaffold Blazor.
- `eCommApp/src/App.tsx` e componentes de `eCommApp/src/components/` — estrutura, rótulos, conteúdo e estados visuais atuais das cinco páginas.
- `eCommApp/src/App.css`, `eCommApp/src/index.css` e `eCommApp/public/products/*.json` — referência de layout/responsividade e dados estáticos.

## Evidence Mapping
- `clarification.md#Frontend` → decisões de Blazor .NET 8, idioma inglês, CSS existente, comportamento responsivo, rotas nativas e limite de acessibilidade desta fase.
- `clarification.md#Generic` → app separado, cinco páginas, dados e assets reutilizados; fluxos de negócio explicitamente adiados.
- `project-profile.yaml#project.structure` → aplicação original preservada e saída sibling `eCommApp-blazor`.
- `App.tsx#Routes` → rotas `/`, `/products`, `/login`, `/admin`, `/cart` listadas no contrato.
- `HomePage.tsx`, `ProductsPage.tsx`, `CartPage.tsx`, `LoginPage.tsx`, `AdminPage.tsx` → conteúdo e controles descritos por rota.
- `App.css` e `index.css` → shell, cores, grade, breakpoints e estrutura responsiva documentados.
- `apple.json`, `grapes.json`, `orange.json`, `pear.json` → quantidade, ordem, valores, descrição e disponibilidade do catálogo.
