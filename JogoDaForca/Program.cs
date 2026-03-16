using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;

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
    static void Main(string[] args)
    {

        int dificuldade = 0;
        int tentativasRestantes = 0;
        String palavra = "";
        bool jogarNovamente = true;

        Console.WriteLine("Bem-vindo ao Jogo da Forca!");

        while (jogarNovamente == true)
        {

            Console.WriteLine("");
            Console.WriteLine("Escolha a dificuldade:");
            Console.WriteLine("1 - Fácil (10 tentativas)");
            Console.WriteLine("2 - Médio (7 tentativas)");
            Console.WriteLine("3 - Difícil (5 tentativas)");
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
                        tentativasRestantes = 10;
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

            palavra = ObterPalavraAleatoria().Result;
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

            while (tentativasRestantes > 0)
            {
                Console.WriteLine("\nPalavra: " + new string(vetorLetrasAdivinhadas));
                Console.WriteLine("Tentativas restantes: " + tentativasRestantes);
                Console.WriteLine("Letras erradas: " + string.Join(", ", letrasErradas));
                Console.Write("Digite uma letra: ");
                char letra = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

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
                        Console.WriteLine("Letra incorreta!");
                    }
                    else
                    {
                        Console.WriteLine("Você já tentou essa letra incorreta antes!");
                    }
                }
                else
                {
                    Console.WriteLine("Letra correta!");
                }

                if (new string(vetorLetrasAdivinhadas) == palavra)
                {
                    Console.WriteLine("\nParabéns! Você adivinhou a palavra: " + palavra);
                    break;
                }
            }

            if (new string(vetorLetrasAdivinhadas) != palavra)
            {
                Console.WriteLine("\nGame over! A palavra era: " + palavra);
            }

            Console.WriteLine("\nDeseja jogar novamente? (s/n)");
            string resposta = Console.ReadLine().ToLower() ?? "";

            if (resposta != "s")
            {
                jogarNovamente = false;
            }
            else
            {
                dificuldade = 0;
            }

        }

    }
}