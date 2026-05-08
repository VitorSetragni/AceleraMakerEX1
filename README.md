# AceleraMakerEX1

## Conta Bancária

Projeto desenvolvido em C# com .NET para simular um sistema simples de gerenciamento de contas bancárias pelo terminal.

O sistema permite cadastrar, listar, buscar, atualizar e excluir contas, além de realizar operações como depósito, saque, transferência e consulta de saldo.

## Funcionalidades

- Criar conta corrente
- Criar conta poupança
- Listar todas as contas
- Procurar conta pelo número
- Depositar valor em uma conta
- Sacar valor de uma conta
- Transferir valor entre contas
- Atualizar dados da conta
- Deletar uma conta
- Consultar saldo
- Salvar os dados localmente em arquivo JSON

## Tipos de conta

### Conta Corrente

A conta corrente possui:

- Número da conta
- Agência
- Tipo da conta
- Titular
- Saldo
- Limite

Na conta corrente, o saque e a transferência consideram o saldo disponível somado ao limite da conta.

Exemplo:

```text
Saldo: R$ 100,00
Limite: R$ 200,00
Saldo disponível: R$ 300,00
```

### Conta Poupança

A conta poupança possui:

- Número da conta
- Agência
- Tipo da conta
- Titular
- Saldo
- Dia de aniversário
- Mês de aniversário

## Armazenamento dos dados

Os dados são salvos localmente em um arquivo chamado:

```text
contas.json
```

Esse arquivo é criado automaticamente quando uma conta é cadastrada.

Como o arquivo `contas.json` armazena dados locais de teste, ele não precisa ser enviado para o GitHub.

## Estrutura do projeto

```text
contaBancaria/
├── controller/
│   └── ContaController.cs
├── models/
│   ├── Conta.cs
│   ├── ContaCorrente.cs
│   └── ContaPoupanca.cs
├── repository/
│   └── IContaRepository.cs
├── Menu.cs
└── contaBancaria.csproj
```

## Como executar o projeto

Para executar o projeto, é necessário ter o .NET instalado na máquina.

Verifique se o .NET está instalado:

```bash
dotnet --version
```
Você pode baixar o .NET pelo site oficial da Microsoft:

(https://dotnet.microsoft.com/en-us/download)

Depois, entre na pasta do projeto:

```bash
cd contaBancaria
```

Restaure as dependências do projeto:

```bash
dotnet restore
```

Execute o projeto:

```bash
dotnet run
```

## Menu do sistema

Ao executar o projeto, será exibido um menu com as opções:

```text
1 - Criar conta corrente
2 - Criar conta poupança
3 - Listar todas as contas
4 - Procurar conta por número
5 - Depositar
6 - Sacar
7 - Transferir
8 - Atualizar dados da conta
9 - Deletar conta
10 - Consultar saldo
0 - Sair
```

## Exemplo de uso

O usuário pode criar uma conta corrente informando:

```text
Agência: 123
Titular: Vitor
Saldo inicial: R$ 100
Limite: R$ 200
```

Depois do cadastro, o sistema informa o número da conta criada.

Exemplo:

```text
Conta cadastrada com sucesso.
Número da conta criada: 1
```

## Tecnologias utilizadas

- C#
- .NET
- JSON para armazenamento local

## Arquivos ignorados pelo Git

O projeto utiliza um arquivo `.gitignore` para evitar o envio de arquivos desnecessários para o GitHub.

Exemplo:

```gitignore
bin/
obj/
.DS_Store
contas.json
.vscode/
*.tmp
*.log
*.user
*.suo
*.rsuser
```

## Autor

Desenvolvido por Vitor Leite Setragni.
