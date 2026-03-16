# 🎮 Jogo da Forca em C

Este projeto é uma implementação simples do **Jogo da Forca** em **C#**,
executado no **console**. O jogo obtém **palavras aleatórias através de
uma API online**, remove acentos para facilitar a adivinhação e permite
escolher diferentes **níveis de dificuldade**.

---

# 📌 Funcionalidades

- 🎲 Palavra aleatória obtida de uma API
- 🔤 Remoção automática de acentos
- 🎯 Sistema de tentativas baseado em dificuldade
- 🎨 Desenho da forca em ASCII no console
- ❌ Exibição das letras erradas já utilizadas
- 🔁 Opção de jogar novamente
- 🧹 Limpeza da tela a cada rodada para melhor visualização
- ⚠️ Tratamento de erros da API

---

# 🧠 Como o jogo funciona

1.  O jogador inicia o programa.
2.  Escolhe o **nível de dificuldade**.
3.  O sistema busca uma **palavra aleatória na API**.
4.  A palavra é exibida como \*\*\_ \_ \_ \_ \_\*\*.
5.  O jogador digita **letras para tentar descobrir a palavra**.
6.  Cada erro **reduz o número de tentativas**.
7.  O jogo termina quando:
    - o jogador descobre a palavra
    - as tentativas acabam
8.  Ao final é possível **jogar novamente**.

---

## ⚙️ Dificuldades

| Nível | Nome                 | Tentativas     |
| ----- | -------------------- | -------------- |
| 1     | Fácil                | 10             |
| 2     | Médio                | 7              |
| 3     | Difícil (sem pernas) | 5              |
| 4     | Sair                 | Encerra o jogo |

---

# 🌐 API utilizada

O jogo utiliza a API:

https://api.dicionario-aberto.net/random

Ela retorna uma palavra aleatória em formato JSON.

Exemplo:

```json
{
  "word": "exemplo"
}
```

Caso a API falhe ou retorne HTML, o jogo usa a palavra padrão:

    DESENVOLVEDOR

---

## 📂 Estrutura do Código

Program
├── ObterPalavraAleatoria()
├── DesenharForca()
├── RemoverAcentos()
└── Main()

---

# 🔎 Explicação das Funções

## ObterPalavraAleatoria()

Responsável por buscar uma palavra aleatória da API.

Etapas:

1. Cria um `HttpClient`
2. Faz requisição **GET** para a API
3. Verifica se a resposta é JSON
4. Extrai a propriedade `"word"`
5. Converte para letras maiúsculas

Caso ocorra erro, o programa retorna uma palavra padrão.

---

## RemoverAcentos()

Remove acentos das palavras.

Exemplos:

| Antes       | Depois      |
| ----------- | ----------- |
| ação        | acao        |
| café        | cafe        |
| programação | programacao |

Isso evita que o jogador precise digitar letras acentuadas.

---

## DesenharForca()

Responsável por desenhar a forca no console utilizando arte ASCII.

A função recebe o número de erros cometidos pelo jogador e exibe o estágio correspondente do boneco da forca.

Cada erro adiciona uma nova parte ao desenho.

Exemplo de estágio inicial:

+---------+

| |

|

|

|

|

Conforme os erros aumentam, o personagem é desenhado até o estado final de derrota.

---

## Main()

Função principal do programa onde acontece toda a lógica do jogo.

Etapas:

1. Mostra o menu de dificuldade
2. Obtém a palavra da API
3. Remove acentos
4. Cria os vetores de letras
5. Executa o loop principal do jogo:
    - Limpa a tela do console
    - Desenha o estado atual da forca
    - Mostra a palavra parcialmente descoberta
    - Mostra as letras erradas já utilizadas
    - Solicita uma nova letra ao jogador
    - Verifica se a letra existe na palavra
    - Atualiza o progresso ou registra um erro
  6. Verifica vitória ou derrota
  7. Pergunta se o jogador deseja jogar novamente

---

# ▶️ Como executar

## Pelo Visual Studio

Abra o projeto e pressione:

    Ctrl + F5

## Pelo terminal (.NET CLI)

    dotnet run

---

# 💻 Exemplo de execução

    Bem-vindo ao Jogo da Forca!

    Escolha a dificuldade:
    1 - Fácil
    2 - Médio
    3 - Difícil
    4 - Sair

    Digite o número da dificuldade desejada: 1

    Palavra: ______
    Tentativas restantes: 10
    Digite uma letra: A

---

# 📦 Tecnologias utilizadas

- C#
- .NET
- System.Net.Http
- System.Text.Json

---

# 🔮 Possíveis melhorias

- Sistema de pontuação
- Mostrar letras já utilizadas corretamente
- Banco de palavras local
- Interface gráfica
- Multiplayer local
- Ranking de jogadores
