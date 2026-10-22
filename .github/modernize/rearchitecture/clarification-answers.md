---
schema: clarification-answers/v1
status: submitted
generated_at: "2026-10-22T15:03:39.905Z"
scope: [frontend, generic]
questions_file: clarification-questions.json
---

# Rearchitecture Clarification Answers

## 🖥️ Frontend

- **F1** Qual versão do .NET deve ser usada para o Blazor Web App?
  - answer: Blazor Web App com .NET 8
  - source: user
- **F2** A interface deve usar alguma biblioteca de componentes ou somente o CSS existente?
  - answer: Sem biblioteca; reaproveitar CSS atual
  - source: user
- **F3** Há screenshots/recordings para fornecer ou devemos usar as páginas React e os arquivos CSS existentes como referência visual?
  - answer: Usar as páginas em eCommApp/src/components e os estilos eCommApp/src/App.css e eCommApp/src/index.css como referência; nenhum screenshot foi anexado nesta conversa.
  - source: user
- **F4** Confirme a origem visual para reproduzir o app: CSS atual, imagens e estilos extraídos do projeto React?
  - answer: Reaproveitar e adaptar o CSS e as imagens existentes; manter o layout visual do projeto atual.
  - source: user
- **F5** Qual padrão de acessibilidade devemos mirar nesta reprodução?
  - answer: Sem requisito formal.  Permanecer como ja está, isso será uma nova etapa posteriormente.
  - source: user
- **F6** Quais navegadores devem ser considerados para a demonstração?
  - answer: modern evergreen (Chrome, Firefox, Safari, Edge — latest 2 major versions)
  - source: user
- **F7** Como tratar responsividade nesta etapa?
  - answer: Preservar comportamento responsivo e breakpoints do CSS atual
  - source: user
- **F8** Quais idiomas devem aparecer na interface Blazor?
  - answer: Manter o idioma atual da interface (inglês)
  - source: user
- **F9** Qual abordagem de estado deve ser usada no novo Blazor?
  - answer: Estado simples em memória, sem biblioteca externa; sem persistência nesta etapa.
  - source: user
- **F10** Como preservar as URLs das cinco páginas no Blazor?
  - answer: Roteamento nativo do Blazor preservando as cinco rotas atuais.
  - source: user

## 📋 General

- **G1** O que define o sucesso desta primeira etapa?
  - answer: Criar o app C# Blazor em diretório separado e reproduzir layout, navegação e as cinco páginas usando CSS, imagens e JSON existentes; sem implementar os fluxos funcionais das etapas 2 e 3.
  - source: user
- **G2** Confirme o que deve permanecer fora desta etapa.
  - answer: Backend/API, banco de dados, autenticação real, persistência e implementação dos fluxos de carrinho, avaliações, promoção e checkout; esses fluxos são etapas posteriores.
  - source: user
- **G3** Como tratar os testes React durante a criação do novo projeto?
  - answer: Manter o React intacto e exigir que os testes existentes continuem passando.
  - source: user
- **G4** Onde criar o novo projeto Blazor?
  - answer: eCommApp-blazor (novo diretório irmão de eCommApp)
  - source: user
- **G5** Há alguma restrição adicional para esta etapa?
  - answer: Não implementar a loja como serviço real; é um programa de treinamento e demonstração. Preservar o app React original.
  - source: user
