# Copyright (c) Mark E. Kraus. All rights reserved.
# Licensed under the MIT License.

@{
    RootModule = 'EndpointCertificate.dll'
    ModuleVersion = '0.0.1'
    CompatiblePSEditions = 'Desktop', 'Core'
    GUID = '7824194a-0c84-4c6f-95b5-e7164e634223'
    Author = 'Mark E. Kraus'
    CompanyName = 'Mark E. Kraus'
    Copyright = 'Copyright (c) Mark E. Kraus. All rights reserved.'
    Description = 'A PowerShell Module for retrieving certificates form HTTPS endpoints.'
    FunctionsToExport = @()
    CmdletsToExport = 'Get-EndpointCertificate'
    VariablesToExport = @()
    AliasesToExport = @()
    PrivateData = @{
        PSData = @{
            # Tags = @()
            LicenseUri = 'https://github.com/markekraus/EndpointCertificate/blob/master/LICENSE'
            ProjectUri = 'https://github.com/markekraus/EndpointCertificate'
            # IconUri = ''
            # ReleaseNotes = ''
        }
    }
}
