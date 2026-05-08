namespace ContaBancaria.Models;

public class Conta
{
    private int Numero; 
    private int Agencia;
    private int Tipo; 
    private string Titular; 
    private float Saldo; 
   
   public Conta(int numero, int agencia, int tipo, string titular, float saldo){

        Numero = numero;
        Agencia = agencia;
        Tipo = tipo;
        Titular = titular;
        Saldo = saldo;
   }
    //Getters
   public int getNumero(){

        return Numero;
   }

    public int getAgencia()
    {
        return Agencia;
    }

    public int getTipo()
    {
        return Tipo;
    }

    public string getTitular()
    {
        return Titular;
    }

    public float getSaldo()
    {
        return Saldo;
    }

    //Setters
   public void setNumero(int numero){

        Numero = numero;
   }

    public void setAgencia(int agencia)
    {
        Agencia = agencia;
    }

    public void setTipo(int tipo)
    {
        Tipo = tipo;
    }

    public void setTitular(string titular)
    {
        Titular = titular;
    }

    public void setSaldo(float saldo)
    {
        Saldo = saldo;
    }

    public virtual bool sacar(float valor){

        if(Saldo < valor){
            return false;
        }

        Saldo -= valor;
        return true;

    }

    public void depositar(float valor){

        Saldo += valor;
    }
}