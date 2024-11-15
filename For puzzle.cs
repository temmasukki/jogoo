using UnityEngine;
using UnityEngine.UI;

public class PuzzleFor : MonoBehaviour
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

        // Oculta o painel do minigame inicialmente
        if (minigamePanel != null)
        {
            minigamePanel.SetActive(false);
        }
    }

    void Update()
    {
        if (Vector3.Distance(jogador.position, inimigo.transform.position) <= distanciaParaAtivar && !minigameAtivo)
        {
            AtivarMinigame();
        }
    }

    // Ativa o minigame, exibe a UI e carrega o nível atual
    void AtivarMinigame()
    {
        minigameAtivo = true;

        if (minigamePanel != null)
        {
            minigamePanel.SetActive(true);
        }

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

        if (minigamePanel != null)
        {
            minigamePanel.SetActive(false);
        }

        perguntaText.gameObject.SetActive(false);
        respostaInput.gameObject.SetActive(false);
        enviarButton.gameObject.SetActive(false);
        feedbackText.gameObject.SetActive(false);
    }

    // Carrega perguntas e respostas para o laço `for`
    void CarregarNivel()
    {
        feedbackText.text = "";
        respostaInput.text = ""; // Limpa o campo de entrada

        switch (nivelAtual)
        {
        
            case 0:
                perguntaText.text = "Nível 1: Use um laço for para contar de 0 até 3.\n" +
                                    "Digite a sequência de números que você espera ver.\n" +
                                    "Exemplo: 0, 1, 2, 3\n";
                break;

            case 1:
                perguntaText.text = "Nível 2: Use um laço for para contar de 1 até 5.\n" +
                                    "Digite a sequência de números que você espera ver.\n" +
                                    "Exemplo: 1, 2, 3, 4, 5\n";
                break;

            case 2:
                perguntaText.text = "Nível 3: Use um laço for para contar de 2 até 10, pulando de 2 em 2.\n" +
                                    "Digite a sequência de números que você espera ver.\n" +
                                    "Exemplo: 2, 4, 6, 8, 10\n";
                break;

            case 3:
                perguntaText.text = "Nível 4: Use um laço for para contar de 5 até 1 (decrescendo).\n" +
                                    "Digite a sequência de números que você espera ver.\n" +
                                    "Exemplo: 5, 4, 3, 2, 1\n";
                break;

            default:
                perguntaText.text = "Parabéns! Você completou o desafio!";
                feedbackText.text = "Pressione qualquer tecla para continuar.";
                inimigo.SetActive(false);
                DesativarMinigame();
                break;
        }
    }

    // Verifica a resposta do jogador
    void VerificarResposta()
    {
        string resposta = respostaInput.text.Trim().ToLower().Replace(" ", "");

        bool estaCorreto = false;
        switch (nivelAtual)
        {
            case 0: // Nível 1
                if (resposta == "0,1,2,3")
                {
                    estaCorreto = true;
                }
                break;

            case 1: // Nível 2
                if (resposta == "1,2,3,4,5")
                {
                    estaCorreto = true;
                }
                break;

            case 2: // Nível 3
                if (resposta == "2,4,6,8,10")
                {
                    estaCorreto = true;
                }
                break;

            case 3: // Nível 4
                if (resposta == "5,4,3,2,1")
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
            feedbackText.text = "Resposta incorreta. Tente novamente! \nDica: Verifique a sequência de números.";
        }
    }
}
