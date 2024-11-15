using UnityEngine;
using UnityEngine.UI;

public class PuzzleVariavel : MonoBehaviour
{
    public Text perguntaText;
    public InputField respostaInput;
    public Button enviarButton;
    public Text feedbackText;
    public GameObject inimigo; // Referência ao inimigo na cena
    public Transform jogador; // Referência ao jogador na cena
    public GameObject minigamePanel; // Painel do minigame
    public float distanciaParaAtivar = 3f; // Distância para ativar o minigame

    private int nivelAtual = 0;
    private bool minigameAtivo = false;

    void Start()
    {
        feedbackText.text = "";
        DesativarMinigame();

        // Configura o botão de envio
        enviarButton.onClick.AddListener(VerificarResposta);

        // Certifique-se de que o painel do minigame esteja oculto inicialmente
        if (minigamePanel != null)
        {
            minigamePanel.SetActive(false);
        }
    }

    void Update()
    {
        // Verifica a distância entre o jogador e o inimigo
        if (Vector3.Distance(jogador.position, inimigo.transform.position) <= distanciaParaAtivar && !minigameAtivo)
        {
            AtivarMinigame();
        }
    }

    // Ativa o minigame, exibe a UI e carrega o nível atual
    void AtivarMinigame()
    {
        minigameAtivo = true;

        // Exibe o painel do minigame
        if (minigamePanel != null)
        {
            minigamePanel.SetActive(true);
        }

        // Exibe os elementos do minigame
        perguntaText.gameObject.SetActive(true);
        respostaInput.gameObject.SetActive(true);
        enviarButton.gameObject.SetActive(true);
        feedbackText.gameObject.SetActive(true);

        CarregarNivel();
    }

    // Desativa o minigame e esconde a UI
    void DesativarMinigame()
    {
        minigameAtivo = false;

        // Oculta o painel do minigame
        if (minigamePanel != null)
        {
            minigamePanel.SetActive(false);
        }

        // Esconde os elementos do minigame
        perguntaText.gameObject.SetActive(false);
        respostaInput.gameObject.SetActive(false);
        enviarButton.gameObject.SetActive(false);
        feedbackText.gameObject.SetActive(false);
    }

    // Função que carrega as perguntas e respostas para cada nível
    void CarregarNivel()
    {
        feedbackText.text = "";
        respostaInput.text = ""; // Limpa o campo de entrada
        switch (nivelAtual)
        {
            case 0:
                perguntaText.text = "Tutorial: Declare uma variável chamada 'idade' \ndo tipo inteiro e defina seu valor para 20.";
                break;

            case 1:
                perguntaText.text = "Dado int a = 10, defina uma variável 'resultado'\n e atribua o valor de a + 5.";
                break;

            case 2:
                perguntaText.text = "Dado int x = 3 e int y = 7, calcule o valor de \n'total' como (x + y) * 2.";
                break;

            case 3:
                perguntaText.text = "Dado int a = 4, int b = 7, e int c = 2,\n crie uma variável 'resultado' para que seu valor final seja 20.\n Use apenas operações aritméticas.";
                break;

            default:
                perguntaText.text = "Parabéns! Você derrotou o inimigo resolvendo os desafios de lógica!";
                feedbackText.text = "Pressione qualquer tecla para prosseguir na jornada.";
                inimigo.SetActive(false); // Remove o inimigo da cena
                DesativarMinigame(); // Desativa o minigame
                break;
        }
    }

    // Função para verificar a resposta do jogador com pelo menos 10 variações de resposta
    void VerificarResposta()
    {
        string resposta = respostaInput.text.Trim().ToLower().Replace(" ", ""); // Normaliza a resposta

        // Verifica as respostas corretas para cada nível com múltiplas opções
        bool estaCorreto = false;
        switch (nivelAtual)
        {
            case 0:
                if (resposta == "intidade=20;" || resposta == "idade=20;" || resposta == "intidade=20" || 
                    resposta == "intidade=20 ;" || resposta == "idade=20 ;" || resposta == "idade = 20;" ||
                    resposta == "idade= 20" || resposta == "int idade=20;" || resposta == "int idade = 20;" ||
                    resposta == "idade=20;")
                {
                    estaCorreto = true;
                }
                break;
            case 1:
                if (resposta == "intresultado=15;" || resposta == "resultado=15;" || resposta == "intresultado=15" ||
                    resposta == "resultado=15" || resposta == "int resultado=15;" || resposta == "int resultado = 15;" ||
                    resposta == "int resultado=15" || resposta == "int resultado=15" || resposta == "resultado= 15" ||
                    resposta == "resultado =15;")
                {
                    estaCorreto = true;
                }
                break;
            case 2:
                if (resposta == "inttotal=20;" || resposta == "total=20;" || resposta == "inttotal=20" ||
                    resposta == "int total=20;" || resposta == "int total = 20;" || resposta == "total= 20" ||
                    resposta == "total=20" || resposta == "int total=20" || resposta == "inttotal= 20" ||
                    resposta == "int total= 20;")
                {
                    estaCorreto = true;
                }
                break;
            case 3:
                if (resposta == "intresultado=a*b-c*a;" || resposta == "resultado=a*b-c*a;" || resposta == "resultado=a*b-c" ||
                    resposta == "int resultado=a*b-c*a;" || resposta == "intresultado=a*b-c*a" || resposta == "int resultado = a * b - c*a;" ||
                    resposta == "int resultado=a*b-c" || resposta == "resultado = a*b - c*a;" || resposta == "resultado= a*b - c*a;" ||
                    resposta == "int resultado= a*b - c*a")
                {
                    estaCorreto = true;
                }
                break;
        }

        if (estaCorreto)
        {
            feedbackText.text = "Correto! Avançando para o próximo nível.";
            nivelAtual++;
            Invoke("CarregarNivel", 1f);
        }
        else
        {
            feedbackText.text = "Errado. Tente novamente! Dica: Revise o valor da variável.";
        }
    }
}
