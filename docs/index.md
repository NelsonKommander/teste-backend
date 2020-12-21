# Documentação das API's teste backend
Aqui se encontra a documentação das API's feitas para o desafio teste.

Para mais informações sobre o SGBD usado ver o [README](https://github.com/NelsonKommander/teste-backend/blob/master/Readme.md).

## Sumário
- [Produtos](#produtos)
- [Compras](#compras)
- [Pagamentos](#pagamentos)

## Produtos
[Ver código fonte](https://github.com/NelsonKommander/teste-backend/blob/master/Controllers/ProdutoController.cs)

#### Rota da API:
/api/produtos

### GET (" ")

#### Descrição

Retorna um array com os todos produtos cadastrados.

#### Exemplo de retorno
```
[
    {
        "id": 1,
        "nome": "Macbook 13" 8GB|128SSD|2.7Ghz",
        "valorUnitario": 8450.00,
        "qtdeEstoque": 4
    }
]
```
#### Error codes

##### 400

Erro interno durante a consulta no banco de dados. Será recebida a mensagem
"Ocorreu um erro desconhecido".

### GET ("/:id")

#### Descrição

Retorna o produto específicado pelo id fornecido com seu id,
nome, valor unitário, quantidade de estoque, data da última venda e valor total
da última venda

Caso o produto não tenha nenhuma venda cadastrada, **valorVenda** e **dataVenda**
serão **null**.

#### Exemplo de retorno
```
{
    "id": 1,
    "nome": "Macbook 13" 8GB|128SSD|2.7Ghz",
    "valorUnitario": 8450.00,
    "qtdeEstoque": 4,
    "valorVenda": 8450.00,
    "dataVenda": "2020-12-20T22:09:34.80645"
}
```

#### Error codes

##### 400

Erro interno durante a consulta no banco de dados. Será recebida a mensagem
"Ocorreu um erro desconhecido".

Uma possível causa deste erro é que o id passado não corresponde a nenhum produto.


### POST (" ")

#### Descrição

Esta rota cadastra um novo produto no banco de dados.

O valor do produto e a quantidade de estoque devem ser maiores que 0.

#### Exemplo de envio

```
{
    "nome": "Redmi Note 9 128GB|4GB RAM",
    "valorUnitario": 1660.00,
    "qtdeEstoque": 4,
}
```

#### Error codes

##### 400

Erro interno durante o cadastro no banco de dados. Será recebida a mensagem
"Ocorreu um erro desconhecido".

##### 412

Erro de validação. Será recebido um objeto json que contem os campos inválidos e
a descrição dos seus erros.

```
{
    "message": "Os valores informados não são válidos",
    "errors": [
        {
            "field": "Nome",
            "message": "O nome do produto deve conter entre 1 e 45 caracteres"
        },
        {
            "field": "QtdeEstoque",
            "message": "Quantidade inválida"
        },
        {
            "field": "ValorUnitario",
            "message": "Valor unitário inválido"
        }
    ]
}
```

## Compras
[Ver código fonte](https://github.com/NelsonKommander/teste-backend/blob/master/Controllers/CompraController.cs)

#### Rota da API
/api/compras

### POST (" ")

#### Descrição

Esta rota realiza a compra de um produto, dá baixa no estoque e salva a transação.

A compra pode aprovada ou rejeitada baseada na resposta do gateway de pagamentos.
> O gateway de pagamentos especificado pelo desafio não pode ser alcançado
> então foi usada a API de pagamentos implementada.

#### Exemplo de envio
```
{
    "produto_id": 1,
    "qtde_comprada": 1,
    "cartao": {
        "titular": "John Doe",
        "numero": "4111111111111111",
        "data_expiracao": "12/2018",
        "bandeira": "VISA",
        "cvv": "123",
    }
}
```

#### Error codes

##### 400

Erro interno durante o cadastro no banco de dados ou na rota de pagamentos.
Será recebida a mensagem "Ocorreu um erro desconhecido".

##### 412

Erro de validação. Será recebido um objeto json que contem os campos inválidos e
a descriçãos do seus erros.

**Caso não seja passado cartão**
```
{
    "message": "Os valores informados não são válidos",
    "errors": [
        {
            "field": "Cartao",
            "message": "Cartão de crédito inválido"
        },
        {
            "field": "Produto_Id",
            "message": "Id inválido"
        },
        {
            "field": "Qtde_Comprada",
            "message": "Quantidade inválida"
        }
    ]
}
```

**Caso o cartão tenha sido passado mas algum campo dele esteja inválido**
```
{
    "message": "Os valores informados não são válidos",
    "errors": [
        {
            "field": "Produto_Id",
            "message": "Id inválido"
        },
        {
            "field": "Qtde_Comprada",
            "message": "Quantidade inválida"
        },
        {
            "field": "Cartao.Cvv",
            "message": "CVV inválido"
        },
        {
            "field": "Cartao.Numero",
            "message": "Número do cartão inválido"
        },
        {
            "field": "Cartao.Titular",
            "message": "Nome do titular inválido"
        },
        {
            "field": "Cartao.Bandeira",
            "message": "Bandeira inválida"
        },
        {
            "field": "Cartao.Data_Expiracao",
            "message": "Data de expiração inválida"
        }
    ]
}
```

## Pagamentos
[Ver código fonte](https://github.com/NelsonKommander/teste-backend/blob/master/Controllers/PagamentoController.cs)

#### Rota da API
/api/pagamento/compras


### POST (" ")

Esta rota é uma API de pagamentos genérica que autoriza ou recusa as transações.



#### Exemplo de envio
```
{
    "valor": 100.00,
    "cartao": {
        "titular": "John Doe",
        "numero": "4111111111111111",
        "data_expiracao": "12/2018",
        "bandeira": "VISA",
        "cvv": "123",
    }
}
```

#### Error codes

##### 400

Erro interno durante o cadastro no banco de dados ou na rota de pagamentos.
Será recebida a mensagem "Ocorreu um erro desconhecido".

##### 412

Erro de validação. Será recebido um objeto json que contem os campos inválidos e
a descrição dos seus erros.

**Caso não seja passado cartão**
```
{
    "message": "Os valores informados não são válidos",
    "errors": [
        {
            "field": "Valor",
            "message": "Valor inválido"
        },
        {
            "field": "Cartao",
            "message": "Cartão de crédito inválido"
        }
    ]
}
```

**Caso o cartão tenha sido passado mas algum campo dele esteja inválido**
```
{
    "message": "Os valores informados não são válidos",
    "errors": [
        {
            "field": "Valor",
            "message": "Valor inválido"
        },
        {
            "field": "Cartao.Cvv",
            "message": "CVV inválido"
        },
        {
            "field": "Cartao.Numero",
            "message": "Número do cartão inválido"
        },
        {
            "field": "Cartao.Titular",
            "message": "Nome do titular inválido"
        },
        {
            "field": "Cartao.Bandeira",
            "message": "Bandeira inválida"
        },
        {
            "field": "Cartao.Data_Expiracao",
            "message": "Data de expiração inválida"
        }
    ]
}
```