using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace markekraus.EndpointCertificate
{
    class EndpointCertificate
    {
        public X509Certificate2 Certificate { get; set; }
        public X509Chain CertificateChain { get; set; }
        public SslPolicyErrors PolicyErrors { get; set; }
        public Uri Uri { get; set; }
    }
}
