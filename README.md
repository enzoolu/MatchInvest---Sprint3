# MatchInvest: Conectando Investidores a Especialistas

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![SQLite](https://img.shields.io/badge/Database-SQLite-lightgrey)
![License](https://img.shields.io/badge/License-MIT-green)
![Status](https://img.shields.io/badge/Status-Em%20Desenvolvimento-yellow)

Projeto desenvolvido para o desafio **FIAP / XP Inc.** na disciplina de Desenvolvimento de Software C# (Sprint 3).  
Este sistema de console conecta investidores a assessores financeiros, demonstrando conceitos essenciais como persistência de dados, manipulação de arquivos e aplicação de lógica de negócio.

---

## 🚀 Funcionalidades do Projeto

- **Estrutura de Código Limpo:** Arquitetura em camadas (**Models, Data, Services**) para melhor organização e manutenção.
- **Manipulação de Arquivos:** Leitura e escrita em arquivos `.json` para gerenciar ativos de investimento.
- **CRUD Completo:** Operações de Criar, Ler, Atualizar e Deletar para Investidores e Assessores usando banco de dados SQLite.
- **Interface de Console:** Menu interativo no terminal para fácil navegação.
- **Lógica de Negócio:** Sugestão automática de assessores de acordo com o perfil de risco do investidor.

---

## 🏗 Arquitetura da Solução

O projeto segue uma arquitetura em camadas para separar responsabilidades e facilitar futuras manutenções.

### Diagrama de Arquitetura


graph TD
    A[Interface de Console] --> B(Camada de Repositório)
    B --> C(Banco de Dados SQLite)
    B --> D(Camada de Serviços)
    A --> D
    D --> E(Arquivo JSON)

### Visão Geral das Camadas

- **Program.cs (Interface):** Responsável pela interação com o usuário e exibição do menu.
- **Models:** Classes que representam as entidades de negócio (`Investor.cs`, `Assessor.cs`).
- **Data:** Persistência de dados.
  - `DatabaseContext.cs` gerencia a conexão com o banco SQLite.
  - Repositórios (`InvestorRepository.cs`, `AssessorRepository.cs`) implementam as operações CRUD.
- **Services:** Contém a lógica de manipulação de arquivos.
  - `FileHandler.cs` faz a leitura/escrita de dados no `ativos.json`.

---

## 🛠 Tecnologias Utilizadas

- **Linguagem:** C#
- **Plataforma:** .NET 8.0 (Console Application)
- **Banco de Dados:** SQLite (via pacote NuGet `Microsoft.Data.Sqlite`)
- **Manipulação de Dados:** `System.Text.Json`

---

## ▶️ Como Rodar a Aplicação

1. **Pré-requisitos:**  
   - Ter o **.NET SDK** instalado em sua máquina.  
   - Ter o SQLite disponível (integrado via NuGet, sem necessidade de instalação separada).

2. **Clonar o Repositório:**  
   Abra o terminal e execute: git clone https://github.com/enzoolu/MatchInvest---Sprint3

3. **Navegar até a Pasta:**
   cd MatchInvest - Sprint3

4. **Executar a Aplicação:**
    dotnet run

Após a execução, o menu interativo será exibido no console.

---

## 🖥 Exemplo de Uso

- **Cadastrar Investidor:**  
Escolha a opção `1`, informe os dados solicitados. O sistema exibirá o ID gerado.

- **Sugerir Assessor:**  
Escolha a opção `12`. Se houver investidores e assessores cadastrados com perfis compatíveis, a sugestão será exibida.

- **Gerenciar Ativos:**  
Escolha a opção `11` para adicionar ou listar ativos no arquivo `ativos.json`.

---

##👨‍💻 Integrantes
-Enzo Luiz Goulart - RM99666
-Gustavo Henrique Santos Bonfim - RM98864
-Kayky Paschoal Ribeiro - RM99929
-Lucas Yuji Farias Umada - RM99757
-Natan Eguchi dos Santos - RM98720


## 📄 Licença

Este projeto está sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.
