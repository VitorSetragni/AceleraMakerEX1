namespace ContaBancaria.Models;

public class ContaCorrente : Conta{

    private float Limite;

    public ContaCorrente(int numero, int agencia, string titular, float saldo, float limite) 
        : base(numero, agencia, 1, titular, saldo){

        Limite = limite;
    }

    //Get e Set
    public float getLimite(){

        return Limite;
    }

    public void setLimite(float limite){

        Limite = limite;
    }

    public override bool sacar(float valor){

        if (Limite < valor){

            return false;
        }

        setSaldo(getSaldo() - valor);
        return true;
    }

    public void visualizar(){

        Console.WriteLine("____________________________________________");
        Console.WriteLine("Dados da Conta Corrente");
        Console.WriteLine("____________________________________________");
        Console.WriteLine($"Número da Conta: {getNumero()}");
        Console.WriteLine($"Agência: {getAgencia()}");
        Console.WriteLine($"Tipo da Conta: {getTipo()}");
        Console.WriteLine($"Titular: {getTitular()}");
        Console.WriteLine($"Saldo: R$ {getSaldo():F2}");
        Console.WriteLine($"Limite: R$ {Limite:F2}");
        Console.WriteLine("____________________________________________");
    }
}