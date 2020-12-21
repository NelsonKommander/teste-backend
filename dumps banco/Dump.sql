  -- Foram feitas algumas alterações ao dump dado para entrar em conformidade com o postgres

  -- id's foram alterados de AUTO_INCREMENT para serial
  -- DATETIME foram alterados para TIMESTAMP
  -- O Entity Framework não mapeia o enum do estado do postgres direito e causou problemas. A restrição foi movida do banco para o backend
  -- Alterada a inserção das transações para não incluirem o id e deixar que a sequencia cuide disso

  CREATE SCHEMA IF NOT EXISTS backend_test;
  SET search_path TO backend_test;

  -- -----------------------------------------------------
  -- Tabela produtos
  -- -----------------------------------------------------
  CREATE TABLE IF NOT EXISTS produtos (
    id SERIAL,
    nome VARCHAR(45) NOT NULL,
    valor_unitario DECIMAL(10,2) NOT NULL,
    qtde_estoque INT NOT NULL,
    data_criacao TIMESTAMP NOT NULL,
    data_atualizacao TIMESTAMP NOT NULL,
    data_exclusao TIMESTAMP NULL,
	CONSTRAINT pk_id_produto PRIMARY KEY (id)
    );

  -- -----------------------------------------------------
  -- Tabela transacoes
  -- -----------------------------------------------------
  CREATE TABLE IF NOT EXISTS transacoes (
    id SERIAL,
    produto_id INT NOT NULL,
    valor_venda DECIMAL(10,2) NOT NULL,
    data_venda TIMESTAMP NULL,
    estado VARCHAR(10) NOT NULL,
    CONSTRAINT pk_id_transacao PRIMARY KEY (id),
    CONSTRAINT fk_transacoes_produtos
      FOREIGN KEY (produto_id)
      REFERENCES produtos (id)
      ON DELETE NO ACTION
      ON UPDATE NO ACTION);

INSERT INTO produtos (nome, valor_unitario, qtde_estoque, data_criacao, data_atualizacao, data_exclusao)
VALUES
	('Macbook 13\" 8GB|128SSD|2.7Ghz', 8450.00, 5, '2018-09-20 16:13:44', '2018-09-20 16:13:44', NULL),
	('Macbook 13\" 16GB|128SSD|2.7Ghz', 8740.00, 3, '2018-09-20 16:14:34', '2018-09-20 16:14:34', NULL),
	('Macbook 13\" 16GB|256SSD|2.7Ghz', 9230.00, 8, '2018-09-20 16:15:19', '2018-09-20 16:15:19', NULL),
	('Macbook 13\" 16GB|512SSD|2.7Ghz', 1125.00, 2, '2018-09-20 16:16:43', '2018-09-20 16:16:43', NULL),
	('iPhone X 64GB', 4500.00, 10, '2018-09-20 16:18:07', '2018-09-20 16:18:07', NULL),
	('iPhone X 128GB', 5800.00, 5, '2018-09-20 16:18:28', '2018-09-20 16:18:28', NULL),
	('iPhone X 256GB', 6400.00, 3, '2018-09-20 16:18:49', '2018-09-20 16:18:49', NULL),
	('iPad Pro 64GB', 3500.00, 10, '2018-09-20 16:25:18', '2018-09-20 16:25:18', NULL),
	('iPad Pro 128GB', 4200.00, 5, '2018-09-20 16:25:52', '2018-09-20 16:25:52', NULL),
	( 'iMac Pro 27', 750000.00, 2, '2018-09-20 16:27:22', '2018-09-20 16:27:22', NULL);

INSERT INTO transacoes (produto_id, valor_venda, data_venda, estado)
VALUES
	(1, 8450.00, '2018-09-20 16:31:01', 'APROVADO'),
	(3, 9230.00, '2018-09-20 16:32:26', 'APROVADO'),
	(10, 750000.00, '2018-09-20 16:32:47', 'REJEITADO'),
	(8, 3450.00, '2018-09-20 16:33:25', 'APROVADO'),
	(5, 4400.00, '2018-09-20 16:33:56', 'REJEITADO');