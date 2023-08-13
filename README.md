# Projeto PokeApiNet

## Descrição
O projeto PokeApiNet é uma API em ASP.NET que simula uma aplicação de captura de Pokémon. Ele permite o cadastro de Mestres Pokémon, a captura de Pokémons e a listagem dos Pokémons capturados. A API consome a PokeAPI para obter informações detalhadas sobre os Pokémons.

## Funcionalidades
- Busca de 10 Pokémons aleatórios com informações básicas, evoluções e imagens.
- Busca de um Pokémon específico com informações básicas, evoluções e imagem.
- Cadastro de Mestres Pokémon com nome, idade e CPF em banco de dados SQLite.
- Registro de captura de um Pokémon por um Mestre Pokémon. 
- Listagem dos Pokémons capturados juntamente com os detalhes dos Mestres Pokémon.
- Validação para evitar a captura repetida de um mesmo Pokémon.
 
## Tecnologias Utilizadas
- ASP.NET Core 6.0
- Entity Framework Core 6.0
- SQLite para persistência de dados
- Consumo da PokeAPI para informações sobre os Pokémons

## Como Usar
1. Clone este repositório para a sua máquina local.
2. Configure a string de conexão com o banco de dados no arquivo `appsettings.json`.
3. Execute as migrations para criar o banco de dados: `dotnet ef database update`.
4. Execute a aplicação: `dotnet run`.
5. Acesse a documentação da API em `https://localhost:5001/swagger`.
