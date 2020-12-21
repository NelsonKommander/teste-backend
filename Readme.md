# Repositório dedicado a resolução do desafio teste de backend da Avonale
O desafio consiste em criar uma api REST usando C#, .NET Core, Entity Framework Core que comunique
com o banco de dados postgresql.

## Configuração
A string de conexão do banco de dados deve ser substituída em appsettings.json

## Documentação
A documentação pode ser encontrada [aqui](https://nelsonkommander.github.io/teste-backend/)

## Adaptações necessárias
Não consegui acessar o gateway indicado no desafio, no lugar dele enviei a requisição para a API feita no item 6.

O dump inicial provido não foi reconhecido logo de cara pelo postgresql e algumas adaptações tiveram
que ser feitas para corrigir os erros que surgiram.

- O tipo dos id's foi alterado de AUTO_INCREMENT para SERIAL
- Todas as datas foram alteradas de DATETIME para TIMESTAMP
- O Entity Framework não mapeia o enum do estado do postgres direito e causou problemas. A restrição foi movida do banco para o backend
- Alterada a inserção das transações para não incluirem o id e deixar que a sequencia cuide disso

O [script final](https://github.com/NelsonKommander/teste-backend/blob/master/dumps%20banco/Dump.sql) usado para gerar o banco no postgres pode ser encontrado na pasta dumps banco.