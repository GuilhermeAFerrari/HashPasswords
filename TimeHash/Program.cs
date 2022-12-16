using Isopoh.Cryptography.Argon2;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

double tempo = 15;

var senha = "Sup3erS3cret@!";

double segundos = 0;
int total = 0;

Console.WriteLine($"Quantas vezes {senha} podem ser feito hash em {tempo} segundos");

Console.WriteLine();
Console.WriteLine("________________________________________________________________");
Console.WriteLine();

(segundos, total) = RealizarHashMD5(senha);
Console.WriteLine($"Hash MD5: Tempo: {segundos}, Vezes: {total:N}");
Console.WriteLine();

(segundos, total) = RealizarHashSHA256(senha);
Console.WriteLine($"Hash SHA256: Tempo: {segundos}, Vezes: {total:N}");
Console.WriteLine();

(segundos, total) = RealizarHashArgon2(senha);
Console.WriteLine($"Hash Argon2: Tempo: {segundos}, Vezes: {total:N}");
Console.WriteLine();

(double, int) RealizarHashMD5(string senha)
{
    int total = 0;
    var segundos = Stopwatch.StartNew();
    
    do
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (var letra in MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(senha)))
        {
            stringBuilder.Append(letra.ToString("X2"));
        }

        total++;
    } while (segundos.Elapsed.TotalSeconds <= tempo);

    segundos.Stop();

    return (segundos.Elapsed.TotalSeconds, total);
}

(double, int) RealizarHashSHA256(string senha)
{
    int total = 0;
    var segundos = Stopwatch.StartNew();

    do
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            total++;
        }
    } while (segundos.Elapsed.TotalSeconds <= tempo);

    segundos.Stop();

    return (segundos.Elapsed.TotalSeconds, total);
}

(double, int) RealizarHashArgon2(string senha)
{
    int total = 0;
    var segundos = Stopwatch.StartNew();

    do
    {
        var value = Argon2.Hash(senha);
        total++;
    } while (segundos.Elapsed.TotalSeconds <= tempo);

    segundos.Stop();

    return (segundos.Elapsed.TotalSeconds, total);
}
