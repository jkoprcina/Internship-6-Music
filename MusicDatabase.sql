CREATE DATABASE MUSIC

CREATE TABLE Musician(
	MusicianId INT PRIMARY KEY IDENTITY,
	Name varchar(50),
	Nationality varchar(50)
)

CREATE TABLE Album(
	AlbumId INT PRIMARY KEY IDENTITY,
	Name varchar(50),
	YearOfRelease date
)

CREATE TABLE Song(
	SongId INT PRIMARY KEY IDENTITY,
	Name varchar(50),
	SongDuration time
)

CREATE TABLE AlbumSong(
	SongId INT FOREIGN KEY REFERENCES Song(SongId),
	AlbumId INT FOREIGN KEY REFERENCES Album(AlbumId)
)

ALTER TABLE Album
ADD MusicianId INT FOREIGN KEY REFERENCES Musician(MusicianId)

INSERT INTO Musician
(Name, Nationality)
VALUES
(N'Queen', N'British'), (N'Iron maiden',N'British'), (N'AC DC',N'Australian')

INSERT INTO Album
(Name, YearOfRelease, MusicianId)
VALUES
(N'Innuendo', '19910101',1),(N'The Miracle','19890101',1),(N'A Kind of Magic', '19860101',1),
(N'Brave New World', '20000101',2),(N'The Book of Souls','20150101',2),(N'The Number of the Beast', '19820101',2),
(N'Back in Black', '19800101',3),(N'Highway to Hell','19790101',3)

INSERT INTO Song
(Name, SongDuration)
VALUES
(N'I"m Going Slightly Mad', '00:04:32'), (N'These Are the Days of Our Lives', '00:05:12'), (N'The Show Must Go On','00:03:14'),
(N'Headlong', '00:03:42'), (N'Innuendo', '00:06:38'),
(N'The Miracle', '00:03:54'), (N'I Want it All', '00:04:02'), (N'The Invisible Man','00:03:44'),
(N'Breakthru', '00:04:32'), (N'Party','00:02:44'),
(N'One Vision', '00:05:03'), (N'A Kind of Magic', '00:04:12'), (N'Friends Will Be Friends','00:04:24'),
(N'Who Wants to Live Forever', '00:05:43'), (N'Princes of the Universe','00:02:55'),

(N'Brave New World', '00:06:32'), (N'Dream of Mirrors', '00:07:12'), (N'Blood Brothers','00:05:12'),
(N'Ghost of the Navigator', '00:06:50'), (N'The Nomad', '00:09:06'),
(N'If Eternity Should Fail', '00:08:28'), (N'Speed of Light', '00:05:01'), (N'The Red and the Black','00:13:34'),
(N'Death of Glory', '00:05:13'), (N'Empire of the Clouds','00:18:01'),
(N'Invaders', '00:03:24'), (N'The Number of the Beast', '00:04:51'), (N'Run to The Hills','00:03:54'),
(N'Hallowed Be Thy Name', '00:07:11'), (N'The Prisoner','00:06:02'),

(N'Hells Bells', '00:05:12'), (N'Shoot to Thrill', '00:05:17'), (N'back in Black','00:04:15'),
(N'You Shook Me All Night Long', '00:03:30'), (N'Shake a Leg', '00:04:05'),
(N'Highway to Hell', '00:03:28'), (N'Girls Got Rythm', '00:03:24'), (N'Touch Too Mouch','00:04:26'),
(N'If You Want Blood (You Got it)', '00:04:37'), (N'Night Prowler','00:06:16')

INSERT INTO AlbumSong
(AlbumId, SongId)
Values
(1, 1),(1, 2),(1, 3),(1, 4),(1, 5),
(2, 6),(2, 7),(2, 8),(2, 9),(2, 10),
(3, 11),(3, 12),(3, 13),(3, 14),(3, 15),
(4, 16),(4, 17),(4, 18),(4, 19),(4, 20),
(5, 21),(5, 22),(5, 23),(5, 24),(5, 25),
(6, 26),(6, 27),(6, 28),(6, 29),(6, 30),
(7, 31),(7, 32),(7, 33),(7, 34),(7, 35),
(8, 36),(8, 37),(8, 38),(8, 39),(8, 40)

