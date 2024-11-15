using UnityEngine;
using UnityEngine.UI;

public class MinigameCPlusPlus : MonoBehaviour
{
    public Text perguntaText;
    public InputField respostaInput;
    public Button enviarButton;
    public Text feedbackText;
    public GameObject inimigo; // Referência ao inimigo na cena
    public Transform jogador; // Referência ao jogador na cena
    public GameObject minigamePanel; // Painel do minigame
    public GameObject trofeuPanel; // Painel do troféu
    public float distanciaParaAtivar = 3f; // Distância para ativar o minigame

    private int nivelAtual = 0;
    private bool minigameAtivo = false;

    void Start()
    {
        feedbackText.text = "";
        DesativarMinigame();
        trofeuPanel.SetActive(false); // Garante que o troféu esteja oculto no início

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

    // Carrega perguntas e respostas para completar códigos em C++
    void CarregarNivel()
    {
        feedbackText.text = "";
        respostaInput.text = ""; // Limpa o campo de entrada

        switch (nivelAtual)
        {
            case 0:
                perguntaText.text = "Complete o código:\n`int a = 5;\nint b = 10;\nint soma = a + b;` \nO que falta?\n";
                break;

            case 1:
                perguntaText.text = "Complete o código:\n`if (a > b) {\n    // fazer algo\n}` \nQual é o que falta na condição?\n";
                break;

            case 2:
                perguntaText.text = "Complete o código:\n`for (int i = 0; i < 5; i++) {\n    // faça algo\n}` \nQual é o que falta na estrutura?\n";
                break;

            case 3:
                perguntaText.text = "Complete o código:\n`while (true) {\n    // executar algo\n}` \nQual condição poderia ser adicionada?\n";
                break;

            case 4:
                perguntaText.text = "Complete o código:\n`cout << \"Olá, Mundo!\";` \nQual biblioteca é necessária para usar `cout`?\n";
                break;

            case 5:
                perguntaText.text = "Complete o código:\n`int main() {\n    // seu código aqui\n}` \nQual é a estrutura correta para terminar a função?\n";
                break;

            default:
                // Quando todos os níveis são completados
                feedbackText.text = "Parabéns! Você completou todos os desafios!";
                IniciarFimDeJogo();
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
                if (resposta == "int a=5;int b=10;int soma=a+b;")
                {
                    estaCorreto = true;
                }
                break;

            case 1:
                if (resposta == "if(a>b){/*fazeralgo*/}")
                {
                    estaCorreto = true;
                }
                break;

            case 2:
                if (resposta == "for(int i=0;i<5;i++){/*façaalgo*/}")
                {
                    estaCorreto = true;
                }
                break;

            case 3:
                if (resposta == "while(true){/*executarasalgo*/}")
                {
                    estaCorreto = true;
                }
                break;

            case 4:
                if (resposta == "#include<iostream>cout<<\"olá,mundo!\";")
                {
                    estaCorreto = true;
                }
                break;

            case 5:
                if (resposta == "int main(){/*seucódigoaqui*/return0;}")
                {
                    estaCorreto = true;
                }
                break;
        }

        if (estaCorreto)
        {
            feedbackText.text = "Correto! Avançando para o próximo nível.";
            nivelAtual++;
            Invoke("ExplicarNivel", 1f);
        }
        else
        {
            feedbackText.text = "Resposta incorreta. Tente novamente! \nDica: Verifique o que está faltando no código.";
        }
    }

    // Explica o nível completado
    void ExplicarNivel()
    {
        switch (nivelAtual - 1)
        {
            case 0:
                feedbackText.text = "Explicação do Nível 0:\n\n" +
                                    "Aqui, você declarou duas variáveis inteiras, `a` e `b`, e calculou a soma delas, armazenando o resultado na variável `soma`. \n" +
                                    "Isso é essencial para entender como as operações básicas funcionam em C++.\n";
                break;

            case 1:
                feedbackText.text = "Explicação do Nível 1:\n\n" +
                                    "O comando `if` é usado para tomar decisões. Ele verifica se a condição entre parênteses (neste caso, se `a` é maior que `b`) é verdadeira. " +
                                    "Se for, o código dentro das chaves será executado. Esse conceito é fundamental para controle de fluxo.\n";
                break;

            case 2:
                feedbackText.text = "Explicação do Nível 2:\n\n" +
                                    "O loop `for` é utilizado para repetir um bloco de código um número específico de vezes. Neste caso, ele irá repetir o código dentro das chaves cinco vezes. " +
                                    "É uma maneira eficiente de executar tarefas repetitivas em C++.\n";
                break;

            case 3:
                feedbackText.text = "Explicação do Nível 3:\n\n" +
                                    "O loop `while` executa um bloco de código enquanto a condição for verdadeira. No exemplo, `true` significa que o loop vai rodar indefinidamente, " +
                                    "a menos que uma condição de parada seja implementada. É importante saber como controlar loops para evitar que seu programa trave.\n";
                break;

            case 4:
                feedbackText.text = "Explicação do Nível 4:\n\n" +
                                    "Para usar `cout` para imprimir na tela, você precisa incluir a biblioteca `<iostream>`. O `cout` é um comando que permite enviar dados para a saída padrão, " +
                                    "geralmente a tela do computador. Esse é um passo básico para interagir com o usuário em um programa.\n";
                break;

            case 5:
                feedbackText.text = "Explicação do Nível 5:\n\n" +
                                    "A função `main` é onde a execução do seu programa começa. Todo programa C++ deve ter uma função `main`. O `return 0;` indica que o programa terminou com sucesso. " +
                                    "Compreender a estrutura do programa é fundamental para qualquer programador.\n";
                Invoke("CarregarNivel", 3f); // Chama o próximo nível após 3 segundos
                break;
        }
    }

    // Inicia o fim de jogo
    void IniciarFimDeJogo()
    {
        trofeuPanel.SetActive(true); // Ativa o painel do troféu
        Invoke("EncerrarJogo", 3f); // Encerra o jogo após 3 segundos
    }

    // Método para encerrar o jogo
    void EncerrarJogo()
    {
        // Aqui você pode adicionar a lógica para encerrar o jogo, como voltar ao menu principal ou sair do aplicativo
        Application.Quit(); // Encerra o aplicativo
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Para o editor, para que o jogo pare de rodar
        #endif
    }
}
