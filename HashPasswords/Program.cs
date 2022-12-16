using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

var tempoDecorrido = Stopwatch.StartNew();

var senhas = new HashSet<string>()
{
    "1aa41cab2321711523f6daa34e009492",
    "ad61dfd4bbd9432941292a3927668776",
    "7497e482b8d38647c7806487cf6481d4",
    "2ce9aff916492d784087bfbac1aa9034",
    "dcb7d034362b6c6630cdad79d00ad15a"
};

string[] linhasDicionarioDeSenhas = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "rockyou.txt"));

Console.WriteLine($"Linhas lidas do arquivo: {linhasDicionarioDeSenhas.Count()}");

foreach (var linhaDicionario in linhasDicionarioDeSenhas)
{
    string removerSenha = string.Empty;

    foreach (var senha in senhas)
    {
        if (senha.ToUpper().Equals(RealizarHashMD5(linhaDicionario)))
        {
            removerSenha = senha;
            Console.WriteLine($"{senha} - {linhaDicionario}");
        }

        if (!string.IsNullOrEmpty(removerSenha))
            senhas.Remove(removerSenha);
    }
}

Console.WriteLine();
Console.WriteLine($"Tempo decorrido: {tempoDecorrido.Elapsed.TotalSeconds}");

string RealizarHashMD5(string senha)
{
    StringBuilder stringBuilder = new StringBuilder();
    foreach (var letra in MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(senha)))
    {
        stringBuilder.Append(letra.ToString("X2"));
    }

    return stringBuilder.ToString();
}