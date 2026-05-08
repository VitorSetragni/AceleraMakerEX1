using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContaBancaria.Models;
using ContaBancaria.Repository;

namespace ContaBancaria.Controller;

public class ContaController : IContaRepository{

    private readonly List<Conta> contas = new();
    private readonly string caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "contas.json");

    public ContaController(){

        carregarDados();
    }

    public void procurarPorNumero(int numero){

        Conta? conta = buscarNosDadosSalvos(numero);

        if (conta == null){

            Console.WriteLine("Conta não encontrada.");
            return;
        }

        visualizarConta(conta);
    }

    public void listarTodas(){

        if (contas.Count == 0){

            Console.WriteLine("Nenhuma conta cadastrada.");
            return;
        }

        foreach (Conta conta in contas){

            visualizarConta(conta);
        }
    }

    public void cadastrar(Conta conta){

        if (buscarNosDadosSalvos(conta.getNumero()) != null){

            Console.WriteLine("Já existe uma conta com esse número.");
            return;
        }

        contas.Add(conta);
        salvarDados();

        Console.WriteLine("Conta cadastrada com sucesso.");
        Console.WriteLine($"Número da conta criada: {conta.getNumero()}");
    }

    public void atualizar(Conta conta){

        Conta? contaAtual = buscarNosDadosSalvos(conta.getNumero());

        if (contaAtual == null){

            Console.WriteLine("Conta não encontrada.");
            return;
        }

        int indice = contas.IndexOf(contaAtual);
        contas[indice] = conta;

        salvarDados();

        Console.WriteLine("Conta atualizada com sucesso.");
    }

    public void deletar(int numero){

        Conta? conta = buscarNosDadosSalvos(numero);

        if (conta == null){

            Console.WriteLine("Conta não encontrada.");
            return;
        }

        contas.Remove(conta);
        salvarDados();

        Console.WriteLine("Conta deletada com sucesso.");
    }

    public void sacar(int numero, float valor){

        Conta? conta = buscarNosDadosSalvos(numero);

        if (conta == null){

            Console.WriteLine("Conta não encontrada.");
            return;
        }

        if (valor <= 0){

            Console.WriteLine("Saque não realizado. Informe um valor maior que zero.");
            return;
        }

        bool saqueRealizado = conta.sacar(valor);

        if (!saqueRealizado){

            Console.WriteLine("Saque não realizado. Verifique o saldo ou limite.");
            return;
        }

        salvarDados();

        Console.WriteLine("Saque realizado com sucesso.");
    }

    public void depositar(int numero, float valor){

        Conta? conta = buscarNosDadosSalvos(numero);

        if (conta == null){

            Console.WriteLine("Conta não encontrada.");
            return;
        }

        if (valor <= 0){

            Console.WriteLine("Depósito não realizado. Informe um valor maior que zero.");
            return;
        }

        conta.depositar(valor);

        salvarDados();

        Console.WriteLine("Depósito realizado com sucesso.");
    }

    public void transferir(int numeroOrigem, int numeroDestino, float valor){

        Conta? contaOrigem = buscarNosDadosSalvos(numeroOrigem);
        Conta? contaDestino = buscarNosDadosSalvos(numeroDestino);

        if (contaOrigem == null || contaDestino == null){

            Console.WriteLine("Conta de origem ou destino não encontrada.");
            return;
        }

        if (numeroOrigem == numeroDestino){

            Console.WriteLine("A conta de origem não pode ser igual à conta de destino.");
            return;
        }

        if (valor <= 0){

            Console.WriteLine("Transferência não realizada. Informe um valor maior que zero.");
            return;
        }

        bool saqueRealizado = contaOrigem.sacar(valor);

        if (!saqueRealizado){

            Console.WriteLine("Transferência não realizada. Verifique o saldo ou limite.");
            return;
        }

        contaDestino.depositar(valor);

        salvarDados();

        Console.WriteLine("Transferência realizada com sucesso.");
    }

    public int gerarNumero(){

        if (contas.Count == 0){

            return 1;
        }

        return contas.Max(conta => conta.getNumero()) + 1;
    }

    public Conta? buscarNosDadosSalvos(int numero){

        return contas.FirstOrDefault(conta => conta.getNumero() == numero);
    }

    private void visualizarConta(Conta conta){

        if (conta is ContaCorrente contaCorrente){

            contaCorrente.visualizar();
        }
        else if (conta is ContaPoupanca contaPoupanca){

            contaPoupanca.visualizar();
        }
        else{

            Console.WriteLine("Tipo de conta desconhecido.");
        }
    }

    private void salvarDados(){

        List<ContaArquivo> dados = contas.Select(conta => new ContaArquivo{

            Numero = conta.getNumero(),
            Agencia = conta.getAgencia(),
            Tipo = conta.getTipo(),
            Titular = conta.getTitular(),
            Saldo = conta.getSaldo(),
            Limite = conta is ContaCorrente corrente ? corrente.getLimite() : 0,
            DiaAniversario = conta is ContaPoupanca poupanca ? poupanca.getDiaAniversario() : 0,
            MesAniversario = conta is ContaPoupanca poupancaMes ? poupancaMes.getMesAniversario() : 0
        }).ToList();

        JsonSerializerOptions options = new(){

            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(dados, options);

        File.WriteAllText(caminhoArquivo, json);
    }

    public void consultarSaldo(int numero){

        Conta? conta = buscarNosDadosSalvos(numero);

        if (conta == null){

            Console.WriteLine("Conta não encontrada.");
            return;
        }

        Console.WriteLine("____________________________________________");
        Console.WriteLine("Consulta de Saldo");
        Console.WriteLine("____________________________________________");
        Console.WriteLine($"Número da Conta: {conta.getNumero()}");
        Console.WriteLine($"Titular: {conta.getTitular()}");
        Console.WriteLine($"Saldo atual: R$ {conta.getSaldo():F2}");
        Console.WriteLine("____________________________________________");
    }

    private void carregarDados(){

        if (!File.Exists(caminhoArquivo)){

            return;
        }

        string json = File.ReadAllText(caminhoArquivo);

        if (string.IsNullOrWhiteSpace(json)){

            return;
        }

        List<ContaArquivo>? dados = JsonSerializer.Deserialize<List<ContaArquivo>>(json);

        if (dados == null){

            return;
        }

        foreach (ContaArquivo item in dados){

            if (item.Tipo == 1){

                contas.Add(new ContaCorrente(
                    item.Numero,
                    item.Agencia,
                    item.Titular,
                    item.Saldo,
                    item.Limite
                ));
            }
            else if (item.Tipo == 2){

                contas.Add(new ContaPoupanca(
                    item.Numero,
                    item.Agencia,
                    item.Titular,
                    item.Saldo,
                    item.DiaAniversario,
                    item.MesAniversario
                ));
            }
        }
    }

    private class ContaArquivo{

        public int Numero { get; set; }
        public int Agencia { get; set; }
        public int Tipo { get; set; }
        public string Titular { get; set; } = string.Empty;
        public float Saldo { get; set; }
        public float Limite { get; set; }
        public int DiaAniversario { get; set; }
        public int MesAniversario { get; set; }
    }
}