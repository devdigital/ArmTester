# ArmTester

Test Azure Resource Manager templates

## Create Certificate

```
$cert=New-SelfSignedCertificate -Subject "CN=Foo" -CertStoreLocation "Cert:\CurrentUser\My"  -KeyExportPolicy Exportable -KeySpec Signature
$bin = $cert.RawData
$base64Value = [System.Convert]::ToBase64String($bin)
$bin = $cert.GetCertHash()
$base64Thumbprint = [System.Convert]::ToBase64String($bin)
$keyid = [System.Guid]::NewGuid().ToString()
$jsonObj = @{customKeyIdentifier=$base64Thumbprint;keyId=$keyid;type="AsymmetricX509Cert";usage="Verify";value=$base64Value}
$keyCredentials=ConvertTo-Json @($jsonObj) | Out-File "keyCredentials.txt"

Get-ChildItem -path cert:\CurrentUser\My
```

## Create an Application Registration

- Azure Active Directory -> App registrations -> New application registration
- Enter name -> Web app / API (Application type) -> Sign-on URL (any)
- Copy the Application ID, that is the clientId required for the ArmTester
- Select Manifest
- Overwrite the current keyCredentials value of [] with the contents of the keyCredentials.txt file

## Grant App Sufficient Permissions to Test Deployments

- Select the subscription or resource group in the portal, depending on the deployment level to which you will test deployments
- Select Access control (IAM)
- Add the Contributor role and search for the name of the app
- Select the app and select Save
- See https://docs.microsoft.com/en-us/azure/active-directory/develop/howto-create-service-principal-portal for more details.

## Create Blob Storage Container

- Storage Accounts -> Add -> Name -> Create
- Select storage account -> Blobs -> Add Container
- Give the container a name -> Create
- Select the new container -> Properties -> URL -> Copy
- Select the storage account -> Shared access signature -> Generate SAS and connection string
- Copy the SAS token (it must begin with ?)

The blob container URL and SAS token are required for deployments
