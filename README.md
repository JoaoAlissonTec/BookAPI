
# Book API

O projeto foi criado com intuito de aprendizado no assunto sobre Entity Framework Core. A API lista livros que podem ser adicionados, alterados e excluídos pelos usuários.

## Requisito

Para debugar/rodar o projeto na sua máquina local, realize os seguintes passos:
- Crie um arquivo com nome "appsettings.json" na solução.
- Copie o seguinte código para dentro do arquivo criado:
```bash
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "Database": "[SUA STRING DE CONEXÃO AQUI]"
  },
  "AllowedHosts": "*"
}

```
Feito os passos acima o projeto já pode ser debugado/rodado.
