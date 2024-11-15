using UnityEngine;
using UnityEngine.UI;

public class MinigameVariaveis : MonoBehaviour
{
    public Text perguntaText;
    public InputField respostaInput;
    public Button enviarButton;
    public Text feedbackText;
    public GameObject inimigo;
    public Transform jogador;
    public GameObject minigamePanel;
    public float distanciaParaAtivar = 3f;

    private int nivelAtual = 0;
    private bool minigameAtivo = false;

    void Start()
    {
        feedbackText.text = "";
        DesativarMinigame();

        enviarButton.onClick.AddListener(VerificarResposta);

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

    void CarregarNivel()
    {
        feedbackText.text = "";
        respostaInput.text = "";

        switch (nivelAtual)
        {
            case 0:
                perguntaText.text = "Tutorial: Declare uma string chamada 'nome' e atribua \"João\" usando cin.\nExemplo: cout << \"Digite seu nome:\"; cin >> nome;";
                break;

            case 1:
                perguntaText.text = "Declare uma string 'cidade' e atribua \"São Paulo\" usando cin.\nExemplo: cout << \"Digite sua cidade:\"; cin >> cidade;";
                break;

            case 2:
                perguntaText.text = "Atribua a letra 'A' a uma variável char chamada 'letraInicial'.\nExemplo: cout << \"Digite a letra inicial:\"; cin >> letraInicial;";
                break;

            case 3:
                perguntaText.text = "Declare uma string 'saudacao' e atribua \"Bom dia!\" usando cin.\nExemplo: cout << \"Digite a saudação:\"; cin >> saudacao;";
                break;

            default:
                perguntaText.text = "Parabéns! Você completou o desafio!";
                feedbackText.text = "Pressione qualquer tecla para continuar.";
                inimigo.SetActive(false);
                DesativarMinigame();
                break;
        }
    }

    void VerificarResposta()
    {
        string resposta = respostaInput.text.Trim().ToLower().Replace(" ", "");

        bool estaCorreto = false;
        switch (nivelAtual)
        {
            case 0:
                if (resposta == "stringnome=\"joão\";" || resposta == "string nome=\"joão\";" ||
                    resposta == "nome=\"joão\";")
                {
                    estaCorreto = true;
                }
                break;

            case 1:
                if (resposta == "stringcidade=\"sãopaulo\";" || resposta == "string cidade=\"sãopaulo\";" ||
                    resposta == "cidade=\"sãopaulo\";" || resposta == "cidade=\"Sãopaulo\";"|| resposta == "cidade=\"SP\";" ||
                     resposta == "cidade=\"sp\";"|| resposta == "cidade=\"Sao Paulo\";"|| resposta == "cidade=\"São Paulo\";")
                {
                    estaCorreto = true;
                }
                break;

            case 2:
                if (resposta == "charletrainicial='a';" || resposta == "char letraInicial='A';" ||
                    resposta == "letrainicial='a';")
                {
                    estaCorreto = true;
                }
                break;

            case 3:
                if (resposta == "stringsaudacao=\"bomdia!\";" || resposta == "string saudacao=\"bomdia!\";" ||
                    resposta == "saudacao=\"bomdia!\";")
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
            feedbackText.text = "Resposta incorreta. Tente novamente! \nDica: Verifique o valor e tipo da variável.";
        }
    }
}
