using System;
using System.Collections.Generic;
using System.IO;

namespace LeituraTXT
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool varrerArquivo = true;
            while (varrerArquivo)
            {
                Console.WriteLine("Selecione a opção que deseja:");
                Console.WriteLine("1 - Filtrar por arquivo");
                Console.WriteLine("2 - Filtrar todos arquivos de c:/temp e salvar o resultados em arquivos separados");
                Console.WriteLine("3 - Filtrar todos arquivos de c:/temp e salvar o resultados em um único arquivo");
                Console.WriteLine("0 - Finalizar busca.");
                string opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        PorArquivo();
                        break;

                    case "2":
                        TodosArquivosSalvarSeparado();
                        break;

                    case "3":
                        TodosArquivosSalvarJunto();
                        break;

                    case "0":
                        varrerArquivo = false;
                        Environment.Exit(1);
                        break;

                    default:
                        Console.WriteLine("Quem disse que essa opção existe? kkkk Tenta de novooo \n");
                        break;
                }
                Console.WriteLine("----------------------------------------------------------------");
            }
        }

        private static void PorArquivo()
        {
            Console.WriteLine("Informe o nome do arquivo");

            string nomeArquivo = Console.ReadLine();

            DirectoryInfo Dir = new DirectoryInfo(@"C:\temp");
            var arquivo = Dir.GetFiles(nomeArquivo + ".txt", SearchOption.AllDirectories);

            string FileName = arquivo[0].FullName;

            Console.WriteLine("Informe a sequência que deseja buscar em todos os arquivos");

            string sequencia = Console.ReadLine();
            List<string> novoArquivo = new List<string>();
            string[] lines = File.ReadAllLines(FileName);

            foreach (string line in lines)
            {
                if (line.Contains(sequencia.ToUpper()))
                    novoArquivo.Add(line);
            }

            if (novoArquivo.Count > 0)
            {
                Console.WriteLine(@"Linhas encontradas e salvas em: C:\Selecionados\" + nomeArquivo + ".txt");
            }

            File.WriteAllLinesAsync(@"C:\Selecionados\" + nomeArquivo + ".txt", novoArquivo.ToArray());
            Console.WriteLine(@"Registrou " + novoArquivo.Count + " linhas de -> " + lines.Length + " linhas");
            Console.WriteLine(" ");
        }

        private static void TodosArquivosSalvarSeparado()
        {
            DirectoryInfo Dir = new DirectoryInfo(@"C:\temp");
            FileInfo[] Arquivos = Dir.GetFiles("*", SearchOption.AllDirectories);
            List<string> novoArquivo = new List<string>();
            Console.WriteLine("Informe a sequência que deseja buscar em todos os arquivos");
            string sequencia = Console.ReadLine();
            foreach (FileInfo arquivoItem in Arquivos)
            {
                string FileName = arquivoItem.FullName.Replace(Dir.FullName, "");

                string[] lines = File.ReadAllLines(@"C:\temp\" + FileName);

                foreach (string line in lines)
                {
                    if (line.Contains(sequencia.ToUpper()))
                        novoArquivo.Add(line);
                }
                Console.WriteLine("Resultado encontrado no aquivo " + FileName);
                Console.WriteLine(@"Registrou " + novoArquivo.Count + " linhas de -> " + lines.Length + " linhas");

                File.WriteAllLinesAsync(@"C:\Selecionados" + FileName, novoArquivo.ToArray());
                foreach (string item in novoArquivo)
                {
                    Console.WriteLine("\t" + item);
                }
                novoArquivo = new List<string>();
                Console.WriteLine(" ");
            }
        }

        private static void TodosArquivosSalvarJunto()
        {
            DirectoryInfo Dir = new DirectoryInfo(@"C:\temp");
            FileInfo[] Arquivos = Dir.GetFiles("*.txt", SearchOption.AllDirectories);
            List<string> novoArquivo = new List<string>();

            Console.WriteLine("Informe a sequência que deseja buscar em todos os arquivos");
            string sequencia = Console.ReadLine();

            foreach (FileInfo arquivoItem in Arquivos)
            {
                string FileName = arquivoItem.FullName.Replace(Dir.FullName, "");

                string[] lines = File.ReadAllLines(@"C:\temp\" + FileName);

                foreach (string line in lines)
                {
                    if (line.Contains(sequencia.ToUpper()))
                        novoArquivo.Add(line);
                }
            }

            File.WriteAllLinesAsync(@"C:\Selecionados\TodosResultados.txt", novoArquivo.ToArray());
            foreach (string item in novoArquivo)
            {
                Console.WriteLine("\t" + item);
            }
            Console.WriteLine(" ");
        }
    }
}