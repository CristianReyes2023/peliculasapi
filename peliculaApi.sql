CREATE DATABASE `peliculaApi`;

USE `peliculaApi`;

DROP TABLE IF EXISTS `peliculas`;

CREATE TABLE `peliculas` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `titulo` varchar(50) NOT NULL,
  `director` varchar(50) NOT NULL,
  `anio` varchar(50) NOT NULL,
  `genero` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
);

INSERT INTO peliculas (titulo, director, anio, genero) VALUES 
('Tiempos Violentos', 'Quentin Tarantino', '1994', 'Drama, Crimen'),
('Sueño de Fuga', 'Frank Darabont', '1994', 'Drama'),
('El Padrino', 'Francis Ford Coppola', '1972', 'Drama, Crimen'),
('El Caballero de la Noche', 'Christopher Nolan', '2008', 'Acción, Crimen, Drama'),
('Forrest Gump', 'Robert Zemeckis', '1994', 'Drama, Romance'),
('Origen', 'Christopher Nolan', '2010', 'Acción, Aventura, Ciencia ficción'),
('La Matriz', 'Lana y Lilly Wachowski', '1999', 'Acción, Ciencia ficción'),
('Bastardos sin Gloria', 'Quentin Tarantino', '2009', 'Drama, Bélico'),
('Los Infiltrados', 'Martin Scorsese', '2006', 'Crimen, Drama, Thriller'),
('Rescatando al Soldado Ryan', 'Steven Spielberg', '1998', 'Acción, Drama, Bélico');
