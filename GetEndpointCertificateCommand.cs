using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace markekraus.EndpointCertificate
{
    [Cmdlet(VerbsCommon.Get,"EndpointCertificate")]
    [OutputType(typeof(EndpointCertificate))]
    public class GetEndpointCertificateCommand : PSCmdlet
    {
        private static RemoteCertificateValidationCallback trustAllCertificates = 
            (sender, cert, chain, errors) => 
            {
                // The Certificate and chain are disposed when callback completes so we clone them
                var newCert = new X509Certificate2(cert);
                var newChain = new X509Chain();
                newChain.Build(newCert);

                lastEndpointCertificate = new EndpointCertificate(){
                    Certificate = newCert,
                    CertificateChain = newChain,
                    PolicyErrors = errors
                };

                return true;
            };
        
        private static EndpointCertificate lastEndpointCertificate;

        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        [ValidateEndpointUri()]
        public Uri Uri { get; set; }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            try 
            {
                using(var client = new TcpClient())
                {
                    client.Connect(Uri.Host, Uri.Port);

                    using(var sslStream = new SslStream(client.GetStream(), false, trustAllCertificates, null))
                    {
                        sslStream.AuthenticateAsClient(Uri.Host);
                        
                        // lastEndpointCertificate  is set in the SslStream Validation Callback.
                        // null indicates the callback failed.
                        if(lastEndpointCertificate == null)
                        {
                            WriteError(
                                new ErrorRecord(new NullReferenceException("Endpoint certificate was not set in SslStream validation callback."), 
                                "ValidationCallbackFailure",
                                ErrorCategory.InvalidResult,
                                Uri));
                        }

                        lastEndpointCertificate.Uri = Uri;
                        WriteObject(lastEndpointCertificate);

                        // Set to null for next process loop.
                        lastEndpointCertificate = null;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteError(
                    new ErrorRecord(
                        new Exception($"Process execution failed: {ex.Message}", ex), 
                        "ProcessFailure",
                        ErrorCategory.OperationStopped,
                        Uri));
            }
        }
    }
}
