namespace ContaBancaria.Models;

public class ContaPoupanca : Conta{

    private int DiaAniversario;
    private int MesAniversario;

    public ContaPoupanca(int numero, int agencia, string titular, float saldo, int dia, int mes) 
        : base(numero, agencia, 2, titular, saldo){

        DiaAniversario = dia;
        MesAniversario = mes;
    }
     
    //Getters
    public int getDiaAniversario(){

        return DiaAniversario;
    }

    public int getMesAniversario(){

        return MesAniversario;
    }

    public string getAniversario(){

        return $"{DiaAniversario:D2}/{MesAniversario:D2}";
    }

    //set
    public void setAniversario(int diaAniversario, int mesAniversario){

        DiaAniversario = diaAniversario;
        MesAniversario = mesAniversario;
    }

    public void visualizar(){

        Console.WriteLine("____________________________________________");
        Console.WriteLine("Dados da Conta Poupança");
        Console.WriteLine("____________________________________________");
        Console.WriteLine($"Número da Conta: {getNumero()}");
        Console.WriteLine($"Agência: {getAgencia()}");
        Console.WriteLine($"Tipo da Conta: {getTipo()}");
        Console.WriteLine($"Titular: {getTitular()}");
        Console.WriteLine($"Saldo: R$ {getSaldo():F2}");
        Console.WriteLine($"Aniversario: {DiaAniversario:D2}/{MesAniversario:D2}");
        Console.WriteLine("____________________________________________");
    }
}