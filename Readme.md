# Repositório dedicado a resolução do desafio teste de backend da Avonale
    O desafio consiste em criar uma api REST usando C#, .NET Core, Entity Framework Core que comunique
    com o banco de dados postgresql.

## Adaptações necessárias
    O dump inicial provido não foi reconhecido logo de cara pelo postgresql e algumas adaptações tiveram
    que ser feitas para corrigir os erros que surgiram.

    - O tipo dos id's foi alterado de AUTO_INCREMENT para SERIAL
    - Todas as datas foram alteradas de DATETIME para TIMESTAMP
    - Foi criado um tipo estadoCompra para o campo estado da tabela de transações (o enum não era reconhecido)