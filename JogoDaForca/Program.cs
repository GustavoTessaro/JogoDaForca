using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;
using System.Threading;

class Program
{

    private static readonly HttpClient client = new HttpClient();

    public static async Task<string> ObterPalavraAleatoria()
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("User-Agent", "JogoDaForca");

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                string url = "https://api.dicionario-aberto.net/random";
                string responseBody = await client.GetStringAsync(url);

                if (responseBody.Trim().StartsWith("<"))
                {
                    Console.WriteLine("A API retornou HTML. Usando palavra padrão.");
                    return "DESENVOLVEDOR";
                }

                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    return doc.RootElement.GetProperty("word").GetString().ToUpper();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na requisição: {ex.Message}");
                return "erro";
            }
        }
    }

    public static void DesenharForca(int erros)
    {
        // Usamos @ antes da string para facilitar o desenho ou \\ para barras invertidas
        string[] estagios = {
        "  +---+\n  |   |\n      |\n      |\n      |\n      |",

        "  +---+\n  |   |\n  O   |\n      |\n      |\n      |",

        "  +---+\n  |   |\n  O   |\n  x   |\n      |\n      |",

        "  +---+\n  |   |\n  O   |\n /x   |\n      |\n      |",

        "  +---+\n  |   |\n  O   |\n /x\\  |\n      |\n      |",

        "  +---+\n  |   |\n  O   |\n /x\\  |\n  x   |\n      |",

        "  +---+\n  |   |\n  O   |\n /x\\  |\n  x   |\n /    |",

        "  +---+\n  |   |\n  O   |\n /x\\  |\n  x   |\n / \\  |\n",

        "  +---+\n  |   |\n  O   |\n /x\\  |\n  x   |\n / \\  |\n Vidas: ♥ ♥ X",

        "  +---+\n  |   |\n  O   |\n /x\\  |\n  x   |\n / \\  |\n Vidas: ♥ X X",

        "  +---+\n  |   |\n  O   |\n /x\\  |\n  x   |\n / \\  |\n Vidas: X X X"
        };

        Console.WriteLine(estagios[erros]);

    }

    public static string RemoverAcentos(string palavra)
    {
        var normalizada = palavra.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var c in normalizada)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    public static string escolhendoCategorias()
    {
        bool verifica = true;
        string escolha = "";
        string palavraSorteada = "";
        Random random = new Random();

        var categorias = new Dictionary<string, string[]>
    {
        { "1", new string[] { "Gato", "Cachorro", "Leao", "Tigre", "Zebra", "Cavalo", "Vaca", "Pato", "Galinha", "Coelho", "Macaco", "Elefante", "Girafa", "Peixe", "Baleia", "Rato", "Sapo", "Urso", "Lobo", "Cobra", "Aranha", "Abelha", "Formiga", "Tatu", "Coruja", "Pomba", "Jacare", "Ovelha", "Porco", "Galo" }},
        { "2", new string[] { "Maca", "Banana", "Uva", "Laranja", "Limao", "Pera", "Manga", "Abacaxi", "Melancia", "Morango", "Amora", "Cereja", "Coco", "Kiwi", "Mamao", "Melao", "Ameixa", "Caju", "Goiaba", "Jabuticaba", "Jaca", "Figo", "Framboesa", "Maracuja", "Nectarina", "Pitaya", "Roma", "Tangerina", "Abacate", "Acerola" }},
        { "3", new string[] { "Cadeira", "Mesa", "Cama", "Porta", "Janela", "Garfo", "Faca", "Prato", "Copo", "Chave", "Caneta", "Lapis", "Livro", "Bolsa", "Relogio", "Escova", "Balde", "Martelo", "Tesoura", "Sabonete", "Toalha", "Vaso", "Quadro", "Espelho", "Tapete", "Lampada", "Pente", "Caderno", "Gaveta" }},
        { "4", new string[] { "Brasil", "Angola", "Canada", "Egito", "Franca", "Grecia", "Italia", "Japao", "Mexico", "Russia", "Suica", "China", "Chile", "India", "Portugal", "Espanha", "Alemanha", "Argentina", "Belgica", "Catar", "Cuba", "Gana", "Iraque", "Israel", "Jamaica", "Marrocos", "Noruega", "Peru", "Turquia", "Uruguai" }}
    };

        while (verifica == true)
        {
            Console.Clear();
            Console.WriteLine("Categorias: ");
            Console.WriteLine("1 - Animais");
            Console.WriteLine("2 - Frutas");
            Console.WriteLine("3 - Objetos");
            Console.WriteLine("4 - Países");
            Console.WriteLine("5 - Voltar");
            Console.Write("Digite o número da categoria: ");

            escolha = Console.ReadLine() ?? "";

            if (categorias.ContainsKey(escolha))
            {
                string[] listaSelecionada = categorias[escolha];

                int indiceAleatorio = random.Next(0, listaSelecionada.Length);

                palavraSorteada = listaSelecionada[indiceAleatorio].ToUpper();

                verifica = false;
            }
            else if (escolha == "5")
            {
                palavraSorteada = "VOLTAR";
                verifica = false;
            }
            else
            {
                Console.WriteLine("Opção inválida!");
                Thread.Sleep(1000);
                Console.Clear();

            }

        }

        return palavraSorteada;
    }
    static void Main(string[] args)
    {

        int dificuldade = 0;
        int tentativasRestantes = 0;
        String palavra = "";
        bool jogarNovamente = true;
        bool escolhaFacil = false;

        Console.WriteLine("Bem-vindo ao Jogo da Forca!");

        while (jogarNovamente == true)
        {
            palavra = "";
            escolhaFacil = false;
            dificuldade = 0;

            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Escolha a dificuldade:");
            Console.WriteLine("1 - Fácil (10 tentativas e Escolher Categoria do Banco de Dados)");
            Console.WriteLine("2 - Médio (7 tentativas e Palavra Aleatória API)");
            Console.WriteLine("3 - Difícil (5 tentativas SEM PERNAS e Palavra Aleatória API)");
            Console.WriteLine("4 - Sair");

            while (dificuldade < 1 || dificuldade > 4)
            {
                Console.Write("Digite o número da dificuldade desejada: ");
                if (int.TryParse(Console.ReadLine(), out dificuldade))
                {
                    if (dificuldade < 1 || dificuldade > 4)
                    {
                        Console.WriteLine("Dificuldade inválida. Por favor, escolha novamente.");
                    }

                    if (dificuldade == 1)
                    {
                        palavra = escolhendoCategorias();
                        tentativasRestantes = 10;
                        escolhaFacil = true;
                    }
                    else if (dificuldade == 2)
                    {
                        tentativasRestantes = 7;
                    }
                    else if (dificuldade == 3)
                    {
                        tentativasRestantes = 5;
                    }

                    if (dificuldade == 4)
                    {
                        Console.WriteLine("Saindo do jogo. Até a próxima!");
                        Environment.Exit(0);
                    }

                }
                else
                {
                    Console.WriteLine("Entrada inválida. Por favor, digite um número.");
                }
            }

            if(palavra == "VOLTAR")
            {
                continue;
            }

            if(escolhaFacil == false)
            {
                palavra = ObterPalavraAleatoria().Result;
            }

            palavra = RemoverAcentos(palavra).ToUpper();

            char[] vetorLetras = palavra.ToCharArray();
            char[] vetorLetrasAdivinhadas = new char[vetorLetras.Length];

            List<char> letrasErradas = new List<char>();

            for (int i = 0; i < vetorLetras.Length; i++)
            {
                vetorLetrasAdivinhadas[i] = '_';

                if (vetorLetras[i] == '-')
                {
                    vetorLetrasAdivinhadas[i] = '-';
                }

            }

            int erros = 0;
            bool chutouPalavra = false;

            while (tentativasRestantes > 0)
            {
                Console.Clear();
                DesenharForca(erros);
                Console.WriteLine("\nPalavra: " + new string(vetorLetrasAdivinhadas));
                Console.WriteLine("Tentativas restantes: " + tentativasRestantes);
                Console.WriteLine("Letras erradas: " + string.Join(", ", letrasErradas));
                Console.Write("Digite uma letra ou digite 1 para chutar uma palavra inteira (UMA TENTATIVA): ");
                char letra = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                bool verifica = true;

                if (letra == '1')
                {
                    while (verifica == true)
                    {

                        Console.Write("Digite a palavra secreta (se houver espaços digite '-' no lugar): ");
                        string? palavraChutada = Console.ReadLine();
                        palavraChutada = palavraChutada.ToUpper();

                        if (palavraChutada == palavra)
                        {

                            Console.WriteLine("\nParabéns! Você adivinhou a palavra: " + palavra);
                            verifica = false;

                        }
                        else
                        {
                            Console.WriteLine("\nGame over! A palavra era: " + palavra);
                            verifica = false;

                        }
                    }

                }

                if (verifica == false)
                {
                    chutouPalavra = true;
                    break;

                }

                bool acertou = false;

                for (int i = 0; i < vetorLetras.Length; i++)
                {
                    if (vetorLetras[i] == letra)
                    {
                        vetorLetrasAdivinhadas[i] = letra;
                        acertou = true;
                    }
                }

                if (!acertou)
                {
                    if (!letrasErradas.Contains(letra))
                    {
                        letrasErradas.Add(letra);
                        tentativasRestantes--;
                        erros++;
                        Console.WriteLine("Letra incorreta!");
                        Thread.Sleep(500);
                    }
                    else
                    {
                        Console.WriteLine("Você já tentou essa letra incorreta antes!");
                        Thread.Sleep(500);
                    }
                }
                else
                {
                    Console.WriteLine("Letra correta!");
                    Thread.Sleep(500);
                }

                if (new string(vetorLetrasAdivinhadas) == palavra)
                {
                    Console.WriteLine("\nParabéns! Você adivinhou a palavra: " + palavra);
                    break;
                }
            }

            if (new string(vetorLetrasAdivinhadas) != palavra && chutouPalavra == false)
            {
                Console.WriteLine("\nGame over! A palavra era: " + palavra);
            }

            Console.WriteLine("\nDeseja jogar novamente? (s/n)");
            string resposta = Console.ReadLine().ToLower() ?? "";

            if (resposta != "s")
            {
                jogarNovamente = false;
                Console.WriteLine("Saindo do jogo. Até a próxima!");
            }
            else
            {
                dificuldade = 0;
                Console.Clear();
            }

        }

    }
}