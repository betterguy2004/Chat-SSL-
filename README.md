DEMO VIDEO: 

All my password in this video is: 123456. Install OpenSSL. Open terminal with admnistrator role.
I. Generate CA(certificate authority)
1. Generate a Private Key
openssl genrsa -aes256 -out caa-key.pem 4096
My pass phrase:123456
2. Create a Certificate Signing Request (CSR):
openssl req -new -x509 -sha256 -days 365 -key caa-key.pem -out caa.pem
II.Generate Certificate
1.Create a RSA key
openssl genrsa -out certa-key.pem 4096
2. Create a Certificate Signing Request (CSR)
openssl req -new -sha256 -subj "/CN=MySSL" -key certa-key.pem -out certa.csr
3.Create the certificate
openssl x509 -req -sha256 -days 365 -in certa.csr -CA caa.pem -CAkey caa-key.pem -out certa.pem -CAcreateserial
4. Assuming the path to your generated CA certificate as C:\Users\HungPhung\ca.pem, run:
Import-Certificate -FilePath "C:\Users\HungPhung\caa.pem" -CertStoreLocation Cert:\LocalMachine\My
5. Combine SSL Certificate and Private Key, and apply.
openssl pkcs12 -export -out certa.pfx -inkey certa-key.pem -in certa.pem

