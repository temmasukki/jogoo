using UnityEngine;
using UnityEngine.UI;

public class PuzzleSwitchCase : MonoBehaviour
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

    // Carrega perguntas e respostas sobre switch case
    void CarregarNivel()
    {
        feedbackText.text = "";
        respostaInput.text = ""; // Limpa o campo de entrada

        switch (nivelAtual)
        {
            case 0:
                perguntaText.text = "Tutorial: Escreva um switch que verifica o \nvalor de uma variável chamada 'opcao'.";
                break;

            case 1:
                perguntaText.text = "Defina os casos para '1', '2' e '3',\n e exiba mensagens diferentes para cada um.";
                break;

            case 2:
                perguntaText.text = "Como você pode usar o 'default' para lidar com valores não esperados?";
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
            case 0:
                if (resposta == "switch(opcao){" || resposta == "switch(opcao) {")
                {
                    estaCorreto = true;
                }
                break;

            case 1:
                if (resposta.Contains("case 1:") && resposta.Contains("case 2:") && resposta.Contains("case 3:"))
                {
                    estaCorreto = true;
                }
                break;

            case 2:
                if (resposta.Contains("default:"))
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
            feedbackText.text = "Resposta incorreta. Tente novamente! \nDica: Verifique a sintaxe e os casos.";
        }
    }
}
