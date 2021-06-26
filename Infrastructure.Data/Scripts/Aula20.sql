CREATE TABLE Livro(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Titulo NVARCHAR(50),
	Isbn NVARCHAR(50),
	Lancamento DATETIME2,
	AutorId INT NULL FOREIGN KEY REFERENCES Autor(Id)
);

SELECT * FROM Livro;

INSERT INTO Livro
	(Titulo, Isbn, Lancamento, AutorId)
	VALUES ('Clean Code', 'KJASJDOJA', '2001-01-01', 1);

SELECT * FROM Autor;

UPDATE Livro
	SET Titulo = 'Clean Code 2', ISBN = 'AAAAAAAA'
	WHERE Id = 2;

DELETE FROM Livro where Id = 2;