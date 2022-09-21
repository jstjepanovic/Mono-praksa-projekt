USE [ProjectBoard]

insert into Genre (GenreId, Name) values
(NEWID(), 'Adventure'),
(NEWID(), 'Ancient'),
(NEWID(), 'Card Game'),
(NEWID(), 'City Building'),
(NEWID(), 'Civilization'),
(NEWID(), 'Educational'),
(NEWID(), 'Fantasy'),
(NEWID(), 'Fighting'),
(NEWID(), 'Horror'),
(NEWID(), 'Mystery'),
(NEWID(), 'Party Game'),
(NEWID(), 'Trivia'),
(NEWID(), 'Medieval'),
(NEWID(), 'Political'),
(NEWID(), 'Puzzle'),
(NEWID(), 'Sports'),
(NEWID(), 'Wargame')
GO

insert into Category (CategoryId, Name, Description) values
(NEWID(), 'Regional', 'Group for certain region'),
(NEWID(), 'Creation', 'Discussing board game creation'),
(NEWID(), 'Virtual play', 'Arranging virtual board game play'),
(NEWID(), 'Market', 'Discussing buy, sell, trade'),
(NEWID(), 'Competative', 'Group for competitive players, tournament organization etc.'),
(NEWID(), 'Media', 'Group for sharing different board game multimedia')
GO
