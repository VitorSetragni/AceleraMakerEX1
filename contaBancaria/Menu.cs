using System.Globalization;
using ContaBancaria.Controller;
using ContaBancaria.Models;

namespace ContaBancaria;

public class Menu{

    private static readonly ContaController contas = new();

    public static void Main(string[] args){

        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

        int opcao;

        do{

            exibirMenu();
            opcao = lerInteiro("Escolha uma opção: ");
            Console.WriteLine();

            switch (opcao){

                case 1:
                    criarContaCorrente();
                    break;

                case 2:
                    criarContaPoupanca();
                    break;

                case 3:
                    contas.listarTodas();
                    break;

                case 4:
                    procurarConta();
                    break;

                case 5:
                    depositar();
                    break;

                case 6:
                    sacar();
                    break;

                case 7:
                    transferir();
                    break;

                case 8:
                    atualizarConta();
                    break;

                case 9:
                    deletarConta();
                    break;

                case 10:
                    consultarSaldo();
                    break;

                case 0:
                    Console.WriteLine("Sistema finalizado.");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (opcao != 0){

                pausar();
            }

        } while (opcao != 0);
    }

    private static void exibirMenu(){

        Console.Clear();

        Console.WriteLine("____________________________________");
        Console.WriteLine("        BANCO CONTA BANCÁRIA        ");
        Console.WriteLine("____________________________________");

        Console.WriteLine("1 - Criar conta corrente");
        Console.WriteLine("2 - Criar conta poupança");
        Console.WriteLine("3 - Listar todas as contas");
        Console.WriteLine("4 - Procurar conta por número");
        Console.WriteLine("5 - Depositar");
        Console.WriteLine("6 - Sacar");
        Console.WriteLine("7 - Transferir");
        Console.WriteLine("8 - Atualizar dados da conta");
        Console.WriteLine("9 - Deletar conta");
        Console.WriteLine("10 - Consultar saldo");
        Console.WriteLine("0 - Sair");

        Console.WriteLine("____________________________________________");
    }

    private static void criarContaCorrente(){

        Console.WriteLine("Criar Conta Corrente");

        int numero = contas.gerarNumero();
        int agencia = lerInteiro("Agência: ");
        string titular = lerTexto("Titular: ");
        float saldo = lerFloatNaoNegativo("Saldo inicial: R$ ");
        float limite = lerFloatNaoNegativo("Limite: R$ ");

        ContaCorrente conta = new ContaCorrente(numero, agencia, titular, saldo, limite);

        contas.cadastrar(conta);
    }

    private static void criarContaPoupanca(){

        Console.WriteLine("Criar Conta Poupança");

        int numero = contas.gerarNumero();
        int agencia = lerInteiro("Agência: ");
        string titular = lerTexto("Titular: ");
        float saldo = lerFloatNaoNegativo("Saldo inicial: R$ ");
        int dia = lerDiaAniversario();
        int mes = lerMesAniversario();

        ContaPoupanca conta = new ContaPoupanca(numero, agencia, titular, saldo, dia, mes);

        contas.cadastrar(conta);
    }

    private static void procurarConta(){

        int numero = lerInteiro("Número da conta: ");
        contas.procurarPorNumero(numero);
    }

    private static void depositar(){

        int numero = lerInteiro("Número da conta: ");
        float valor = lerFloat("Valor do depósito: R$ ");

        contas.depositar(numero, valor);
    }

    private static void sacar(){

        int numero = lerInteiro("Número da conta: ");
        float valor = lerFloat("Valor do saque: R$ ");

        contas.sacar(numero, valor);
    }

    private static void transferir(){

        int numeroOrigem = lerInteiro("Número da conta de origem: ");
        int numeroDestino = lerInteiro("Número da conta de destino: ");
        float valor = lerFloat("Valor da transferência: R$ ");

        contas.transferir(numeroOrigem, numeroDestino, valor);
    }

    private static void atualizarConta(){

        Console.WriteLine("Atualizar Conta");

        int numero = lerInteiro("Número da conta que deseja atualizar: ");

        Conta? contaExistente = contas.buscarNosDadosSalvos(numero);

        if (contaExistente == null){

            Console.WriteLine("Conta não encontrada.");
            return;
        }

        int agencia = lerInteiro("Nova agência: ");
        string titular = lerTexto("Novo titular: ");
        float saldoAtual = contaExistente.getSaldo();

        if (contaExistente is ContaCorrente){

            float limite = lerFloatNaoNegativo("Novo limite: R$ ");

            ContaCorrente contaAtualizada = new ContaCorrente(
                numero,
                agencia,
                titular,
                saldoAtual,
                limite
            );

            contas.atualizar(contaAtualizada);
        }
        else if (contaExistente is ContaPoupanca){

            int dia = lerDiaAniversario();
            int mes = lerMesAniversario();

            ContaPoupanca contaAtualizada = new ContaPoupanca(
                numero,
                agencia,
                titular,
                saldoAtual,
                dia,
                mes
            );

            contas.atualizar(contaAtualizada);
        }
    }

    private static void consultarSaldo(){

        int numero = lerInteiro("Número da conta: ");
        contas.consultarSaldo(numero);
    }

    private static void deletarConta(){

        int numero = lerInteiro("Número da conta: ");
        contas.deletar(numero);
    }

    private static int lerInteiro(string mensagem){

        while (true){

            Console.Write(mensagem);
            string? entrada = Console.ReadLine();

            if (int.TryParse(entrada, out int valor)){

                return valor;
            }

            Console.WriteLine("Entrada inválida. Digite um número inteiro.");
        }
    }

    private static float lerFloat(string mensagem){

        while (true){

            Console.Write(mensagem);

            string? entrada = Console.ReadLine();

            string entradaNormalizada = (entrada ?? string.Empty)
                .Trim()
                .Replace(",", ".");

            if (float.TryParse(
                entradaNormalizada,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out float valor
            )){

                return valor;
            }

            Console.WriteLine("Entrada inválida. Digite um número válido.");
        }
    }

    private static float lerFloatNaoNegativo(string mensagem){

        while (true){

            float valor = lerFloat(mensagem);

            if (valor >= 0){

                return valor;
            }

            Console.WriteLine("Valor inválido. Digite um valor maior ou igual a zero.");
        }
    }

    private static string lerTexto(string mensagem){

        while (true){

            Console.Write(mensagem);
            string? texto = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(texto)){

                return texto.Trim();
            }

            Console.WriteLine("Entrada inválida. O texto não pode ficar vazio.");
        }
    }

    private static int lerDiaAniversario(){

        while (true){

            int dia = lerInteiro("Dia de aniversário da poupança: ");

            if (dia >= 1 && dia <= 31){

                return dia;
            }

            Console.WriteLine("Dia inválido. Informe um dia entre 1 e 31.");
        }
    }

    private static int lerMesAniversario(){

        while (true){

            int mes = lerInteiro("Mês de aniversário da poupança: ");

            if (mes >= 1 && mes <= 12){

                return mes;
            }

            Console.WriteLine("Mês inválido. Informe um mês entre 1 e 12.");
        }
    }

    private static void pausar(){

        Console.WriteLine();
        Console.WriteLine("Pressione ENTER para continuar...");
        Console.ReadLine();
    }
}