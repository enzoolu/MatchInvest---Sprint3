using System;
using System.IO;
using MatchInvest.Data;
using MatchInvest.Models;
using MatchInvest.Services;
using System.Collections.Generic;
using System.Linq;

namespace MatchInvest
{
    class Program
    {
        static void Main(string[] args)
        {
            var investorRepo = new InvestorRepository();
            var assessorRepo = new AssessorRepository();

            Console.WriteLine("Bem-vindo ao MatchInvest - Gerenciamento de Dados!");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nSelecione uma op��o:");
                Console.WriteLine("\n-- Menu Investidor --");
                Console.WriteLine("1. Cadastrar novo investidor");
                Console.WriteLine("2. Listar todos os investidores");
                Console.WriteLine("3. Buscar investidor por ID");
                Console.WriteLine("4. Atualizar investidor");
                Console.WriteLine("5. Excluir investidor");

                Console.WriteLine("\n-- Menu Assessor --");
                Console.WriteLine("6. Cadastrar novo assessor");
                Console.WriteLine("7. Listar todos os assessores");
                Console.WriteLine("8. Buscar assessor por ID");
                Console.WriteLine("9. Atualizar assessor");
                Console.WriteLine("10. Excluir assessor");

                Console.WriteLine("\n-- Outras Funcionalidades --");
                Console.WriteLine("11. Gerenciar ativos (JSON)");
                Console.WriteLine("12. Sugerir Assessor para Investidor");
                Console.WriteLine("0. Sair");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        CadastrarInvestidor(investorRepo);
                        break;
                    case "2":
                        Console.Clear();
                        ListarTodosInvestidores(investorRepo);
                        break;
                    case "3":
                        Console.Clear();
                        BuscarInvestidor(investorRepo);
                        break;
                    case "4":
                        Console.Clear();
                        AtualizarInvestidor(investorRepo);
                        break;
                    case "5":
                        Console.Clear();
                        ExcluirInvestidor(investorRepo);
                        break;
                    case "6":
                        Console.Clear();
                        CadastrarAssessor(assessorRepo);
                        break;
                    case "7":
                        Console.Clear();
                        ListarTodosAssessores(assessorRepo);
                        break;
                    case "8":
                        Console.Clear();
                        BuscarAssessor(assessorRepo);
                        break;
                    case "9":
                        Console.Clear();
                        AtualizarAssessor(assessorRepo);
                        break;
                    case "10":
                        Console.Clear();
                        ExcluirAssessor(assessorRepo);
                        break;
                    case "11":
                        Console.Clear();
                        GerenciarAtivos();
                        break;
                    case "12":
                        Console.Clear();
                        SugerirAssessor(investorRepo, assessorRepo);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Op��o inv�lida. Tente novamente.");
                        Console.WriteLine("Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nOpera��o conclu�da. Pressione qualquer tecla para voltar ao menu principal...");
                    Console.ReadKey();
                }
            }
        }

        static void CadastrarInvestidor(InvestorRepository repo)
        {
            Console.WriteLine("\n--- Cadastro de Investidor ---");
            Console.Write("Nome: ");
            var nome = Console.ReadLine();
            Console.Write("Capital Dispon�vel: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal capital))
            {
                Console.WriteLine("Capital inv�lido.");
                return;
            }
            Console.Write("Apetite de Risco (Ex: Baixo, Medio, Alto): ");
            var risco = Console.ReadLine();
            Console.Write("Objetivos: ");
            var objetivos = Console.ReadLine();
            Console.Write("Prefer�ncias: ");
            var preferencias = Console.ReadLine();

            var novoInvestidor = new Investor
            {
                Nome = nome,
                CapitalDisponivel = capital,
                ApetiteRisco = risco,
                Objetivos = objetivos,
                Preferencias = preferencias
            };

            repo.AddInvestor(novoInvestidor);
            var investorId = repo.GetLastInsertedId();
            Console.WriteLine($"\nInvestidor cadastrado com sucesso! ID: {investorId}");
        }

        static void ListarTodosInvestidores(InvestorRepository repo)
        {
            Console.WriteLine("\n--- Lista de Todos os Investidores ---");
            var investidores = repo.GetAllInvestors();
            if (investidores.Any())
            {
                foreach (var inv in investidores)
                {
                    Console.WriteLine($"ID: {inv.Id}, Nome: {inv.Nome}, Risco: {inv.ApetiteRisco}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum investidor cadastrado.");
            }
        }

        static void BuscarInvestidor(InvestorRepository repo)
        {
            Console.WriteLine("\n--- Buscar Investidor ---");
            Console.Write("ID do investidor: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var investidor = repo.GetInvestorById(id);
                if (investidor != null)
                {
                    Console.WriteLine("\nDados do Investidor:");
                    Console.WriteLine($"ID: {investidor.Id}");
                    Console.WriteLine($"Nome: {investidor.Nome}");
                    Console.WriteLine($"Capital: {investidor.CapitalDisponivel}");
                    Console.WriteLine($"Risco: {investidor.ApetiteRisco}");
                    Console.WriteLine($"Objetivos: {investidor.Objetivos}");
                    Console.WriteLine($"Prefer�ncias: {investidor.Preferencias}");
                }
                else
                {
                    Console.WriteLine("Investidor n�o encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inv�lido.");
            }
        }

        static void AtualizarInvestidor(InvestorRepository repo)
        {
            Console.WriteLine("\n--- Atualizar Investidor ---");
            Console.Write("ID do investidor para atualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var investidor = repo.GetInvestorById(id);
                if (investidor != null)
                {
                    Console.WriteLine("Deixe em branco para manter o valor atual.");
                    Console.Write($"Novo Nome ({investidor.Nome}): ");
                    var novoNome = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(novoNome)) investidor.Nome = novoNome;

                    Console.Write($"Novo Capital Dispon�vel ({investidor.CapitalDisponivel}): ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal novoCapital)) investidor.CapitalDisponivel = novoCapital;

                    Console.Write($"Novo Apetite de Risco ({investidor.ApetiteRisco}): ");
                    var novoRisco = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(novoRisco)) investidor.ApetiteRisco = novoRisco;

                    Console.Write($"Novos Objetivos ({investidor.Objetivos}): ");
                    var novosObjetivos = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(novosObjetivos)) investidor.Objetivos = novosObjetivos;

                    Console.Write($"Novas Prefer�ncias ({investidor.Preferencias}): ");
                    var novasPreferencias = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(novasPreferencias)) investidor.Preferencias = novasPreferencias;

                    repo.UpdateInvestor(investidor);
                    Console.WriteLine("Investidor atualizado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Investidor n�o encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inv�lido.");
            }
        }

        static void ExcluirInvestidor(InvestorRepository repo)
        {
            Console.WriteLine("\n--- Excluir Investidor ---");
            Console.Write("ID do investidor para excluir: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                repo.DeleteInvestor(id);
                Console.WriteLine("Investidor exclu�do com sucesso!");
            }
            else
            {
                Console.WriteLine("ID inv�lido.");
            }
        }

        static void CadastrarAssessor(AssessorRepository repo)
        {
            Console.WriteLine("\n--- Cadastro de Assessor ---");
            Console.Write("Nome: ");
            var nome = Console.ReadLine();
            Console.Write("Certifica��es: ");
            var certificacoes = Console.ReadLine();
            Console.Write("Especializa��o (Ex: Renda Fixa, Renda Vari�vel, Criptomoedas): ");
            var especializacao = Console.ReadLine();
            Console.Write("Hist�rico de Atua��o: ");
            var atuacao = Console.ReadLine();

            var novoAssessor = new Assessor
            {
                Nome = nome,
                Certificacoes = certificacoes,
                Especializacao = especializacao.ToLower(),
                Atuacao = atuacao
            };

            repo.AddAssessor(novoAssessor);
            var assessorId = repo.GetLastInsertedId();
            Console.WriteLine($"\nAssessor cadastrado com sucesso! ID: {assessorId}");
        }

        static void ListarTodosAssessores(AssessorRepository repo)
        {
            Console.WriteLine("\n--- Lista de Todos os Assessores ---");
            var assessores = repo.GetAllAssessors();
            if (assessores.Any())
            {
                foreach (var assessor in assessores)
                {
                    Console.WriteLine($"ID: {assessor.Id}, Nome: {assessor.Nome}, Especializa��o: {assessor.Especializacao}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum assessor cadastrado.");
            }
        }

        static void BuscarAssessor(AssessorRepository repo)
        {
            Console.WriteLine("\n--- Buscar Assessor ---");
            Console.Write("ID do assessor: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var assessor = repo.GetAssessorById(id);
                if (assessor != null)
                {
                    Console.WriteLine("\nDados do Assessor:");
                    Console.WriteLine($"ID: {assessor.Id}");
                    Console.WriteLine($"Nome: {assessor.Nome}");
                    Console.WriteLine($"Certifica��es: {assessor.Certificacoes}");
                    Console.WriteLine($"Especializa��o: {assessor.Especializacao}");
                    Console.WriteLine($"Hist�rico de Atua��o: {assessor.Atuacao}");
                }
                else
                {
                    Console.WriteLine("Assessor n�o encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inv�lido.");
            }
        }

        static void AtualizarAssessor(AssessorRepository repo)
        {
            Console.WriteLine("\n--- Atualizar Assessor ---");
            Console.Write("ID do assessor para atualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var assessor = repo.GetAssessorById(id);
                if (assessor != null)
                {
                    Console.WriteLine("Deixe em branco para manter o valor atual.");
                    Console.Write($"Novo Nome ({assessor.Nome}): ");
                    var novoNome = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(novoNome)) assessor.Nome = novoNome;

                    Console.Write($"Novas Certifica��es ({assessor.Certificacoes}): ");
                    var novasCertificacoes = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(novasCertificacoes)) assessor.Certificacoes = novasCertificacoes;

                    Console.Write($"Nova Especializa��o ({assessor.Especializacao}): ");
                    var novaEspecializacao = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(novaEspecializacao)) assessor.Especializacao = novaEspecializacao;

                    Console.Write($"Novo Hist�rico de Atua��o ({assessor.Atuacao}): ");
                    var novaAtuacao = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(novaAtuacao)) assessor.Atuacao = novaAtuacao;

                    repo.UpdateAssessor(assessor);
                    Console.WriteLine("Assessor atualizado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Assessor n�o encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID inv�lido.");
            }
        }

        static void ExcluirAssessor(AssessorRepository repo)
        {
            Console.WriteLine("\n--- Excluir Assessor ---");
            Console.Write("ID do assessor para excluir: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                repo.DeleteAssessor(id);
                Console.WriteLine("Assessor exclu�do com sucesso!");
            }
            else
            {
                Console.WriteLine("ID inv�lido.");
            }
        }

        static void GerenciarAtivos()
        {
            Console.Clear();
            Console.WriteLine("\n--- Gerenciamento de Ativos ---");
            Console.WriteLine("1. Adicionar novo ativo");
            Console.WriteLine("2. Listar todos os ativos");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    AdicionarAtivo();
                    break;
                case "2":
                    Console.Clear();
                    ListarAtivos();
                    break;
                default:
                    Console.WriteLine("Op��o inv�lida.");
                    break;
            }
        }

        static void AdicionarAtivo()
        {
            Console.WriteLine("\n--- Adicionar Ativo ---");
            Console.Write("Nome do Ativo: ");
            var nome = Console.ReadLine();
            Console.Write("Classe (Ex: Renda Fixa, Renda Vari�vel): ");
            var classe = Console.ReadLine();
            Console.Write("Descri��o: ");
            var descricao = Console.ReadLine();

            var novoAtivo = new Ativo
            {
                Nome = nome,
                Classe = classe,
                Descricao = descricao
            };

            var filePath = "ativos.json";
            var ativos = FileHandler.ReadJsonFile<Ativo>(filePath);
            ativos.Add(novoAtivo);
            FileHandler.WriteJsonFile(filePath, ativos);

            Console.WriteLine("Ativo adicionado com sucesso e salvo no arquivo JSON!");
        }

        static void ListarAtivos()
        {
            Console.WriteLine("\n--- Lista de Ativos do Arquivo JSON ---");
            var filePath = "ativos.json";
            var ativos = FileHandler.ReadJsonFile<Ativo>(filePath);

            if (ativos.Any())
            {
                foreach (var ativo in ativos)
                {
                    Console.WriteLine($"- Nome: {ativo.Nome}, Classe: {ativo.Classe}, Descri��o: {ativo.Descricao}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum ativo encontrado no arquivo JSON.");
            }
        }

        static void SugerirAssessor(InvestorRepository investorRepo, AssessorRepository assessorRepo)
        {
            Console.WriteLine("\n--- Sugest�o de Assessores ---");
            Console.Write("ID do investidor: ");
            if (int.TryParse(Console.ReadLine(), out int investorId))
            {
                var investidor = investorRepo.GetInvestorById(investorId);
                if (investidor == null)
                {
                    Console.WriteLine("Investidor n�o encontrado.");
                    return;
                }

                var especializacaoRecomendada = string.Empty;
                switch (investidor.ApetiteRisco.ToLower())
                {
                    case "baixo":
                        especializacaoRecomendada = "renda fixa";
                        break;
                    case "medio":
                        especializacaoRecomendada = "renda vari�vel";
                        break;
                    case "alto":
                        especializacaoRecomendada = "criptomoedas";
                        break;
                    default:
                        Console.WriteLine("Perfil de risco n�o reconhecido.");
                        return;
                }

                var assessoresRecomendados = assessorRepo.GetAssessoresByEspecializacao(especializacaoRecomendada);

                if (assessoresRecomendados.Any())
                {
                    Console.WriteLine($"\nAssessores recomendados para seu perfil de risco ({investidor.ApetiteRisco}):");
                    foreach (var assessor in assessoresRecomendados)
                    {
                        Console.WriteLine($"- {assessor.Nome} (Especializa��o: {assessor.Especializacao})");
                    }
                }
                else
                {
                    Console.WriteLine($"Nenhum assessor com especializa��o em '{especializacaoRecomendada}' foi encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID de investidor inv�lido.");
            }
        }
    }
}