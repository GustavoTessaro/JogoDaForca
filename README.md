# 🎮 Jogo da Forca em C

Este projeto é uma implementação simples do **Jogo da Forca** em **C#**,
executado no **console**. O jogo obtém **palavras aleatórias através de
uma API online**, remove acentos para facilitar a adivinhação e permite
escolher diferentes **níveis de dificuldade**.

------------------------------------------------------------------------

# 📌 Funcionalidades

-   🎲 Palavra aleatória obtida de uma API
-   🔤 Remoção automática de acentos
-   🎯 Sistema de tentativas baseado em dificuldade
-   🔁 Opção de jogar novamente
-   ⚠️ Tratamento de erros da API
-   🖥️ Interface simples via console

------------------------------------------------------------------------

# 🧠 Como o jogo funciona

1.  O jogador inicia o programa.
2.  Escolhe o **nível de dificuldade**.
3.  O sistema busca uma **palavra aleatória na API**.
4.  A palavra é exibida como \*\*\_ \_ \_ \_ \_\*\*.
5.  O jogador digita **letras para tentar descobrir a palavra**.
6.  Cada erro **reduz o número de tentativas**.
7.  O jogo termina quando:
    -   o jogador descobre a palavra
    -   as tentativas acabam
8.  Ao final é possível **jogar novamente**.

------------------------------------------------------------------------

## ⚙️ Dificuldades

| Nível | Nome | Tentativas |
|------|------|------------|
| 1 | Fácil | 10 |
| 2 | Médio | 7 |
| 3 | Difícil | 5 |
| 4 | Sair | Encerra o jogo |

------------------------------------------------------------------------

# 🌐 API utilizada

O jogo utiliza a API:

https://api.dicionario-aberto.net/random

Ela retorna uma palavra aleatória em formato JSON.

Exemplo:

``` json
{
 "word": "exemplo"
}
```

Caso a API falhe ou retorne HTML, o jogo usa a palavra padrão:

    DESENVOLVEDOR

------------------------------------------------------------------------

# 📂 Estrutura do Código

    Program
     ├── ObterPalavraAleatoria()
     ├── RemoverAcentos()
     └── Main()

------------------------------------------------------------------------

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

------------------------------------------------------------------------

## RemoverAcentos()

Remove acentos das palavras.

Exemplos:

| Antes | Depois |
|------|------|
| ação | acao |
| café | cafe |
| programação | programacao |

Isso evita que o jogador precise digitar letras acentuadas.

------------------------------------------------------------------------

## Main()

Função principal do programa onde acontece toda a lógica do jogo.

Etapas:

- Mostra o menu de dificuldade
- Obtém a palavra da API
- Remove acentos
- Cria os vetores de letras
- Executa o loop principal do jogo
- Verifica vitória ou derrota
- Pergunta se o jogador deseja jogar novamente

------------------------------------------------------------------------

# ▶️ Como executar

## Pelo Visual Studio

Abra o projeto e pressione:

    Ctrl + F5

## Pelo terminal (.NET CLI)

    dotnet run

------------------------------------------------------------------------

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

------------------------------------------------------------------------

# 📦 Tecnologias utilizadas

-   C#
-   .NET
-   System.Net.Http
-   System.Text.Json

------------------------------------------------------------------------

# 🔮 Possíveis melhorias

-   Desenho da forca em ASCII
-   Sistema de pontuação
-   Mostrar letras já utilizadas
-   Banco de palavras local
-   Interface gráfica

