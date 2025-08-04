using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using System.Text;
using GarnetAccounting.Interfaces;
using GarnetAccounting.ViewModels.Tax;

namespace GarnetAccounting.Services
{
    public class CsrService : ICsrService
    {
        public CsrResult GenerateCsrForHoghooghi(CsrInfoHoghooghi csrInfo)
        {
            var keyPair = GenerateKeyPair();
            var csr = GenerateCsrRequest(csrInfo, keyPair);
            return PrepareCsrResult(csr, keyPair);
        }

        public CsrResult GenerateCsrForHaghighi(CsrInfoHaghighi csrInfo)
        {
            var keyPair = GenerateKeyPair();
            var csr = GenerateCsrRequest(csrInfo, keyPair);
            return PrepareCsrResult(csr, keyPair);
        }

        private AsymmetricCipherKeyPair GenerateKeyPair()
        {
            var keyGenerationParameters = new KeyGenerationParameters(new SecureRandom(), 2048);
            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            return keyPairGenerator.GenerateKeyPair();
        }

        private Pkcs10CertificationRequest GenerateCsrRequest(CsrInfoHoghooghi csrInfo, AsymmetricCipherKeyPair keyPair)
        {
            var subjectName = new X509Name($"C={csrInfo.Country}, O={csrInfo.Organization}, OU={csrInfo.OrganizationalUnit1}, CN={csrInfo.CommonName}, E={csrInfo.Email}, SERIALNUMBER={csrInfo.SerialNumber}");
            return new Pkcs10CertificationRequest("SHA256WITHRSA", subjectName, keyPair.Public, null, keyPair.Private);
        }

        private Pkcs10CertificationRequest GenerateCsrRequest(CsrInfoHaghighi csrInfo, AsymmetricCipherKeyPair keyPair)
        {
            var subjectName = new X509Name($"C={csrInfo.Country}, O={csrInfo.Organization}, CN={csrInfo.CommonName}, E={csrInfo.Email}, SERIALNUMBER={csrInfo.SerialNumber}");
            return new Pkcs10CertificationRequest("SHA256WITHRSA", subjectName, keyPair.Public, null, keyPair.Private);
        }

        private CsrResult PrepareCsrResult(Pkcs10CertificationRequest csr, AsymmetricCipherKeyPair keyPair)
        {
            var csrResult = new CsrResult
            {
                CSRCod = ConvertToPem(csr),
                GeneralKey = ConvertToPem(keyPair.Public),
                PrivateKey = ConvertPrivateKeyToPkcs8Pem(keyPair.Private)
            };

            return csrResult;
        }

        private string ConvertToPem(object obj)
        {
            using var writer = new StringWriter();
            var pemWriter = new PemWriter(writer);
            pemWriter.WriteObject(obj);
            return writer.ToString();
        }

        private string ConvertPrivateKeyToPkcs8Pem(AsymmetricKeyParameter privateKey)
        {
            PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);
            byte[] serializedPrivateBytes = privateKeyInfo.GetDerEncoded();
            string base64Encoded = Convert.ToBase64String(serializedPrivateBytes);

            StringBuilder pem = new StringBuilder();
            pem.AppendLine("-----BEGIN PRIVATE KEY-----");
            pem.AppendLine(InsertLineBreaks(base64Encoded));
            pem.AppendLine("-----END PRIVATE KEY-----");

            return pem.ToString();
        }

        private static string InsertLineBreaks(string base64Encoded)
        {
            const int lineLength = 64;
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < base64Encoded.Length; i += lineLength)
            {
                if (i + lineLength > base64Encoded.Length)
                {
                    result.AppendLine(base64Encoded.Substring(i));
                }
                else
                {
                    result.AppendLine(base64Encoded.Substring(i, lineLength));
                }
            }

            return result.ToString();
        }

        public string LoadPrivateKey(string pemString)
        {
            var pemLines = pemString.Split('\n').Select(line => line.Trim());

            int beginIndex = pemLines
                .Select((line, index) => new { Line = line, Index = index })
                .FirstOrDefault(item => item.Line.StartsWith("-----BEGIN"))
                ?.Index ?? -1;

            int endIndex = pemLines
                .Select((line, index) => new { Line = line, Index = index })
                .LastOrDefault(item => item.Line.StartsWith("-----END"))
                ?.Index ?? -1;

            if (beginIndex < 0 || endIndex < 0 || beginIndex >= endIndex)
            {
                throw new InvalidOperationException("Invalid PEM format");
            }

            // Extract the substring between "-----BEGIN" and "-----END"
            string extractedKey = string.Join("\n", pemLines.Skip(beginIndex + 1).Take(endIndex - beginIndex - 1));

            return extractedKey;
        }
    }
}
