SELECT * FROM Autor;

CREATE TABLE Livro(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Nome NVARCHAR(50) NOT NULL,
	AutorId INT NOT NULL FOREIGN KEY REFERENCES Autor(Id)
);

DROP TABLE Livro;

SELECT * FROM Livro;

INSERT INTO Livro
	(Nome, AutorId)
	VALUES ('Harry Potter', 17);

INSERT INTO Livro
	(Nome, AutorId)
	VALUES ('Crônicas de Nárnia', 1),
		   ('A Bússola de Ouro', 1),
		   ('Dragonlance', 1);

SELECT Livro.Nome, Autor.Nome FROM Livro
	INNER JOIN Autor 
	ON Livro.AutorId = Autor.Id;