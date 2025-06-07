#  📚 Biblioteca API - Trabalho Final falvojr

Este repositório documenta uma jornada prática por **Clean Code**, **Design Patterns** e estrutura **DDD**, desenvolvido em c#. Sistema desenvolvido para o trabalho final.

## Como Usar Este Repositório

> [!IMPORTANT]
> **Compilação do projeto:** A versão do dotnet precisa ser a 9.0
> link para dowload da versão mais recente https://dotnet.microsoft.com/pt-br/download/dotnet/9.0

```bash
# 1. Clone o repositório
git clone https://github.com/LucasMatheus144/BibliotecaAplication.git

# 2. Instale os pacotes NuGet necessários
dotnet add package NHibernate
dotnet add package Npgsql

# 3. Navegue até o projeto
# Abra a solução no Visual Studio (TrabalhoFinal.sln)
# ou navegue até a pasta via terminal:

cd C:\Users\[SEU_USUARIO]\[CAMINHO_ONDE_SALVOU]\BibliotecaAPI\Biblioteca.API

# 4. Valide a compilação do projeto
dotnet build

# 5. Execute a aplicação
# Se não houver erros de compilação, a API estará pronta para uso:
dotnet run

# Observação:
# Caso ocorram erros de compilação, verifique se os pacotes NuGet estão corretos
# ou se a versão do SDK do .NET instalada na máquina é compatível com o projeto
```

## ⚠ Possiveis Erros

* Se houver falhas durante o build , verifique:
* Se os pacotes NuGet foram instalados corretamente.
* Se a versão do SDK do .NET instalada é compatível a 9.0
* Se a string de conexão está corretamente apontando para o banco PostgreSQL ativo.


### Dicas 

* Voce precisa configurar a string de conection com o banco de dados, ficando dentro da soliução ConsoleApp.INFRA > Config > appsettins.json
* Toda a estrutuda do banco de dados que voce deve ter, está no ConsoleApp.INFRA > DataBaseSchema.sql

