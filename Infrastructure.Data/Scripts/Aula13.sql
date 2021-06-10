
-- https://docs.microsoft.com/en-us/sql/t-sql/statements/create-table-transact-sql?view=sql-server-ver15
CREATE TABLE Autor(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Nome NVARCHAR(50),
	UltimoNome NVARCHAR(50),
	Nacionalidade NVARCHAR(50),
	QuantidadeLivrosPublicados INT,
	Nascimento DATETIME2,
);

DROP TABLE Autor;

 -- https://www.w3schools.com/sql/sql_select.asp
SELECT * FROM Autor;
SELECT * FROM Autor WHERE Id = 17;

-- https://www.w3schools.com/sql/sql_insert.asp
INSERT INTO Autor
	(Nome, UltimoNome, Nacionalidade, QuantidadeLivrosPublicados, Nascimento)
	OUTPUT INSERTED.Id
	VALUES ('Felipe', 'Andrade', 'Brasileiro', 5, '1988-02-23');
INSERT INTO Autor
	(Nome, UltimoNome, Nacionalidade, QuantidadeLivrosPublicados, Nascimento)
	VALUES ('João', 'Neves', 'Brasileiro', 10, '2000-02-23');

 -- https://www.w3schools.com/sql/sql_update.asp
UPDATE Autor
	SET Nome = 'aaa', UltimoNome = 'aaa', Nacionalidade = 'aaa', 
	QuantidadeLivrosPublicados = 20, Nascimento = '2020-02-23'
	WHERE Id = 1002;

-- https://www.w3schools.com/sql/sql_delete.asp
DELETE FROM Autor WHERE Id = 16;