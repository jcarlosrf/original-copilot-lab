## [t1] Especificação da experiência Blazor da fase 1
- Descoberta: cinco páginas correspondem às rotas `/`, `/products`, `/cart`, `/login` e `/admin`; a navegação do cabeçalho tem quatro links e não inclui `/admin` diretamente.
- Correção de escopo: manter controles para paridade visual não autoriza implementar carrinho, reviews, login, promoção ou checkout; a clarificação reserva esses efeitos para fases seguintes.
- Padrão reutilizável: especificar dados visíveis e estados da UI com nomes/valores derivados dos componentes e JSON atuais; não definir implementação C# na responsabilidade UX.
- Learnings consumed: (none)

## [t6] Revisão UX de paridade e responsividade
- Runtime: cinco rotas diretas e navegação por links verificadas em Chromium; nenhuma apresentou overflow horizontal a 320, 768 ou 1280 px CSS.
- Recursos: `storefront.css` e os quatro JSONs do Blazor são idênticos por hash às fontes React; imagens e labels também foram verificadas no browser.
- Ações de negócio inertes e `/admin` sem login continuam alinhadas ao escopo de fase 1, não são regressões.
- Learnings consumed: [ux/blazor-phase-one-scope]
