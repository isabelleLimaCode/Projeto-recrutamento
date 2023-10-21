using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colorful;
using Console = Colorful.Console;
using System.Threading;
using JobOffer.Domain.Models;
using JobOffer.infrastructure;
using JobOffer.infrastructure.Repositories;
using JobOffer.Domain.SeekWork;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Net.WebSockets;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.Diagnostics;

namespace JobOfferConsola
{
    public  class menu
    {
        public static void menuprincipal()
        {
            for (; ; )
            {

                // menu principal
                int option;
                Console.Clear();
                writelogo();
                Console.Write("\n\n");
                Cal("1", "Criar Conta");
                Cal("2", "Login");
                Cal("3", "Area Administrador");
                Cal("0", "Sair");
                Console.WriteLine("introdiza uma opção:");
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1://criar conta recrutador ou candidato
                        addcandidatourecrutador();
                        break;

                    case 2://menu recrutador
                        menurecrutadoroucanidato(option);
                        break;

                    case 3://menu administrador
                        administrador(option);
                        break;

                    case 0://sair
                        Console.WriteLine("Fechando a aplicação....");
                        Thread.Sleep(1400);
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Erro: Porfavor escolha outra opção");
                        Thread.Sleep(1500);
                        break;


                }
            }
        }
         



        public static void Cal(string prefix, string messagem)// escreve no ecra a estrutura do Menu
                {
                    Console.Write("[");
                    Console.Write(prefix, Color.White);
                    Console.WriteLine("]"+ messagem);
                }

        public static void writelogo() // escreve no ecra
        {
            string logo = @"
                 ██╗ ██████╗ ██████╗      ██████╗ ███████╗███████╗███████╗██████╗ 
                 ██║██╔═══██╗██╔══██╗    ██╔═══██╗██╔════╝██╔════╝██╔════╝██╔══██╗
                 ██║██║   ██║██████╔╝    ██║   ██║█████╗  █████╗  █████╗  ██████╔╝
            ██   ██║██║   ██║██╔══██╗    ██║   ██║██╔══╝  ██╔══╝  ██╔══╝  ██╔══██╗
            ╚█████╔╝╚██████╔╝██████╔╝    ╚██████╔╝██║     ██║     ███████╗██║  ██║";

            Console.WriteLine(logo,Color.Azure);
        }

        public static void addcandidatourecrutador() // adiciona candidado ou recrutador a base de dados 
        {
            Console.Clear();
            menu.writelogo();
            Console.Title = "Cadastro Recrutador ou Candidato";
            string nome, pass;
            int escolha = 0;
            Console.WriteLine("\n\n");
            Console.WriteLine("Desejas Cadastrar Candidato:1 ou Recrutador:2");
            escolha = int.Parse(Console.ReadLine());

            using (var n = new JobOfferDBcontext())
            {

                if (escolha == 2)
                {
                    Console.WriteLine("\nNovo Candidato:");
                    Console.WriteLine("Nome:");
                    nome = Console.ReadLine();
                    Console.WriteLine("Password:");
                    pass = Console.ReadLine();
                    var recrutador1 = new Recrutador
                    {
                        Nome = nome,
                        Password = pass

                    };

                    n.Add(recrutador1);
                    n.SaveChanges();
                    Console.WriteLine("Recrutador Adicionado com Sucesso, prencione Enter para Continuar");
                    Console.ReadKey();
                }
                else if (escolha == 1) // Adcionar Candidato
                {
                    string experiencia, nivelcompetencias, idomas, passwordc;
                    int nivelescolaridade, niveis;
                    Console.WriteLine("Nome:");
                    nome = Console.ReadLine();
                    Console.WriteLine("Password:");
                    passwordc = Console.ReadLine();
                    Console.WriteLine("Experiencia:");
                    experiencia = Console.ReadLine();
                    Console.WriteLine("Nivel de escolaridade:");
                    nivelescolaridade = int.Parse(Console.ReadLine());
                    listaNivel();
                    Console.WriteLine("escolha um nivel:");
                    niveis = int.Parse(Console.ReadLine());
                    Console.Write("idiomas:");
                    idomas = Console.ReadLine();
                    Console.Write("Nivel de Competecias:");
                    nivelcompetencias = Console.ReadLine();

                    var candidato = new Candidato
                    {
                        Nome = nome,
                        Password = passwordc,
                        Experiencia = experiencia,
                        NivelEscolaridade = nivelescolaridade,
                        NivelCompeteciaDigitais = nivelcompetencias,
                        NivelId = niveis,// resolver o problema que não guarda o nivel na base de dados - tentei fazer uma validação mas da erros
                        Idiomas = idomas,
                        
                    };

                    n.Add(candidato);
                    n.SaveChanges();
                    Console.WriteLine("Candidato Adicionado com Sucesso, prencione Enter para Continuar");
                    Console.ReadKey();
                }


            }
         
        }
       
 
        public static int menurecrutadoroucanidato(int option)// menu recrutador ou candadito dependendo da senha que introduzir
        {
            Console.Clear();
            menu.writelogo();
            Console.Title = "Login";
            option = 1;
            using (var uow = new UnitOfWork())
            {
                string n;
                string passw;
                Console.WriteLine("\nLogin:");
                Console.WriteLine("Nome:");
                n = Console.ReadLine();
                Console.WriteLine("Password:");
                passw = Console.ReadLine();
                //verifica se é o recrutador ou candidato e valida a conta para entrar no menu
                var recutadornome = uow.RecrutadorRepository.FindbyName(n);
                var candidatonome = uow.CandidatoRepository.FindbyName(n);

                    if (recutadornome != null) // por se null n pode fazer referencia ao objeto então
                    {
                        if (recutadornome.Password == passw && recutadornome.Nome == n )
                        {
                            Console.WriteLine("Migrando para o Menu Recrutador....");
                            Thread.Sleep(1400);


                            while (option != 0)
                            {

                                
                                  

                                Console.Clear();
                                menu.writelogo();
                                Console.Title = "Login";
                                Console.Write("\n\n");
                                Console.WriteLine($"Recrutador:{recutadornome.Nome}\n");
                                Cal("1", "Minha Conta ");
                                Cal("2", "Adicionar Vaga");
                                Cal("3", "Visualizar Candidaturas as Vagas");
                                Cal("4", "Visualizar Cargos");
                                Cal("5", "Visualizar Minhas Vagas");
                                Cal("6", "Analisar Vagas");
                                Cal("0", "Sair");
                                Console.WriteLine("introduza uma opção:");
                                option = int.Parse(Console.ReadLine());
                                switch (option)
                                {
                                    case 1:
                                        listarMinhaconta(recutadornome.Id);
                                        Console.ReadKey();
                                        break;

                                    case 2:
                                        string Tipo, Regime, Horario, Tipo_contrato;
                                        int N_vaga, cargo, empresa;
                                        Console.WriteLine("Tipo:");
                                        Tipo = Console.ReadLine();
                                        Console.WriteLine("Regime:");
                                        Regime = Console.ReadLine();
                                        Console.WriteLine("Horario:");
                                        Horario = Console.ReadLine();
                                        Console.WriteLine("Tipo de Contrato:");
                                        Tipo_contrato = Console.ReadLine();
                                        Console.WriteLine("Numero de vagas:");
                                        N_vaga = int.Parse(Console.ReadLine());
                                        listarCargo();
                                        Console.WriteLine("Cargo");
                                        cargo = int.Parse(Console.ReadLine());
                                        listarEmpresa();
                                        Console.WriteLine("Empresa");
                                        empresa = int.Parse(Console.ReadLine());
                                        using (var registarovaga = new JobOfferDBcontext())
                                        {
                                            var vaga = new Vaga
                                            {
                                                Tipo = Tipo,
                                                Regime = Regime,
                                                Horario = Horario,
                                                Tipo_contrato = Tipo_contrato,
                                                N_vaga = N_vaga,
                                                RecrutadorId = recutadornome.Id,
                                                EstadoId = 1,
                                                CargoId = cargo,
                                                EmpresaId = empresa,

                                                //prencher
                                            };
                                            registarovaga.Add(vaga);
                                            registarovaga.SaveChanges();
                                        }

                                        Console.WriteLine("Vaga Adicionada com Sucesso, prencione Enter para Continuar");
                                        Console.ReadKey();
                                        break;

                                    case 3:
                                        listarCandidaturas(); // listar Candidaturas as Vagas
                                    Console.ReadKey();
                                        break;

                                    case 4:
                                        listarCargo(); // lsitar cargo
                                        Console.ReadKey();
                                        break;

                                    case 5:

                                        listarVagasRecrutador(recutadornome.Id);//listar cargo do recrutador
                                        Console.ReadKey();
                                        break;

                                    case 6:
                                        listarCandidaturas();// analisar e passa para admitido a candidatura
                                        Console.WriteLine("Escolha a Vaga que deseja Mudar estado:");
                                        int escolhaVaga = int.Parse(Console.ReadLine());
                                        Console.WriteLine("O Candidato foi Admitido -> 1 Não Admitido -> 2");
                                        int admitidoouNao = int.Parse(Console.ReadLine());
                                        analisarVaga(escolhaVaga, admitidoouNao);
                                        Console.WriteLine("Vaga Analisada com Sucesso!");
                                        Console.ReadKey();
                                        break;

                                    case 0://sai da aplicação
                                        Console.WriteLine("Fechando a aplicação....");
                                        Thread.Sleep(1400);
                                        Environment.Exit(0);
                                        break;

                                    default:

                                        Console.WriteLine("Erro: Opção Invalida:\n precione enter para continuar.");
                                        Console.ReadKey();

                                        break;

                                }
                            }
                       
                        }
                    
                    }else if ( candidatonome != null && candidatonome.Nome == n)
                    {
                        
                        if(candidatonome.Password == passw  ) // verifica password candidado se é valida
                        {

                            Console.WriteLine("Migrando para o Menu Candidato....");
                            Thread.Sleep(1400);
                            while (option != 0)
                            {

                                //menu do candidato
                                Console.Clear();
                                menu.writelogo();
                                Console.Title = "Login";
                                Console.Write("\n\n");
                                Console.WriteLine($"Candidato:{candidatonome.Nome}\n");
                                Cal("1", "Minha Conta ");
                                Cal("2", "Estado das Minhas Vagas");
                                Cal("3", "Candidatar a Vaga");
                                Cal("0", "Sair");
                                Console.WriteLine("introdiza uma opção:");
                                option = int.Parse(Console.ReadLine());
                                switch (option)
                                {
                                    case 1:
                                        int op;
                                        Console.Clear();
                                        Console.Title = "Sub Menu Conta Candidato";
                                        Console.WriteLine("-------------Sub Menu Conta Candidato----------");
                                        Cal("1", "Editar Conta");
                                        Cal("2", "Eliminar Conta");
                                        Cal("3", "Visualizar Conta");
                                        Cal("0", "Sair");
                                        op = int.Parse(Console.ReadLine());
                                        submenuContaCand(op, candidatonome.Id);//sub menu diferente a estrutura recrutador pq
                                        //tem mais dados a ser alterado.
                                        break;

                                    case 2:
                                        listarVgagasCandidatadas(candidatonome.Id);//lista as candidaturas deste candidato
                                        Console.ReadKey();
                                        break;

                                    case 3:
                                        CandidataraVaga(candidatonome.Id);// Candidatar a uma vaga
                                        break;

                                    case 0:
                                        Console.WriteLine("Fechando a aplicação....");
                                        Thread.Sleep(1400);
                                        Environment.Exit(0);
                                        break;

                                    default:

                                        Console.WriteLine("Erro: Opção Invalida:\n precione enter para continuar.");
                                        Console.ReadKey();

                                        break;

                            }
                            }
                        }
                    }
                else if(candidatonome == null || recutadornome == null)
                {
                    Console.WriteLine("Erro: Password ou username Incorretos:\n precione enter para continuar.");
                    Console.ReadKey();
                }
                return option;
            }
        }
        public static void analisarVaga(int id,int resposta) //dependo da resposta o estado da candidatura muda
        {
            int buscarid;
            using (var find = new UnitOfWork())
            {
                var buscardeidcandidatura = find.Candidato_has_vagaRepository.FindbyCandidato_has_vagaid(id);
                buscarid = buscardeidcandidatura.Id;
            }

            if(resposta == 2)
            {
                using (var alterarNVagas = new JobOfferDBcontext())
                {
                    var numevaga = alterarNVagas.Candidato_has_vagas.First(c => c.Id == buscarid);
                    numevaga.Estado_da_Candidatura = "Não Admitido";
                    alterarNVagas.SaveChanges();
                    
                };
            }else if (resposta == 1) {
                using (var alterarNVagas = new JobOfferDBcontext())
                {
                    var numevaga = alterarNVagas.Candidato_has_vagas.First(c => c.Id == buscarid);
                    numevaga.Estado_da_Candidatura = "Admitido";
                    alterarNVagas.SaveChanges();
                };
            }

        }
        public static void CandidataraVaga(int idcandidato) // candidatar a uma vaga . obs: so mostra vagas que ainda não foi 
            //prenchida ex: N_vaga =0 já não ira mostar
        {

            Console.Title = "Candidatar a uma Vaga";
            string buscanome, nomeestado;
            int buscarestado, idvaga, alteraln_vaga, buscarnvagas,buscarid;
            listarVagasDisponiveis();
            Console.WriteLine("Escolha uma vaga:");
            idvaga = int.Parse(Console.ReadLine());
            using (var find = new UnitOfWork())
            {
                var buscardescricao = find.VagaRepository.FindByVagaId(idvaga);
                buscarid = buscardescricao.Id;
                buscanome = buscardescricao.Tipo;
                buscarnvagas = buscardescricao.N_vaga;
                buscarestado = buscardescricao.EstadoId;
                var buscarnomeestado = find.EstadoRepository.FindbyId(buscarestado);
                nomeestado = buscarnomeestado.Descricao;
            }

            using (var criarcargo = new JobOfferDBcontext())
            {

                var vaga = new Candidato_vaga
                {
                    CandidatoId = idcandidato,
                    VagaId = idvaga,
                    Descricao = buscanome,
                    Data = DateTime.Now,
                    Estado_da_Candidatura = nomeestado,
                  

                };
                criarcargo.Add(vaga);
                criarcargo.SaveChanges();
                // retirar a vaga 
                alteraln_vaga = buscarnvagas - 1;
                using (var alterarNVagas = new JobOfferDBcontext())
                {
                    var numevaga = alterarNVagas.Vagas.First(c => c.Id == buscarid);
                    numevaga.N_vaga = alteraln_vaga;
                    alterarNVagas.SaveChanges();
                };
                Console.WriteLine("Candidatura feita com Sucesso, prencione Enter para Continuar");
                Console.ReadKey();
            }
        }
       

        public static void submenuContaCand(int op, int id)// editar/visualizar/remover conta 
        {
            string nome;
            switch (op)
            {
                case 1:
                    Console.Title = "Sub Menu Conta Candidato";
                    Console.Title = "Editar Conta";
                    string experiencia, nivelcompetencias, idomas, passwordc;
                    int nivelescolaridade, niveis, idcanid;
                    Console.WriteLine("Nome:");
                    nome = Console.ReadLine();
                    Console.WriteLine("Password:");
                    passwordc = Console.ReadLine();
                    Console.WriteLine("Experiencia:");
                    experiencia = Console.ReadLine();
                    Console.WriteLine("Nivel de escolaridade:");
                    nivelescolaridade = int.Parse(Console.ReadLine());
                    listaNivel();
                    Console.WriteLine("escolha um nivel:");
                    niveis = int.Parse(Console.ReadLine());
                    Console.Write("idiomas:");
                    idomas = Console.ReadLine();
                    Console.Write("Nivel de Competecias:");
                    nivelcompetencias = Console.ReadLine();
                    using (var find = new UnitOfWork())
                    {
                        var buscarid = find.CandidatoRepository.FindbyId(id);
                        idcanid = buscarid.Id;
                    }
                    using (var cand = new JobOfferDBcontext())
                    {


                        var candidato = new Candidato
                        {
                            Id = idcanid,
                            Nome = nome,
                            Password = passwordc,
                            Experiencia = experiencia,
                            NivelEscolaridade = nivelescolaridade,
                            NivelCompeteciaDigitais = nivelcompetencias,
                            NivelId = niveis,
                            Idiomas = idomas,

                        };
                        cand.Update(candidato);
                        cand.SaveChanges();
                        Console.WriteLine("Alterações feitas com Sucesso, pressione Enter para Continuar");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                        Console.Title = "Sub Menu Conta Candidato";
                        Console.Title = "Deletar Conta";
                       
                        using (var n = new UnitOfWork())
                        {
                            var buscarConta = n.CandidatoRepository.FindbyId(id);
                            n.CandidatoRepository.Delete(buscarConta);
                            n.Save();
                        }
                        Console.WriteLine("Conta Eliminada com Sucesso, prencione Enter para Continuar");
                        Console.ReadKey();
                        Console.WriteLine("Fechando a aplicação....");
                        Thread.Sleep(1400);
                        Environment.Exit(0);
                    break;
                    case 3:
                    using (var uow = new UnitOfWork())
                        {
                            var candidato = uow.CandidatoRepository.FindbyId(id);
                            Console.WriteLine($"-> Nome:{candidato.Nome} \n-> Password:{candidato.Password} \n" +
                            $"-> Experiencia: {candidato.Experiencia} \n-> Nivel Escolaridade: {candidato.NivelEscolaridade}" +
                            $" \n-> Nivel de Competencias Digitais: {candidato.NivelCompeteciaDigitais}" +
                            $"\n-> Idiomas: {candidato.Idiomas}");
                            Console.ReadKey();
                         }
                         break;
                    default:
                    Console.WriteLine("Erro: Opção incorreta:\n precione enter para continuar.");
                    Console.ReadKey();
                    break;



            }
        }
        public static int  administrador(int option) // menu extra para o administrador a opção n é 
            {//visivel no ecra pq somente ele sabe o valor para entrar no menu seria o valor 12345.
                Console.Clear();
                writelogo();
                Console.WriteLine("\n\n");
                Console.WriteLine("Digite a senha:");
                string senha = Console.ReadLine();
                       if(senha != "12345")
                       {
                             Console.WriteLine("Erro: Senha introduzida esta incorreta.. Precione enter para continuar");
                            Console.ReadKey();
                       }
                    if(senha == "12345")
                    {
                        Console.WriteLine("Migrando para o Menu Admistrador....");
                        Thread.Sleep(1400);
                        while (option != 0)
                        {
                            Console.Clear();
                            Console.WriteLine("-------------Menu administrador ----------");
                            Console.Title = "Area Administrador";
                            Console.Write("\n\n");
                            Cal("1", "Sub Menu Cargos ");
                            Cal("2", "Sub Menu Empresas");
                            Cal("0", "Sair");
                            Console.WriteLine("introdiza uma opção:");
                            option = int.Parse(Console.ReadLine());


                            switch (option)
                            {
                                case 1://Visualizar/Editar/Adicionar Cargo
                                    Console.Clear();
                                    Console.Title = "Sub Menu Cargo";
                                    Console.WriteLine("-------------Sub Menu Cargo  ----------");
                                    Cal("1", "Adicionar Cargos ");
                                    Cal("2", "Editar Cargos");
                                    Cal("3", "visualizar Cargos");
                                    Cal("4", "Eliminar Cargo");
                                    option = int.Parse(Console.ReadLine());
                                    submenuCargo(option);
                                    break;

                                case 2://Visualizar/Editar/Adicionar Empresa
                                    Console.Clear();
                                    Console.Title = "Sub Menu Empresa";
                                    Console.WriteLine("-------------Sub Menu Empresa  ----------");
                                    Cal("1", "Adicionar Empresas ");
                                    Cal("2", "Editar Empresa");
                                    Cal("3", "visualizar Empresas");
                                    Cal("4", "Eliminar Empresa");
                                    option = int.Parse(Console.ReadLine());
                                    submenuEmpresa(option);
                                    break;

                                default:
                                    Console.WriteLine("Erro: Opção incorreta:\n precione enter para continuar.");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        
                
                    }
               
                     return option;
           
               
        }
                //visualizar e editar cargos 
                
 
        public static void submenuCargo(int option)
        {
            string descriçao;
            switch (option)
            {
                case 1:
                    Console.Title = "Sub Menu Cargo";
                    Console.WriteLine("Descrição:");
                    descriçao = Console.ReadLine();

                    using (var criarcargo = new JobOfferDBcontext())
                    {
                       
                        var cargo = new Cargo
                        {
                            Descricao = descriçao,
                        };
                        
                        criarcargo.Add(cargo);
                        criarcargo.SaveChanges();
                        Console.WriteLine("Cargo Adicionado com Sucesso, prencione Enter para Continuar");
                        Console.ReadKey();
                    }
                    break;

                case 2:
                    Console.Title = "Sub Menu Cargo";
                    Console.WriteLine("Lista Cargo:");
                    listarCargo();
                    Console.WriteLine("Escolha o Cargo que deseja Alterar id:");
                    int id = int.Parse(Console.ReadLine());
                    int teste;
                    Console.WriteLine("Descrição:");
                    descriçao = Console.ReadLine();

                    using (var buscaid = new UnitOfWork())
                    {
                        var buscarid = buscaid.CargoRepository.FindbyId(id);
                        teste = buscarid.Id;
                    }

                    using (var updatecargo = new JobOfferDBcontext())
                    {

                        var cargo = new Cargo
                        {
                            Id = teste,
                            Descricao = descriçao,
                        };
                        updatecargo.Update(cargo);
                        updatecargo.SaveChanges();
                        Console.WriteLine("Cargo Alterado com Sucesso, prencione Enter para Continuar");
                        Console.ReadKey();
                    }
                    break;

                case 3:
                    Console.Title = "Sub Menu Cargo";
                    listarCargo();
                    Console.ReadKey();
                    break;

                case 4:
                    Console.Title = "Sub Menu Cargo";
                    listarCargo();
                    Console.WriteLine("\nQual o Cargo que deseja apaga:");
                    Console.WriteLine("Id:");
                    int descri = int.Parse(Console.ReadLine());
                    using (var deletecargo = new UnitOfWork())
                    {
                        var buscarcarg = deletecargo.CargoRepository.FindbyId(descri);
                        deletecargo.CargoRepository.Delete(buscarcarg);
                        deletecargo.Save();
                        Console.WriteLine("Cargo Deletado com Sucesso, prencione Enter para Continuar");
                        Console.ReadKey();
                    }

                    break;

                default:
                    Console.WriteLine("Erro: Opção incorreta:\n precione enter para continuar.");
                    Console.ReadKey();
                    break;
            }
        }

        public static void listarMinhaconta(int id)
        {
            using (var uow = new UnitOfWork())
            {
                var MinhacontaList = uow.RecrutadorRepository.FindbyId(id);

                Console.WriteLine("____________________________________________________________________________");
                Console.WriteLine($"|Nome:{MinhacontaList.Nome}\n|Password:{MinhacontaList.Password} \n");
                Console.WriteLine("____________________________________________________________________________");
            }
            Console.WriteLine("1 -> Editar Conta 2\n2 -> Eliminar Conta\n0 -> Sair ");
            int op = int.Parse(Console.ReadLine());
            switch (op)
            {

                case 1:
                    string nome ,password;
                    Console.WriteLine("Nome:");
                    nome = Console.ReadLine();
                    Console.WriteLine("Password:");
                    password = Console.ReadLine();
                    using (var cand = new JobOfferDBcontext())
                    {


                        var recurtador = new Recrutador
                        {
                            Id = id,
                            Nome = nome,
                          
                        };
                        cand.Update(recurtador);
                        cand.SaveChanges();
                        Console.WriteLine("Alterações feitas com Sucesso, pressione Enter para Continuar");
                        Console.ReadKey();
                    }
                    break;

                case 2:
                    using (var n = new UnitOfWork())
                    {
                        var buscarConta = n.RecrutadorRepository.FindbyId(id);
                        n.RecrutadorRepository.Delete(buscarConta);
                        n.Save();
                    }
                    Console.WriteLine("Conta Eliminada com Sucesso, prencione Enter para Continuar");
                    Console.ReadKey();
                    Console.WriteLine("Fechando a aplicação....");
                    Thread.Sleep(1400);
                    Environment.Exit(0);
                    break;
                case 0:
                    Console.Clear();
                    writelogo();
                    Console.WriteLine("\n\n");
                    Console.WriteLine("prencione enter para Mudar o Menu");

                    break;
  
                default:
                    Console.WriteLine("Erro: Opção incorreta:\n precione enter para continuar.");
                    Console.ReadKey();
                    break;



            }
        }


        public static void listarCargo()
        {
            using (var listar = new UnitOfWork())
            {
                var cargoList = listar.CargoRepository.FindAll();
             
                foreach (var cargos in cargoList)
                {
                    Console.WriteLine("____________________________________________________________________________");
                    Console.WriteLine($"|Id:{cargos.Id}\n|Cargo:{cargos.Descricao}");
                    Console.WriteLine("____________________________________________________________________________");

                }
                


            }
        }
        public static void listarEmpresa()
        {
            using (var listar = new UnitOfWork())
            {
                var empresaList = listar.EmpresaRepository.FindAll();


                foreach (var cargos in empresaList)
                {
                    Console.WriteLine("____________________________________________________________________________");
                    Console.WriteLine($"|Id:{cargos.Id}\n|Empresa:{cargos.Nome}");
                    Console.WriteLine("____________________________________________________________________________");
                }



            }
        }
        public static void listarEstado()
        {
            using (var listar = new UnitOfWork())
            {
                var estadoList = listar.EstadoRepository.FindAll();
            
                foreach (var estados in estadoList)
                {
                    Console.WriteLine("____________________________________________________________________________");
                    Console.WriteLine($"|Id{estados.Id}\n|Descrição:{estados.Descricao}");
                    Console.WriteLine("____________________________________________________________________________");

                }



            }
        }
        public static void listarCandidaturas()
        {
            using (var listar = new UnitOfWork())
            {
                var candidaturasList = listar.Candidato_has_vagaRepository.FindAll();
            
                foreach (var cand in candidaturasList)
                {
                    Console.WriteLine("____________________________________________________________________________");
                    Console.WriteLine($"|Id{cand.Id}\n|Id_Candidato:{cand.CandidatoId}\n|Id_vaga:{cand.VagaId} " +
                        $"\n|Descrição:{cand.Descricao}\n|Data:{cand.Data}\n|Estado Candidatura:{cand.Estado_da_Candidatura}");
                    Console.WriteLine("____________________________________________________________________________");
                }



            }
        }
        public static void listaNivel()
        {
            using (var listar = new UnitOfWork())
            {
                var nivelList = listar.NiveisRepository.FindAll();


                foreach (var cand in nivelList)
                {
                    Console.WriteLine("____________________________________________________________________________");
                    Console.WriteLine($"|Id{cand.Id}\n|Descrição:{cand.Descricao} ");
                    Console.WriteLine("____________________________________________________________________________");

                }



            }
        }

        public static void listarVagasRecrutador(int id )
        {
            using (var procurarrecrutador = new UnitOfWork())
            {

                var vagas_recrutador = procurarrecrutador.VagaRepository.FindAllByRecrutadorId(id);
               

                foreach (var vaga in vagas_recrutador)
                {
                    Console.WriteLine("____________________________________________________________________________");
                    Console.WriteLine($"|Id{vaga.Id}\n|Tipo: {vaga.Tipo}\n|Regime: {vaga.Regime}\n|Horario: {vaga.Horario} " +
                                     $"\n|Tipo de Contrato: {vaga.Tipo_contrato} \n|Nº de Vaga: {vaga.N_vaga} \n" +
                                     $"\n|Id RecrutadorId: {vaga.RecrutadorId}\n|Id Estado {vaga.EstadoId}\n|Id Cargo {vaga.CargoId}" +
                                     $"\n|Id Empresa: {vaga.EmpresaId} ");
                    Console.WriteLine("____________________________________________________________________________");


                }

            }

        }
        public static void listarVagasDisponiveis()
        {
            using (var procurarestadoevagas = new UnitOfWork())
            {
                var procurarvagas = procurarestadoevagas.VagaRepository.FindAll();
               
                foreach (var vaga in procurarvagas)
                {
                    
                    if ( vaga.EstadoId == 1 &&  vaga.N_vaga >0)
                    {
                      
                        int buscarestado = 1, vagaencontrada, buscarnomeempresa;
                        string nomeestado, nomecargo, nomeempresa;
                        //procurar estado para mostra em texto
                        var procurarvaga = procurarestadoevagas.VagaRepository.FindbyId(vaga.Id);
                        var procurar = procurarestadoevagas.EstadoRepository.FindbyId(buscarestado);
                        nomeestado = procurar.Descricao;
                        //procurar CragoID para mostra em texto
                        vagaencontrada = procurarvaga.CargoId;
                        var procurarcargo = procurarestadoevagas.CargoRepository.FindbyId(vagaencontrada);
                        nomecargo = procurarcargo.Descricao;
                        //
                        buscarnomeempresa = procurarvaga.EmpresaId;
                        var procurarempresa = procurarestadoevagas.CargoRepository.FindbyId(buscarnomeempresa);
                        nomeempresa = procurarempresa.Descricao;
                       
                        Console.WriteLine("____________________________________________________________________________");
                        Console.WriteLine($"|Id: {vaga.Id}\n|Tipo: {vaga.Tipo} \n|Regime: {vaga.Regime}\n|Horario: {vaga.Horario} " +
                                $"\n|Tipo de Contrato: {vaga.Tipo_contrato} \n|Nº de Vaga: {vaga.N_vaga} \n" +
                                $"\n|Id Recrutador: {vaga.RecrutadorId}\n|Id Estado: {nomeestado}\n|Id Cargo: {nomecargo}" +
                                $"\n|Id Empresa: {nomeempresa} ");
                        Console.WriteLine("____________________________________________________________________________");

                    }
                   


                }

            }
        }

        public static void listarVgagasCandidatadas(int id)
        {

            using (var procurarvagascandidatadas = new UnitOfWork())
            {
                var listarcandidaturas = procurarvagascandidatadas.Candidato_has_vagaRepository.FindAllByCandidatoId(id);
                
                foreach (var vaga in listarcandidaturas)
                {
                    Console.WriteLine("____________________________________________________________________________");
                    Console.WriteLine($"|Id: {vaga.Id} \n|Id Candidato: {vaga.CandidatoId}\n|Id Vaga: {vaga.VagaId} " +
                        $"\n|Descrição: {vaga.Descricao}\n|Data: {vaga.Data} \n" +
                        $"\n|Estado Candidatura: {vaga.Estado_da_Candidatura}");
                    Console.WriteLine("____________________________________________________________________________");

                }

            }
        }
        public static void submenuEmpresa(int option)
        {
            string nome;
            switch (option)
            {
                case 1:
                    Console.Title = "Sub Menu Empresa";
                    Console.WriteLine("Nome:");
                    nome = Console.ReadLine();
                    using (var addempresa = new JobOfferDBcontext())
                    {

                        var empresa = new Empresa
                        {
                           Nome = nome,
                        };

                        addempresa.Add(empresa);
                        addempresa.SaveChanges();
                        Console.WriteLine("Empresa Adicionada com Sucesso, prencione Enter para Continuar");
                        Console.ReadKey();
                    }
                    break;

                case 2:
                    Console.Title = "Sub Menu Empresa";
                    Console.WriteLine("Lista Empresa:");
                    listarEmpresa();
                    Console.WriteLine("Escolha a Empresa que deseja Alterar id:");
                    int id = int.Parse(Console.ReadLine());
                    int guardaid;
                    Console.WriteLine("Nome:");
                    nome = Console.ReadLine();

                    using (var buscarid = new UnitOfWork())
                    {
                        var buscarid2 = buscarid.EmpresaRepository.FindbyId(id);
                        guardaid= buscarid2.Id;

                    }

                    using (var n = new JobOfferDBcontext())
                    {

                        var emp = new Cargo
                        {
                            Id = guardaid,
                            Descricao = nome,
                        };
                        n.Update(emp);
                        n.SaveChanges();
                        Console.WriteLine("Empresa Alterada com Sucesso, prencione Enter para Continuar");
                        Console.ReadKey();
                    }
                    break;

                case 3:
                    Console.Title = "Sub Menu Empresa";
                    listarEmpresa();
                    Console.ReadKey();
                    break;

                case 4:
                    Console.Title = "Sub Menu Empresa";
                    listarEmpresa();
                    Console.WriteLine("\nQual a Empresa  que deseja apaga:");
                    Console.WriteLine("Id:");
                    int descri = int.Parse(Console.ReadLine());
                    using (var deletarempresa = new UnitOfWork())
                    {
                        var buscarcarg = deletarempresa.EmpresaRepository.FindbyId(descri);
                        deletarempresa.EmpresaRepository.Delete(buscarcarg);
                        deletarempresa.Save();
                    }
                    Console.WriteLine("Empresa Eliminada com Sucesso, prencione Enter para Continuar");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Erro: Opção incorreta:\n precione enter para continuar.");
                    Console.ReadKey();
                    break;
            }
        }




    }
 
}   
