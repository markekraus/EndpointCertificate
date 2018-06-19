using System;
using System.Management.Automation;

namespace markekraus.EndpointCertificate
{
    class ValidateEndpointUriAttribute : ValidateArgumentsAttribute {
       protected override void Validate(object arguments, EngineIntrinsics engineIntrinsics) {
            Uri uri = (Uri)arguments;
            if(uri.Scheme != "https")
            {
                throw new ParameterBindingException("URI must begin with https.");
            }
        }
    }
}
